using System.Windows.Input;
using CommunityToolkit.Maui.Views;
using Drrobo.Modules.Dashboard.Models;
using Drrobo.Modules.Shared.Components.PopUp;
using Drrobo.Modules.Shared.Models;
using Drrobo.Modules.Shared.Services.Navigation;
using Drrobo.Modules.Shared.ViewModels;

namespace Drrobo.Modules.Dashboard.ViewModels
{
	public class ConfigureServerViewModel : BaseViewModel<ConfigureServerModel>
    {
        public ICommand AddServerCommand => new Command(async () => await AddServerAsync());

        INavigationService _serviceNavigation;

        public ConfigureServerViewModel(INavigationService serviceNavigation)
        {
            _serviceNavigation = serviceNavigation;

            var List = new List<ServerModel>()
            {
                new ServerModel(){ Name= "Robo1", URL="https://teste"},
                 new ServerModel(){ Name= "Robo2", URL="https://teste"},
                  new ServerModel(){ Name= "Robo3", URL="https://teste"}
            };

            Model.ServerList = List;
        }

        private async Task AddServerAsync()
        {
            await Application.Current.MainPage.ShowPopupAsync(new AddItemPopup());
        }
    }
}