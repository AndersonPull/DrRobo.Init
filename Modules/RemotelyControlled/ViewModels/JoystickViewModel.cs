﻿using Drrobo.Modules.Shared.ViewModels;
using Drrobo.Modules.Shared.Services.Navigation;
using Drrobo.Modules.RemotelyControlled.Models;
using System.Windows.Input;
using CommunityToolkit.Maui.Views;
using Drrobo.Modules.Shared.Components.PopUp;
using Drrobo.Utils.Bluetooth;
using Drrobo.Modules.Shared.Services.Data;
using Drrobo.Modules.Shared.Models;
using System.Collections.ObjectModel;
using Drrobo.Modules.Shared.ComponentModels;

namespace Drrobo.Modules.RemotelyControlled.ViewModels
{
	public class JoystickViewModel : BaseViewModel<JoystickModel>
    {
        public ICommand MovementCommand => new Command(async (value) => await MovementAsync((string)value));
        public ICommand DevicesPopupCommand => new Command(async () => await DevicesPopupAsync());

        INavigationService _serviceNavigation;
        IBluetoothUtil _bluetoothUtil;

        DevicesData _deviceData;
        public JoystickViewModel
        (
            INavigationService serviceNavigation,
            IBluetoothUtil bluetoothUtil
        )
        {
            _serviceNavigation = serviceNavigation;
            _bluetoothUtil = bluetoothUtil;

            _deviceData = new DevicesData();
        }

        public override async Task InitializeAsync(object navigationData)
        {
            GetDevices();
            Model.Device = Model.DevicesList
                .FirstOrDefault(x => x.IsSelected == true) ?? Model.DevicesList.FirstOrDefault();

            if (Model.Device.IsBluetooth && Model.Device.GuidBluetooth != null)
                Model.Bluetooth.ConnectedDevice = await _bluetoothUtil.ConnectDeviceAsync(Model.Device.GuidBluetooth);

            Model.HaveCam = Model.Device.HaveCamera;
            await base.InitializeAsync(navigationData);
        }

        private void GetDevices()
        {
            Model.DevicesList = new ObservableCollection<DevicesModel>();
            foreach (var item in _deviceData.GetAll())
                Model.DevicesList.Add(item);
        }

        private async Task DevicesPopupAsync()
        {
            var items = new List<ListItemsComponentModel>();

            foreach (var device in Model.DevicesList)
            {
                var item = new ListItemsComponentModel()
                {
                    Id = device.Id,
                    Image = device.Image,
                    Name = device.Name
                };

                items.Add(item);
            }

            var deviceSelected = (ListItemsComponentModel)await Application.Current.MainPage
                .ShowPopupAsync(new ListItemsPopup(items));

            if (deviceSelected == null)
                return;

            Model.Device.IsSelected = false;
            _deviceData.Update(Model.Device);

            Model.Device = _deviceData.GetById(deviceSelected.Id);
            Model.Device.IsSelected = true;
            _deviceData.Update(Model.Device);

            Model.WebViewCam = false;
            Model.HaveCam = Model.Device.HaveCamera;

            if (Model.Device.IsBluetooth && Model.Device.GuidBluetooth != null)
                Model.Bluetooth.ConnectedDevice = await _bluetoothUtil.ConnectDeviceAsync(Model.Device.GuidBluetooth);
        }

        private async Task MovementAsync(string value)
        {
            if (Model.Device.IsBluetooth)
                await CommunicationBLE(value, Model.Bluetooth.ConnectedDevice, _bluetoothUtil);
            //else
                //await CommunicationWifi(value, Model.Device.URL, _universalService);
        }
    }
}