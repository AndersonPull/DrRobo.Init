﻿using System.Windows.Input;
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
        public ICommand DeleteCommand => new Command(async (value) => await DeleteAsync((ServerModel)value));

        ServerData _serverData;

        public ConfigureServerViewModel()
        {
            _serverData = new ServerData();

            GetServers();
        }

        private void GetServers()
        {
            Model.ServerList = new System.Collections.ObjectModel.ObservableCollection<ServerModel>();
            var collection = _serverData.GetAll();
            foreach (var item in collection)
            {
                Model.ServerList.Add(item);
            }
        }

        private async Task AddAsync()
        {
            var response = (List<string>)await Application.Current.MainPage.ShowPopupAsync(new AddItemPopup());

            if (response == null)
                return;

            var server = new ServerModel
            {
                Name = response.FirstOrDefault(),
                URL = response.LastOrDefault(),
            };

            _serverData.Save(server);
            Model.ServerList.Add(server);
        }

        private async Task DeleteAsync(ServerModel value)
        {
            _serverData.Delete(value);
            Model.ServerList.Remove(value);
        }
    }
}