using System;
using Drrobo.Modules.RemotelyControlled.Models;
using Drrobo.Modules.Shared.Services.Navigation;
using Drrobo.Modules.Shared.ViewModels;

namespace Drrobo.Modules.RemotelyControlled.ViewModels
{
	public class ConfigureJoystickViewModel : BaseViewModel<ConfigureJoystickModel>
    {
        INavigationService _serviceNavigation;
        public ConfigureJoystickViewModel(INavigationService serviceNavigation)
        {
            _serviceNavigation = serviceNavigation;
        }
    }
}