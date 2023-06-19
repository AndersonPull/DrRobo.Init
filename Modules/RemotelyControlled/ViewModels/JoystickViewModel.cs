using Drrobo.Modules.Shared.ViewModels;
using Drrobo.Modules.Shared.Services.Navigation;
using Plugin.BLE.Abstractions.Contracts;
using Drrobo.Modules.RemotelyControlled.Models;
using Plugin.BLE.Abstractions;
using System.Windows.Input;
using CommunityToolkit.Maui.Views;
using Drrobo.Modules.Shared.Components.PopUp;
using Drrobo.Utils.Bluetooth;
using Drrobo.Modules.Shared.Services.Data;
using Drrobo.Modules.Shared.Models;
using System.Runtime.CompilerServices;

namespace Drrobo.Modules.RemotelyControlled.ViewModels
{
	public class JoystickViewModel : BaseViewModel<JoystickModel>
    {
        public ICommand BluetoothPopupCommand => new Command(async () => await BluetoothPopupAsync());
        public ICommand MovementCommand => new Command(async (value) => await MovementAsync((string)value));
        public ICommand ConfigureCommand => new Command(async () => await ConfigureAsync());
        public ICommand GetServerCommand => new Command(() => GetServer());

        INavigationService _serviceNavigation;
        IBluetoothUtil _bluetoothUtil;
        DevicesData _serverData;
        public JoystickViewModel(INavigationService serviceNavigation, IBluetoothUtil bluetoothUtil)
        {
            _serviceNavigation = serviceNavigation;
            _bluetoothUtil = bluetoothUtil;
            _serverData = new DevicesData();
        }

        private void GetServer()
        {
            if (Model.Server != null)
                return;
        }

        private async Task BluetoothPopupAsync()
        {
            var result = (IDevice)await Application.Current.MainPage
                .ShowPopupAsync(new BluetoothPopup(await _bluetoothUtil.SearchDevicesAsync()));

            if (result != null)
                Model.Bluetooth.ConnectedDevice = await _bluetoothUtil.SelectDeviceAsync(result);

            if (Model.Bluetooth.ConnectedDevice != null)
                Model.Bluetooth.BluetoothConnected = true;
        }

        private Task MovementAsync(string value)
        {
            throw new NotImplementedException();
        }

        private async Task ConfigureAsync()
            => await _serviceNavigation.NavigateToAsync<ConfigureJoystickViewModel>();

       
    }
}