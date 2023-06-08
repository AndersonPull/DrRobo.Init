﻿using Drrobo.Modules.Shared.ViewModels;
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

namespace Drrobo.Modules.RemotelyControlled.ViewModels
{
	public class JoystickViewModel : BaseViewModel<JoystickModel>
    {
        public ICommand BluetoothPopupCommand => new Command(async () => await BluetoothPopupAsync());
        public ICommand MovementCommand => new Command(async (value) => await MovementAsync((string)value));
        public ICommand ConfigureCommand => new Command(async () => await ConfigureAsync());

        IBluetoothUtil _bluetoothUtil;
        INavigationService _serviceNavigation;
        ServerData _serverData;
        public JoystickViewModel(INavigationService serviceNavigation, IBluetoothUtil bluetoothUtil)
        {
            _serviceNavigation = serviceNavigation;
            _bluetoothUtil = bluetoothUtil;
            _serverData = new ServerData();

            GetServer();
        }

        private void GetServer()
        {
            Model.Server = _serverData.GetByConnectedjoystick();

            if (Model.Server != null)
                return;

            AddServe();
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

        private void AddServe()
        {
            Model.Server = new ServerModel();
            Model.Server.Name = "Default_robo";
            Model.Server.Connectedjoystick = true;
            Model.Server.IsBluetooth = true;
            _serverData.Save(Model.Server);
        }
    }
}