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
using Plugin.BLE.Abstractions.Contracts;
using Drrobo.Modules.Shared.Components.PopUp;

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
                await CreateDeviceAsync(command);
                return;
            }

            switch (command)
            {
                case "devices":
                    ListDevices();
                    break;
                case "connect":
                    ConnectDevice(value);
                    break;
                case "disconnect":
                    Disconnect();
                    break;
                case "open_cam":
                    OpenCloseCam(true);
                    break;
                case "close_cam":
                    OpenCloseCam(false);
                    break;
                case "create":
                    if (!string.IsNullOrEmpty(value) && separatedCommand.Length > 1)
                        await CreateDeviceAsync(value);
                    else
                        Model.CommandsList.Add(AppResources.ErrorCreate);
                    break;
                case "remove":
                    RemoveDevice(value);
                    break;
                case "clear":
                    Model.CommandsList = new ObservableCollection<string>();
                    break;
                case "move":
                    await MovementAsync(value);
                    break;
                default:
                    Model.CommandsList.Add(AppResources.CommandNotKnown);
                break;
            }
        }

        private void ConnectDevice(string value)
        {
            Model.DeviceConnected = _deviceData.GetByName(value);
            if (Model.DeviceConnected != null)
                Model.DeviceConnectedLabel = $"{Model.DeviceConnectedLabel
                    .Split(' ')
                    .FirstOrDefault()} / {Model.DeviceConnected.Name} %";
            else
                Model.CommandsList.Add($"{value} : {AppResources.NotFound}");
        }

        private void ListDevices()
        {
            var devices = _deviceData.GetAll();
            if (devices == null || devices.Count == 0)
                Model.CommandsList.Add($"{AppResources.NoDeviceFound}");
            else
                foreach (var device in devices)
                    Model.CommandsList.Add(device.Name);
        }

        private void OpenCloseCam(bool value)
        {
            if (Model.DeviceConnected != null)
            {
                if (Model.DeviceConnected.HaveCamera)
                    Model.OpenCam = value;
                else
                    Model.CommandsList.Add(AppResources.NotHaveCamera);
            }
            else
                Model.CommandsList.Add(AppResources.DisconnectedAlert);
        }

        private void Disconnect()
        {
            Model.OpenCam = false;
            Model.DeviceConnectedLabel = $"{DeviceInfo.Name} ~ %";
            Model.DeviceConnected = null;
        }

        private async Task MovementAsync(string value)
        {
            if (Model.DeviceConnected.IsBluetooth)
                await CommunicationBLE(value, Model.Bluetooth.ConnectedDevice, _bluetoothUtil);
            else
                await CommunicationWifi(value, Model.DeviceConnected.URL, _universalService);
        }

        private async Task CreateDeviceAsync(string value)
        {
            if (value.ToLower().Equals("cancel"))
            {
                Model.CreateDevice = false;
                Model.CreationStep = 0;
                return;
            }

            switch (Model.CreationStep)
            {
                case 0:
                    if (_deviceData.GetByName(value) == null)
                    {
                        Model.CreateDevice = true;
                        Model.CreationStep = 1;
                        await CreateDeviceAsync(value);
                    }
                    else
                        Model.CommandsList.Add(AppResources.AlreadyExists);                   
                break;
                case 1:
                    Model.NewDevice = new DevicesModel();
                    Model.NewDevice.Name = value;
                    var deviceTypes = (DeviceTypeEnum[])Enum.GetValues(typeof(DeviceTypeEnum));
                    Model.CommandsList.Add($" {AppResources.DeviceTypes}: {string.Join(", ", deviceTypes)}");
                    Model.DeviceConnectedLabel = $"{AppResources.WhatTypeOfYourDevice} :";
                    Model.CreationStep = 2;
                break;
                case 2:
                    if (Enum.TryParse(value, out DeviceTypeEnum resultado))
                    {
                        Model.NewDevice.Image = resultado.Value();
                        Model.NewDevice.Type = resultado.Description();
                        if (Model.NewDevice.Type == DeviceTypeEnum.Jumper.Description())
                            Model.NewDevice.Isjoystick = true;

                        Model.DeviceConnectedLabel = AppResources.CommunicationType;
                        Model.CreationStep = 3;
                    }
                    else
                        Model.CommandsList.Add(AppResources.UnrecognizedType);
                break;
                case 3:
                    if (value.ToLower().Equals("s"))
                    {
                        var result = (IDevice)await Application.Current.MainPage
                            .ShowPopupAsync(new BluetoothPopup(await _bluetoothUtil.SearchDevicesAsync()));

                        if (result != null)
                        {
                            Model.NewDevice.IsBluetooth = true;
                            Model.NewDevice.GuidBluetooth = result.Id;

                            Model.DeviceConnectedLabel = AppResources.HaveCam;
                            Model.CreationStep = 6;
                        }
                        else
                        {
                            Model.NewDevice.IsBluetooth = false;
                        }
                    }
                    else if (value.ToLower().Equals("n"))
                    {
                        Model.NewDevice.IsBluetooth = false;
                        Model.DeviceConnectedLabel = $"{AppResources.EnterDeviceURL} :";
                        Model.CreationStep = 5;
                    }
                    else
                        Model.CommandsList.Add(AppResources.CommandNotKnown);
                break;
                case 5:
                    if (Util.IsValidUrl(value))
                    {
                        Model.NewDevice.URL = value;
                        Model.DeviceConnectedLabel = AppResources.HaveCam;
                        Model.CreationStep = 6;
                    }
                    else
                        Model.CommandsList.Add($"{value} : {AppResources.NotValidURL}");
                break;
                case 6:
                    if (value.ToLower().Equals("s"))
                    {
                        Model.NewDevice.HaveCamera = true;
                        Model.DeviceConnectedLabel = $"{AppResources.EnterCameraURL} :";
                        Model.CreationStep = 7;
                    }
                    else if (value.ToLower().Equals("n"))
                    {
                        Model.NewDevice.HaveCamera = false;
                        _deviceData.Save(Model.NewDevice);
                        Model.CommandsList.Add($"{Model.NewDevice.Name} : {AppResources.SuccessfullyCreated}");
                        Model.CreateDevice = false;
                        Model.CreationStep = 0;
                        Model.DeviceConnected = _deviceData.GetByName(Model.NewDevice.Name);
                        Model.DeviceConnectedLabel = $"{DeviceInfo.Name} / {Model.DeviceConnected.Name} %";
                    }
                    else
                        Model.CommandsList.Add(AppResources.CommandNotKnown);
                break;
                case 7:
                    if (Util.IsValidUrl(value))
                    {
                        Model.NewDevice.URLCamera = value;
                        _deviceData.Save(Model.NewDevice);
                        Model.CommandsList.Add($"{Model.NewDevice.Name} : {AppResources.SuccessfullyCreated}");
                        Model.CreateDevice = false;
                        Model.CreationStep = 0;
                        Model.DeviceConnected = _deviceData.GetByName(Model.NewDevice.Name);
                        Model.DeviceConnectedLabel = $"{DeviceInfo.Name} / {Model.DeviceConnected.Name} %";
                    }
                    else
                        Model.CommandsList.Add($"{value} : {AppResources.NotValidURL}");
                break;
            }
        }

        private void RemoveDevice(string name)
        {
            var device = _deviceData.GetByName(name);
            if(device != null)
            {
                _deviceData.Delete(device);
                Model.CommandsList.Add($"{name} : {AppResources.SuccessfullyRemoved}");
            }
            else
                Model.CommandsList.Add($"{name} : {AppResources.NotFound}");
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