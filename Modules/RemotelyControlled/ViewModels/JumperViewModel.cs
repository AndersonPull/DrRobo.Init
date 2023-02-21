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
    public class JumperViewModel : BaseViewModel<JumperModel>
    {
        public ICommand BluetoothPopupCommand => new Command(async () => await BluetoothPopupAsync());
        public ICommand MovementJumperCommand => new Command(async (value) => await MovementJumperAsync((string)value));

        IBluetoothUtil _bluetoothUtil; 
        INavigationService _serviceNavigation;

        public JumperViewModel(INavigationService serviceNavigation, IBluetoothUtil bluetoothUtil)
        {
            _serviceNavigation = serviceNavigation;
            _bluetoothUtil = bluetoothUtil;
        }

        private async Task BluetoothPopupAsync()
        {
            var result = (IDevice)await App.Current.MainPage
                .ShowPopupAsync(new BluetoothPopup(await _bluetoothUtil.SearchDevicesAsync()));

            if(result != null)
                Model.Bluetooth.BluetoothConnected = await _bluetoothUtil.SelectDeviceAsync(result);
        }

        public async Task MovementJumperAsync(string value)
            =>  Model.Bluetooth.BluetoothConnected = await _bluetoothUtil
            .SendAsync(Model.Bluetooth.ConnectedDevice, value);
    }
}