using System.Windows.Input;
using Drrobo.Modules.Shared.ViewModels;
using Drrobo.Modules.Shared.Services.Navigation;
using Drrobo.Modules.Dashboard.Enums;
using Drrobo.Modules.Dashboard.Components.Content;
using Drrobo.Modules.Dashboard.Views;
using Drrobo.Modules.Dashboard.Models;
using Drrobo.Modules.RemotelyControlled.ViewModels;
using System.Collections.ObjectModel;
using Drrobo.Utils.Bluetooth;
using CommunityToolkit.Maui.Views;
using Drrobo.Modules.Shared.Components.RemoteControl;
using Drrobo.Modules.Dashboard.Components.Popup;
using System.Globalization;
using Drrobo.Utils.Translations;
using Drrobo.Modules.Dashboard.Data;
using Drrobo.Modules.Shared.Services.Data;
using Drrobo.Modules.Shared.Models;
using Drrobo.Modules.Shared.Services.Service;
using Drrobo.Modules.Shared.Enums;
using Drrobo.Utils;

namespace Drrobo.Modules.Dashboard.ViewModels
{
    public class StartViewModel : BaseViewModel<StartModel>
    {
        public ICommand SetContentTypeCommand => new Command(async (value) => await SetContentTypeAsync((DashboardPageTypeEnum)value));
        public ICommand RemotelyControlledCommand => new Command(async (value) => await RemotelyControlledAsync((CarouselItems)value));
        public ICommand EnterCommand => new Command(async () => await EnterAsync());
        public ICommand AccessCardsViewCommand => new Command(async () => await AccessCardsViewAsync());
        public ICommand ProfileClickButtonCommand => new Command(async (value) => await ProfileClickButtonAsync((ProfileButtonEnum)value));
        public ICommand GetDevicesConnectCommand => new Command(async () => await GetDevicesConnectAsync());

        private Dictionary<DashboardPageTypeEnum, Lazy<ContentView>> ContentType =
            new Dictionary<DashboardPageTypeEnum, Lazy<ContentView>>
            {
                { DashboardPageTypeEnum.Cmd, new Lazy<ContentView>(()=> new CmdContent())},
                { DashboardPageTypeEnum.Dashboard, new Lazy<ContentView>(()=> new DashBoardContent())},
                { DashboardPageTypeEnum.Profile, new Lazy<ContentView>(()=> new ProfileContent())}
            };

        public DashboardPageTypeEnum CurrentContent { get; set; } = DashboardPageTypeEnum.Dashboard;

        IBluetoothUtil _bluetoothUtil;
        INavigationService _serviceNavigation;
        IUniversalService _universalService;

        LanguageData _languageData;
        DevicesData _deviceData;
        public StartViewModel
        (
            INavigationService serviceNavigation,
            IBluetoothUtil bluetoothUtil,
            IUniversalService universalService
        )
        {
            _serviceNavigation = serviceNavigation;
            _bluetoothUtil = bluetoothUtil;
            _universalService = universalService;

            _languageData = new LanguageData();
            _deviceData = new DevicesData();
        }

        public override async Task InitializeAsync(object navigationData)
        {
            GetLanguage();
            await base.InitializeAsync(navigationData);
        }

        private void GetLanguage()
        {
            var languages = _languageData.GetAll();
            if (languages.Count > 0)
            {
                LocalizationResourceManager.Instance
                    .SetCulture(new CultureInfo(languages.FirstOrDefault().CultureInfo));

                Model.Profile.Language = languages.FirstOrDefault().Name;
            }
        }

        private async Task GetDevicesConnectAsync()
        {
            Model.DevicesList = new ObservableCollection<DevicesModel>();

            var devices = _deviceData.GetAll();
            if (devices == null || devices.Count == 0)
                return;

            foreach (var item in devices)
            {
                if (item.IsBluetooth)
                {
                    var response = await _bluetoothUtil.ConnectDeviceAsync(item.GuidBluetooth);
                    if (response != null)
                        Model.DevicesList.Add(item);
                    else
                        continue;
                }
                else 
                {
                    var response = await _universalService.HealthCheckAsync(item.URL);
                    if (response != null)
                        Model.DevicesList.Add(item);
                    else
                        continue;
                }
            }

            if (Model.DevicesList.Count > 0)
                Model.DevicesOn = true;
        }

        private async Task SetContentTypeAsync(DashboardPageTypeEnum item)
        {
            IsBusy = true;
            var currentView = Application.Current.MainPage;
            if (currentView.Navigation != null)
                currentView = currentView.Navigation.NavigationStack.Last();

            var MyView = currentView as StartView;

            if (CurrentContent == item)
                return;

            CurrentContent = item;

            await MyView.SetContent(ContentType[CurrentContent].Value);
            IsBusy = false;
        }

        private async Task RemotelyControlledAsync(CarouselItems type)
        {
            switch (type)
            {
                case CarouselItems.RemotelyControlled:
                    await _serviceNavigation.NavigateToAsync<JoystickViewModel>();
                    break;
                case CarouselItems.Camera:
                    await _serviceNavigation.NavigateToAsync<CameraMonitoringViewModel>();
                    break;
            }
        }

        private async Task EnterAsync()
        {
            var commandText = Model.CommandText;

            Model.CommandsList.Add($"{Model.DeviceConnectedLabel} {commandText}");
            Model.CommandText = string.Empty;

            await ExecutionCommand(commandText);
        }

        private async Task ExecutionCommand(string commandText)
        {
            var separatedCommand = commandText.Split(' ');
            
            var command = separatedCommand.FirstOrDefault();
            var value = separatedCommand.LastOrDefault();

            if (Model.CreateDevice)
            {
                CreateDevice(command);
                return;
            }

            switch (command)
            {
                case "devices":
                    var devices = _deviceData.GetAll();
                    if(devices == null || devices.Count == 0)
                    {
                        Model.CommandsList.Add($"nenhum device encontrado");
                        break;
                    }

                    foreach (var device in devices)
                    {
                        Model.CommandsList.Add(device.Name);
                    }
                    break;
                case "connect":
                    Model.DeviceConnected = _deviceData.GetByName(value);
                    if (Model.DeviceConnected != null)
                        Model.DeviceConnectedLabel = $"{Model.DeviceConnectedLabel
                            .Split(' ')
                            .FirstOrDefault()} / {Model.DeviceConnected.Name} %";
                    else
                        Model.CommandsList.Add($"{value} : não encontrado");
                    break;
                case "disconnect":
                    Model.DeviceConnectedLabel = $"{DeviceInfo.Name} ~ %";
                    Model.DeviceConnected = null;
                    break;
                case "open_cam":
                    if (Model.DeviceConnected != null)
                    {
                        if (Model.DeviceConnected.HaveCamera)
                            Model.OpenCam = true;
                        else
                            Model.CommandsList.Add($"dispositivo não possui camera");
                    }
                    else
                        Model.CommandsList.Add($"conecte-se em um dispositivo para realizar essa ação");

                    break;
                case "close_cam":
                    if (Model.DeviceConnected != null)
                    {
                        if (Model.DeviceConnected.HaveCamera)
                            Model.OpenCam = false;
                        else
                            Model.CommandsList.Add($"dispositivo não possui camera");
                    }
                    else
                        Model.CommandsList.Add($"conecte-se em um dispositivo para realizar essa ação");
                    break;
                case "create":
                    if (!string.IsNullOrEmpty(value) && separatedCommand.Length > 1)
                    {
                        var device = _deviceData.GetByName(value);
                        if (device == null)
                        {
                            Model.CreateDevice = true;
                            CreateDevice(value);
                        }
                        else
                            Model.CommandsList.Add($"já existe um dispositivo com esse nome");
                    }
                    else
                        Model.CommandsList.Add($"o comando create deve ser seguido pelo nome do dispositivo");
                    break;
                case "remove":
                    RemoveDevice(value);
                    break;
                case "clear":
                    Model.CommandsList = new ObservableCollection<string>();
                    break;
                default:
                    Model.CommandsList.Add($"comando nao conhecido");
                break;
            }
        }

        private void CreateDevice(string value)
        {
            switch (Model.CreationStep)
            {
                case 0:
                    Model.NewDevice = new DevicesModel();
                    Model.NewDevice.Name = value;
                    var deviceTypes = (DeviceTypeEnum[])Enum.GetValues(typeof(DeviceTypeEnum));
                    Model.CommandsList.Add($" tipos de devices : {string.Join(", ", deviceTypes)}");
                    Model.DeviceConnectedLabel = "qual tipo do seu device? :";
                    Model.CreationStep = 1;
                    break;
                case 1:
                    if (Enum.TryParse(value, out DeviceTypeEnum resultado))
                    {
                        Model.NewDevice.Image = resultado.Value();
                        Model.NewDevice.Type = resultado.Description();
                        Model.DeviceConnectedLabel = "comunicação será via bluetooth? S/N :";
                        Model.CreationStep = 2;
                    }
                    else
                        Model.CommandsList.Add($"tipo não reconhecido");
                    break;
                case 2:

                    if (value.ToLower().Equals("s"))
                    {
                        Model.NewDevice.IsBluetooth = true;
                        Model.DeviceConnectedLabel = "seu dispositivo possui camera? S/N :";
                        Model.CreationStep = 5;
                    }
                    else if (value.ToLower().Equals("n"))
                    {
                        Model.NewDevice.IsBluetooth = false;
                        Model.DeviceConnectedLabel = "digite a Url do dispositivo :";
                        Model.CreationStep = 4;
                    }
                    else
                        Model.CommandsList.Add($"comando nao conhecido");
                    break;
                case 4:
                    if (Util.IsValidUrl(value))
                    {
                        Model.NewDevice.URL = value;
                        Model.DeviceConnectedLabel = "seu dispositivo possui camera? S/N :";
                        Model.CreationStep = 5;
                    }
                    else
                        Model.CommandsList.Add($"{value} : não é uma url valida");
                    break;
                case 5:
                    if (value.ToLower().Equals("s"))
                    {
                        Model.NewDevice.HaveCamera = true;
                        Model.DeviceConnectedLabel = "digite a Url para camera :";
                        Model.CreationStep = 6;
                    }
                    else if (value.ToLower().Equals("n"))
                    {
                        Model.NewDevice.HaveCamera = false;
                        _deviceData.Save(Model.NewDevice);
                        Model.CommandsList.Add($"{Model.NewDevice.Name} : creado com sucesso!");
                        Model.CreateDevice = false;
                        Model.CreationStep = 0;
                        Model.DeviceConnected = _deviceData.GetByName(Model.NewDevice.Name);
                        Model.DeviceConnectedLabel = $"{DeviceInfo.Name} / {Model.DeviceConnected.Name} %";
                    }
                    else
                        Model.CommandsList.Add($"comando nao conhecido");
                    break;
                case 6:
                    if (Util.IsValidUrl(value))
                    {
                        Model.NewDevice.URLCamera = value;
                        _deviceData.Save(Model.NewDevice);
                        Model.CommandsList.Add($"{Model.NewDevice.Name} : creado com sucesso!");
                        Model.CreateDevice = false;
                        Model.CreationStep = 0;
                        Model.DeviceConnected = _deviceData.GetByName(Model.NewDevice.Name);
                        Model.DeviceConnectedLabel = $"{DeviceInfo.Name} / {Model.DeviceConnected.Name} %";
                    }
                    else
                        Model.CommandsList.Add($"{value} : não é uma url valida");
                    break;
            }
        }

        private void RemoveDevice(string name)
        {
            var device = _deviceData.GetByName(name);
            if(device != null)
            {
                _deviceData.Delete(device);
                Model.CommandsList.Add($"{name} : removido com sucesso");
            }
            else
                Model.CommandsList.Add($"{name} : não encontrado");
        }

        private async Task ProfileClickButtonAsync(ProfileButtonEnum value)
        {
            switch (value)
            {
                case ProfileButtonEnum.Language:
                    await SetLanguageAsync();
                    break;
                case ProfileButtonEnum.ConfigDevice:
                    await _serviceNavigation.NavigateToAsync<ListDevicesViewModel>();
                    break;
                default:
                    break;
            }
        }

        private async Task SetLanguageAsync()
        {
            var response = await Application.Current.MainPage.ShowPopupAsync(new LanguagesPopup());

            if (response == null)
                return;

            CultureInfo cultureInfo = new CultureInfo("pt");
            switch ((LanguagesEnum)response)
            {
                case LanguagesEnum.Português:
                    cultureInfo = new CultureInfo("pt");
                    break;
                case LanguagesEnum.Español:
                    cultureInfo = new CultureInfo("es");
                    break;
                case LanguagesEnum.English:
                    cultureInfo = new CultureInfo("en");
                    break;
            }

            LocalizationResourceManager.Instance.SetCulture(cultureInfo);
            Model.Profile.Language = (LanguagesEnum)response;

            var languages = _languageData.GetAll();
            if (languages.Count == 0)
            {
                LanguageModel language = new LanguageModel()
                {
                    Name = (LanguagesEnum)response,
                    CultureInfo = cultureInfo.ToString()
                };

                _languageData.Save(language);
            }
            else
            {
                languages.FirstOrDefault().Name = (LanguagesEnum)response;
                languages.FirstOrDefault().CultureInfo = cultureInfo.ToString();
                _languageData.Update(languages.FirstOrDefault());
            }
        }

        private async Task AccessCardsViewAsync()
            => await Application.Current.MainPage.ShowPopupAsync(new RemoteControlPopup());
    }
}