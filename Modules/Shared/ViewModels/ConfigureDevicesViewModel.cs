using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Maui.Views;
using Drrobo.Modules.Shared.Components.PopUp;
using Drrobo.Modules.Shared.Models;
using Drrobo.Modules.Shared.Services.Data;

namespace Drrobo.Modules.Shared.ViewModels
{
	public class ConfigureDevicesViewModel : BaseViewModel<ConfigureDevicesModel>
    {
        public ICommand AddCommand => new Command(async () => await AddAsync());
        public ICommand DeleteCommand => new Command((value) => DeleteAsync((DevicesModel)value));
        public ICommand UpdateCommand => new Command(async (value) => await UpdateAsync((DevicesModel)value));

		DevicesData _deviceData;
        public ConfigureDevicesViewModel()
        {
            _deviceData = new DevicesData();

            GetDevices();
        }

        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData is DevicesModel)
            {

            }

            return base.InitializeAsync(navigationData);
        }

        private void GetDevices()
        {
            Model.DevicesList = new ObservableCollection<DevicesModel>();

            foreach (var item in _deviceData.GetAll())
                Model.DevicesList.Add(item);
        }

        private async Task AddAsync()
        {
            var response = (List<string>)await Application.Current.MainPage.ShowPopupAsync(new AddItemPopup());
            if (response == null)
                return;

            _deviceData.Save(new DevicesModel
            {
                Name = response.FirstOrDefault(),
                URL = response.LastOrDefault(),
            });

            GetDevices();
        }

        private void DeleteAsync(DevicesModel value)
        {
            _deviceData.Delete(value);
            GetDevices();
        }

        private async Task UpdateAsync(DevicesModel value)
        {
            var response = (List<string>)await Application.Current.MainPage.ShowPopupAsync(new AddItemPopup(value));
            if (response == null)
                return;

            value.Name = response.FirstOrDefault();
            value.URL = response.LastOrDefault();

            _deviceData.Update(value);
            GetDevices();
        }
    }
}

