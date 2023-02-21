using Drrobo.Modules.Shared.ViewModels;
using Drrobo.Modules.Shared.Services.Navigation;
using Plugin.BLE.Abstractions.Contracts;
using Drrobo.Modules.RemotelyControlled.Models;
using Plugin.BLE.Abstractions;
using System.Windows.Input;
using CommunityToolkit.Maui.Views;
using Drrobo.Modules.RemotelyControlled.Components.Popup;
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
            Model.Devices = await _bluetoothUtil.SearchDevicesAsync();

            var result = (IDevice)await App.Current.MainPage
                .ShowPopupAsync(new BluetoothPopup(Model.Devices));

            if(result != null)
                await DeviceSelectedAsync(result);
        }

        public async Task DeviceSelectedAsync(IDevice device)
        {
            Model.ConnectedDevice = device;

            var result = await App.Current.MainPage
                .DisplayAlert("AVISO", $"Deseja se conectar com {Model.ConnectedDevice.Name}", "Conectar", "Cancelar");

            if (!result)
                return;

            Model.BluetoothConnected = await _bluetoothUtil.SelectDeviceAsync(Model.ConnectedDevice);
        }

        public async Task MovementJumperAsync(string value)
        {
            if (Model.ConnectedDevice == null || Model.ConnectedDevice.State != DeviceState.Connected)
            {
                Model.BluetoothConnected = false;
                return;
            }

            await _bluetoothUtil.SendAsync(Model.ConnectedDevice, value);
        }
    }
}