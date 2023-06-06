using System.Windows.Input;
using Drrobo.Modules.Shared.ViewModels;
using Drrobo.Modules.Shared.Services.Navigation;
using Drrobo.Modules.Dashboard.Enums;
using Drrobo.Modules.Dashboard.Components.Content;
using Drrobo.Modules.Dashboard.Views;
using Drrobo.Modules.Dashboard.Models;
using Drrobo.Modules.RemotelyControlled.Enums;
using Drrobo.Modules.RemotelyControlled.ViewModels;
using System.Collections.ObjectModel;
using Drrobo.Utils.Bluetooth;
using Plugin.BLE.Abstractions.Contracts;
using CommunityToolkit.Maui.Views;
using Drrobo.Modules.Shared.Components.PopUp;
using Drrobo.Modules.Shared.Components.RemoteControl;
using Drrobo.Modules.Dashboard.Components.Popup;
using System.Globalization;
using Drrobo.Utils.Translations;
using Drrobo.Modules.Dashboard.Data;
using Drrobo.Modules.Cards.ViewModels;

namespace Drrobo.Modules.Dashboard.ViewModels
{
    public class StartViewModel : BaseViewModel<StartModel>
    {
        public ICommand SetContentTypeCommand => new Command(async (value) => await SetContentTypeAsync((DashboardPageTypeEnum)value));
        public ICommand RemotelyControlledCommand => new Command(async (value) => await RemotelyControlledAsync((CarouselItems)value));
        public ICommand EnterCommand => new Command(async () => await EnterAsync());
        public ICommand AccessCardsViewCommand => new Command(async () => await AccessCardsViewAsync());
        public ICommand ProfileClickButtonCommand => new Command(async (value) => await ProfileClickButtonAsync((ProfileButtonEnum)value));

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
        LanguageData _languageData;

        public StartViewModel(INavigationService serviceNavigation, IBluetoothUtil bluetoothUtil)
        {
            _serviceNavigation = serviceNavigation;
            _bluetoothUtil = bluetoothUtil;
            _languageData = new LanguageData();

            GetLanguage();
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
                case CarouselItems.Cards:
                    await _serviceNavigation.NavigateToAsync<HomeCardsViewModel>();
                    break;
            }
        }

        private async Task EnterAsync()
        {
            Model.CommandsList.Add(Model.CommandText);
            Model.CommandText = string.Empty;

            switch (Model.CommandsList.LastOrDefault())
            {
                case "drone open_cam":
                    Model.DroneCamOn = true;
                    break;
                case "drone close_cam":
                    Model.DroneCamOn = false;
                    break;
                case "jumper open_cam":
                    Model.JumperCamOn = true;
                    break;
                case "jumper close_cam":
                    Model.JumperCamOn = false;
                    break;
                case "clear":
                    Model.CommandsList = new ObservableCollection<string>();
                    break;
                case "ble connect":
                    await BluetoothPopupAsync();
                    break;
                case "jumper left":
                    await _bluetoothUtil.SendAsync(Model.Bluetooth.ConnectedDevice, "L");
                    break;
                case "jumper front":
                    await _bluetoothUtil.SendAsync(Model.Bluetooth.ConnectedDevice, "F");
                    break;
                case "jumper right":
                    await _bluetoothUtil.SendAsync(Model.Bluetooth.ConnectedDevice, "R");
                    break;
                case "jumper back":
                    await _bluetoothUtil.SendAsync(Model.Bluetooth.ConnectedDevice, "B");
                    break;
                case "jumper stop":
                    await _bluetoothUtil.SendAsync(Model.Bluetooth.ConnectedDevice, "S");
                    break;
                case "remote_control":
                    await Application.Current.MainPage.ShowPopupAsync(new RemoteControlPopup());
                    break;
                default:
                    break;
            }
        }

        private async Task BluetoothPopupAsync()
        {
            var result = (IDevice)await Application.Current.MainPage
                .ShowPopupAsync(new BluetoothPopup(await _bluetoothUtil.SearchDevicesAsync()));

            if (result != null)
                Model.Bluetooth.ConnectedDevice = await _bluetoothUtil.SelectDeviceAsync(result);
        }

        private async Task ProfileClickButtonAsync(ProfileButtonEnum value)
        {
            switch (value)
            {
                case ProfileButtonEnum.Language:
                    await SetLanguageAsync();
                    break;
                case ProfileButtonEnum.ConfigServerProfile:
                    await _serviceNavigation.NavigateToAsync<ConfigureServerViewModel>();
                    break;
                case ProfileButtonEnum.ConfigJoystick:
                    await _serviceNavigation.NavigateToAsync<ConfigureJoystickViewModel>();
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
                default:
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