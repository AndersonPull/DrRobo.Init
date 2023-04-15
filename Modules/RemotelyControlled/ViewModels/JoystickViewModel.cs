using Drrobo.Modules.Shared.ViewModels;
using Drrobo.Modules.Shared.Services.Navigation;
using Plugin.BLE.Abstractions.Contracts;
using Drrobo.Modules.RemotelyControlled.Models;
using Plugin.BLE.Abstractions;
using System.Windows.Input;
using CommunityToolkit.Maui.Views;
using Drrobo.Modules.Shared.Components.PopUp;
using Drrobo.Utils.Bluetooth;

namespace Drrobo.Modules.RemotelyControlled.ViewModels
{
	public class JoystickViewModel : BaseViewModel<JumperModel>
    {
        public ICommand BluetoothPopupCommand => new Command(async () => await BluetoothPopupAsync());
        public ICommand MovementCommand => new Command(async (value) => await MovementAsync((string)value));

        IBluetoothUtil _bluetoothUtil;
        INavigationService _serviceNavigation;

        public JoystickViewModel(INavigationService serviceNavigation, IBluetoothUtil bluetoothUtil)
        {
            _serviceNavigation = serviceNavigation;
            _bluetoothUtil = bluetoothUtil;
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
    }
}
