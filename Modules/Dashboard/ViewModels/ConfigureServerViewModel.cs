using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Maui.Views;
using Drrobo.Modules.Dashboard.Models;
using Drrobo.Modules.Shared.Components.PopUp;
using Drrobo.Modules.Shared.Models;
using Drrobo.Modules.Shared.Services.Data;
using Drrobo.Modules.Shared.Services.Navigation;
using Drrobo.Modules.Shared.ViewModels;

namespace Drrobo.Modules.Dashboard.ViewModels
{
	public class ConfigureServerViewModel : BaseViewModel<ConfigureServerModel>
    {
        public ICommand AddCommand => new Command(async () => await AddAsync());
        public ICommand DeleteCommand => new Command((value) => DeleteAsync((ServerModel)value));
        public ICommand UpdateCommand => new Command(async (value) => await UpdateAsync((ServerModel)value));

        INavigationService _serviceNavigation;
        ServerData _serverData;
        public ConfigureServerViewModel(INavigationService serviceNavigation)
        {
            _serviceNavigation = serviceNavigation;
            _serverData = new ServerData();

            GetServers();
        }

        private void GetServers()
        {
            Model.ServerList = new ObservableCollection<ServerModel>();

            foreach (var item in _serverData.GetAll())
                Model.ServerList.Add(item);
        }

        private async Task AddAsync()
            => await _serviceNavigation.NavigateToAsync<ConfigureDevicesViewModel>();
        
        private async Task UpdateAsync(ServerModel value)
            => await _serviceNavigation.NavigateToAsync<ConfigureDevicesViewModel>(value);

        private void DeleteAsync(ServerModel value)
        {
            _serverData.Delete(value);
            GetServers();
        }
    }
}