using System;
using System.Windows.Input;
using CommunityToolkit.Maui.Views;
using Drrobo.Modules.Shared.Components.PopUp;
using Drrobo.Modules.Shared.Models;
using Drrobo.Modules.Shared.Services.Navigation;
using Drrobo.Modules.Shared.ViewModels;

namespace Drrobo.Modules.Dashboard.ViewModels
{
	public class ConfigureServerViewModel : BaseViewModel<BaseModel>
    {
        public ICommand AddServerCommand => new Command(async () => await AddServerAsync());

        INavigationService _serviceNavigation;

        public ConfigureServerViewModel(INavigationService serviceNavigation)
        {
            _serviceNavigation = serviceNavigation;
        }

        private async Task AddServerAsync()
        {
            await Application.Current.MainPage.ShowPopupAsync(new AddItemPopup());
        }
    }
}