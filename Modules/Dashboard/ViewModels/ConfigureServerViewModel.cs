using System;
using Drrobo.Modules.Shared.Models;
using Drrobo.Modules.Shared.Services.Navigation;
using Drrobo.Modules.Shared.ViewModels;

namespace Drrobo.Modules.Dashboard.ViewModels
{
	public class ConfigureServerViewModel : BaseViewModel<BaseModel>
    {
        INavigationService _serviceNavigation;

        public ConfigureServerViewModel(INavigationService serviceNavigation)
        {
            _serviceNavigation = serviceNavigation;
        }
    }
}