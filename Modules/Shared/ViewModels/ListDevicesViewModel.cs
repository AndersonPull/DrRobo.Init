using System.Collections.ObjectModel;
using System.Windows.Input;
using Drrobo.Modules.Shared.Models;
using Drrobo.Modules.Shared.Services.Data;
using Drrobo.Modules.Shared.Services.Navigation;
using Drrobo.Modules.Shared.Services.Service;
using Drrobo.Utils.Bluetooth;

namespace Drrobo.Modules.Shared.ViewModels
{
	public class ListDevicesViewModel : BaseViewModel<ListDevicesModel>
    {
        public ICommand AddCommand => new Command(async () => await AddAsync());
        public ICommand DeleteCommand => new Command(async (value) => await DeleteAsync((DevicesModel)value));
        public ICommand UpdateCommand => new Command(async (value) => await UpdateAsync((DevicesModel)value));
        public ICommand GetDevicesCommand => new Command(async() => await GetDevicesAsync());

        INavigationService _serviceNavigation;
        IBluetoothUtil _bluetoothUtil;
        IUniversalService _universalService;

        DevicesData _deviceData;
        public ListDevicesViewModel(
            INavigationService serviceNavigation,
            IBluetoothUtil bluetoothUtil,
            IUniversalService universalService
        )
        {
            _serviceNavigation = serviceNavigation;
            _bluetoothUtil = bluetoothUtil;
            _universalService = universalService;

            _deviceData = new DevicesData();
        }

        private async Task GetDevicesAsync()
        {
            Model.DevicesList = new ObservableCollection<DevicesModel>();
            foreach (var item in _deviceData.GetAll())
            {
                if (item.IsBluetooth)
                {
                    var response = await _bluetoothUtil.ConnectDeviceAsync(item.GuidBluetooth);
                    if (response != null)
                        item.Isconnected = true;
                }
                else
                {
                    var response = await _universalService.HealthCheckAsync(item.URL);
                    if (response != null)
                        item.Isconnected = true;
                }

                Model.DevicesList.Add(item);
            }
        }

        private async Task AddAsync()
            => await _serviceNavigation.NavigateToAsync<ConfigureDevicesViewModel>();

        private async Task UpdateAsync(DevicesModel value)
            => await _serviceNavigation.NavigateToAsync<ConfigureDevicesViewModel>(value);

        private async Task DeleteAsync(DevicesModel value)
        {
            _deviceData.Delete(value);
            await GetDevicesAsync();
        }
    }
}