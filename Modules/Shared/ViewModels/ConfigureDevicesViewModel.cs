using System.Windows.Input;
using CommunityToolkit.Maui.Views;
using Drrobo.Modules.Shared.ComponentModels;
using Drrobo.Modules.Shared.Components.PopUp;
using Drrobo.Modules.Shared.Enums;
using Drrobo.Modules.Shared.Models;
using Drrobo.Modules.Shared.Services.Data;
using Drrobo.Utils;
using Drrobo.Utils.Bluetooth;
using Plugin.BLE.Abstractions.Contracts;

namespace Drrobo.Modules.Shared.ViewModels
{
	public class ConfigureDevicesViewModel : BaseViewModel<ConfigureDevicesModel>
    {
        public ICommand SelectCommunicationCommand => new Command(async () => await SelectCommunicationAsync());
        public ICommand SelectTypeDeviceCommand => new Command(async () => await SelectTypeDevice());
        public ICommand AddCommand => new Command(async () => await AddAsync());
        public ICommand UpdateCommand => new Command(async (value) => await UpdateAsync((DevicesModel)value));

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
                Model.Device.URL = "https://";

            if (string.IsNullOrEmpty(Model.Device.URLCamera))
                Model.Device.URLCamera = "https://";

            if (string.IsNullOrEmpty(Model.Device.Image))
                Model.Device.Image = DeviceTypeEnum.Other.Value();

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
        }

        private async Task AddAsync()
        {
            _deviceData.Save(Model.Device);
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private async Task UpdateAsync(DevicesModel value)
        {
            _deviceData.Update(Model.Device);
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private async Task BluetoothPopupAsync()
        {
            var result = (IDevice)await Application.Current.MainPage
                .ShowPopupAsync(new BluetoothPopup(await _bluetoothUtil.SearchDevicesAsync()));

            if (result != null)
                Model.Device.GuidBluetooth = result.Id;
        }
    }
}