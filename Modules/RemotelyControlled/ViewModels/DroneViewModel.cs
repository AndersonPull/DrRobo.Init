using System;
using Drrobo.Modules.Shared.ViewModels;
using Drrobo.Modules.Shared.Services.Navigation;
using Drrobo.Modules.RemotelyControlled.Models;

namespace Drrobo.Modules.RemotelyControlled.ViewModels
{
	public class DroneViewModel : BaseViewModel<DroneModel>
    {
        INavigationService _serviceNavigation;
        public DroneViewModel(INavigationService serviceNavigation)
        {
            _serviceNavigation = serviceNavigation;
        }
	}
}