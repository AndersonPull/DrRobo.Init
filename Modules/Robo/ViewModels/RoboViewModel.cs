using Drrobo.Modules.Robo.Models;
using Drrobo.Modules.Shared.Services.Navigation;
using Drrobo.Modules.Shared.ViewModels;

namespace Drrobo.Modules.Robo.ViewModels
{
    class RoboViewModel : BaseViewModel<RoboModel>
    {
        INavigationService _serviceNavigation;
        public RoboViewModel(INavigationService serviceNavigation)
        {
            _serviceNavigation = serviceNavigation;
        }
    }
}