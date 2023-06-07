using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Maui.Views;
using Drrobo.Modules.RemotelyControlled.Models;
using Drrobo.Modules.Shared.Components.PopUp;
using Drrobo.Modules.Shared.Models;
using Drrobo.Modules.Shared.Services.Data;
using Drrobo.Modules.Shared.Services.Navigation;
using Drrobo.Modules.Shared.ViewModels;

namespace Drrobo.Modules.RemotelyControlled.ViewModels
{
	public class ConfigureJoystickViewModel : BaseViewModel<ConfigureJoystickModel>
    {
        public ICommand SelectServerCommand => new Command(async () => await SelectServerAsync());
        public ICommand SelectCommunicationCommand => new Command((value) => SelectCommunicationAsync((bool)value));

        ServerData _serverData;
        public ConfigureJoystickViewModel()
        {
            _serverData = new ServerData();

            GetServers();
        }

        private async Task SelectServerAsync()
        {
            var response = await Application.Current.MainPage
                .ShowPopupAsync(new ListItemsPopup(Model.ServersNames));

            if (response == null)
                return;

            var server = (ServerModel)Model.ServerList
                .FirstOrDefault(server => server.Name.Equals(response));

            if (Model.Server != null)
                SetServe(Model.Server, false);

            Model.Server = server;
            SetServe(Model.Server, true);
        }

        private void GetServers()
        {
            Model.ServersNames = new List<string>();
            Model.ServerList = new ObservableCollection<ServerModel>();

            foreach (var item in _serverData.GetAll())
            {
                Model.ServerList.Add(item);
                Model.ServersNames.Add(item.Name);
            }

            Model.Server = Model.ServerList
                .FirstOrDefault(server => server.Connectedjoystick == true);

            if (Model.Server != null)
                return;

            AddServe();
            GetServers();
        }

        private void SetServe(ServerModel server, bool connected)
        {
            server.Connectedjoystick = connected;
            _serverData.Update(server);
        }

        private void SelectCommunicationAsync(bool value)
        {
            Model.Server.IsBluetooth = value;
            _serverData.Update(Model.Server);
        }

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