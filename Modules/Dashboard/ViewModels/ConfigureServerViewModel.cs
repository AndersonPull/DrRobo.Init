using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Maui.Views;
using Drrobo.Modules.Dashboard.Models;
using Drrobo.Modules.Shared.Components.PopUp;
using Drrobo.Modules.Shared.Models;
using Drrobo.Modules.Shared.Services.Data;
using Drrobo.Modules.Shared.ViewModels;

namespace Drrobo.Modules.Dashboard.ViewModels
{
	public class ConfigureServerViewModel : BaseViewModel<ConfigureServerModel>
    {
        public ICommand AddCommand => new Command(async () => await AddAsync());
        public ICommand DeleteCommand => new Command((value) => DeleteAsync((ServerModel)value));
        public ICommand UpdateCommand => new Command(async (value) => await UpdateAsync((ServerModel)value));

        ServerData _serverData;

        public ConfigureServerViewModel()
        {
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
        {
            var response = (List<string>)await Application.Current.MainPage.ShowPopupAsync(new AddItemPopup());
            if (response == null)
                return;

            _serverData.Save(new ServerModel
            {
                Name = response.FirstOrDefault(),
                URL = response.LastOrDefault(),
            });

            GetServers();
        }

        private void DeleteAsync(ServerModel value)
        {
            _serverData.Delete(value);
            GetServers();
        }

        private async Task UpdateAsync(ServerModel value)
        {
            var response = (List<string>)await Application.Current.MainPage.ShowPopupAsync(new AddItemPopup(value));
            if (response == null)
                return;

            value.Name = response.FirstOrDefault();
            value.URL = response.LastOrDefault();

            _serverData.Update(value);
            GetServers();
        }
    }
}