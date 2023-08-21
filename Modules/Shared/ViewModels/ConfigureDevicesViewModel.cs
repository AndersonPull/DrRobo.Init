using System.Windows.Input;
using CommunityToolkit.Maui.Views;
using Drrobo.Modules.Shared.ComponentModels;
using Drrobo.Modules.Shared.Components.PopUp;
using Drrobo.Modules.Shared.Enums;
using Drrobo.Modules.Shared.Models;
using Drrobo.Modules.Shared.Services.Data;
using Drrobo.Utils;
using Drrobo.Utils.Bluetooth;
using Drrobo.Utils.Translations;
using Plugin.BLE.Abstractions.Contracts;

namespace Drrobo.Modules.Shared.ViewModels
{
	public class ConfigureDevicesViewModel : BaseViewModel<ConfigureDevicesModel>
    {
        public ICommand SelectCommunicationCommand => new Command(async () => await SelectCommunicationAsync());
        public ICommand SelectTypeDeviceCommand => new Command(async () => await SelectTypeDevice());
        public ICommand AddCommand => new Command(async () => await AddAsync());
        public ICommand UpdateCommand => new Command(async (value) => await UpdateAsync());

        IBluetoothUtil _bluetoothUtil;
        DevicesData _deviceData;
        public ConfigureDevicesViewModel(IBluetoothUtil bluetoothUtil)
        {
            _bluetoothUtil = bluetoothUtil;
            _deviceData = new DevicesData();
        }

        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData is DevicesModel)
            {
                Model.Device = (DevicesModel)navigationData;
                Model.IsUpdate = true;
            }
            else
                Model.Device = new DevicesModel();

            if (string.IsNullOrEmpty(Model.Device.URL))
                Model.Device.URL = "http://";

            if (string.IsNullOrEmpty(Model.Device.URLCamera))
                Model.Device.URLCamera = "http://";

            if (string.IsNullOrEmpty(Model.Device.Image))
                Model.Device.Image = DeviceTypeEnum.Other.Value();

            if (Model.Device.Type == DeviceTypeEnum.Camera.Description())
                Model.IsCamera = true;
            else
                Model.IsCamera = false;

            return base.InitializeAsync(navigationData);
        }
        
        private async Task SelectCommunicationAsync()
        {
            if (Model.Device.IsBluetooth)
                await BluetoothPopupAsync();
        }

        private async Task SelectTypeDevice()
        {
            var items = new List<ListItemsComponentModel>();

            var deviceTypes = (DeviceTypeEnum[])Enum.GetValues(typeof(DeviceTypeEnum));
            foreach (DeviceTypeEnum deviceType in deviceTypes)
            {
                var item = new ListItemsComponentModel()
                {
                    Image = deviceType.Value(),
                    Name = deviceType.Description()
                };

                items.Add(item);
            }
            
            var typeDevice = (ListItemsComponentModel)await Application.Current.MainPage
                .ShowPopupAsync(new ListItemsPopup(items));

            if (typeDevice == null)
                return;

            Model.Device.Image = typeDevice.Image;
            Model.Device.Type = typeDevice.Name;

            if (Model.Device.Type == DeviceTypeEnum.Jumper.Description())
                Model.Device.Isjoystick = true;

            if (Model.Device.Type == DeviceTypeEnum.Camera.Description())
                Model.IsCamera = true;
            else
                Model.IsCamera = false;
        }

        private async Task AddAsync()
        {
            if (await ValidateDevice())
            {
                Model.Device.Name = Model.Device.Name.Replace(" ", "_").ToLower();
                _deviceData.Save(Model.Device);
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }
        private async Task UpdateAsync()
        {
            if (await ValidateDevice())
            {
                _deviceData.Update(Model.Device);
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }

        private async Task BluetoothPopupAsync()
        {
            var result = (IDevice)await Application.Current.MainPage
                .ShowPopupAsync(new BluetoothPopup(await _bluetoothUtil.SearchDevicesAsync()));

            if (result != null)
                Model.Device.GuidBluetooth = result.Id;
            else
                Model.Device.IsBluetooth = false;
        }

        private async Task<bool> ValidateDevice()
        {
            var response = true;
            var message = string.Empty;

            if (Model.IsCamera && !Util.IsValidUrl(Model.Device.URLCamera))
                message = AppResources.EnterCameraURL;

            if(Model.Device.HaveCamera && !Util.IsValidUrl(Model.Device.URLCamera))
                message = AppResources.EnterCameraURL;

            if (!Model.Device.IsBluetooth && !Util.IsValidUrl(Model.Device.URL))
                message = AppResources.EnterDeviceURL;

            if (_deviceData.ValidName(Model.Device.Name) && !Model.IsUpdate)
                message = AppResources.AlreadyExists;

            if (string.IsNullOrEmpty(Model.Device.Name))
                message = AppResources.DeviceName;

            if (!string.IsNullOrEmpty(message))
            {
                response = false;
                await Application.Current.MainPage.DisplayAlert("Alert", message, "OK");
            }

            return response;
        }
    }
}