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

namespace Drrobo.Modules.Dashboard.ViewModels
{
    public class StartViewModel : BaseViewModel<StartModel>
    {
        public ICommand SetContentTypeCommand => new Command(async (value) => await SetContentTypeAsync((DashboardPageTypeEnum)value));
        public ICommand RemotelyControlledCommand => new Command(async (value) => await RemotelyControlledAsync((RemotelyControlledTypeEnum)value));
        public ICommand EnterCommand => new Command(async () => await EnterAsync());

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

        public StartViewModel(INavigationService serviceNavigation, IBluetoothUtil bluetoothUtil)
        {
            _serviceNavigation = serviceNavigation;
            _bluetoothUtil = bluetoothUtil;
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

        private async Task RemotelyControlledAsync(RemotelyControlledTypeEnum type)
        {
            switch (type)
            {
                case RemotelyControlledTypeEnum.Drone:
                    await _serviceNavigation.NavigateToAsync<DroneViewModel>();
                    break;
                case RemotelyControlledTypeEnum.Jumper:
                    await _serviceNavigation.NavigateToAsync<JumperViewModel>();
                    break;
                case RemotelyControlledTypeEnum.Spider:
                    await _serviceNavigation.NavigateToAsync<JumperViewModel>();
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
                    await RemoteControlPopupAsync();
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

        private async Task RemoteControlPopupAsync()
        {
            await Application.Current.MainPage
                .ShowPopupAsync(new RemoteControlPopup());
        }
    }
}