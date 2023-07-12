using System.Collections.ObjectModel;
using System.Windows.Input;
using Drrobo.Modules.Shared.Models;
using Drrobo.Modules.Shared.Services.Data;
using Drrobo.Modules.Shared.Services.Navigation;

namespace Drrobo.Modules.Shared.ViewModels
{
	public class ListDevicesViewModel : BaseViewModel<ListDevicesModel>
    {
        public ICommand AddCommand => new Command(async () => await AddAsync());
        public ICommand DeleteCommand => new Command((value) => DeleteAsync((DevicesModel)value));
        public ICommand UpdateCommand => new Command(async (value) => await UpdateAsync((DevicesModel)value));
        public ICommand GetDevicesCommand => new Command(() => GetDevices());

        INavigationService _serviceNavigation;
        DevicesData _deviceData;
        public ListDevicesViewModel(INavigationService serviceNavigation)
        {
            _serviceNavigation = serviceNavigation;
            _deviceData = new DevicesData();
        }

        public override Task InitializeAsync(object navigationData)
        {
            GetDevices();
            return base.InitializeAsync(navigationData);
        }

        private void GetDevices()
        {
            Model.DevicesList = new ObservableCollection<DevicesModel>();
            foreach (var item in _deviceData.GetAll())
                Model.DevicesList.Add(item);
        }

        private async Task AddAsync()
            => await _serviceNavigation.NavigateToAsync<ConfigureDevicesViewModel>();

        private async Task UpdateAsync(DevicesModel value)
            => await _serviceNavigation.NavigateToAsync<ConfigureDevicesViewModel>(value);

        private void DeleteAsync(DevicesModel value)
        {
            _deviceData.Delete(value);
            GetDevices();
        }
    }
}