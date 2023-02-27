using System;
using Drrobo.Modules.Shared.ViewModels;
using Drrobo.Modules.Shared.Services.Navigation;
using Drrobo.Modules.RemotelyControlled.Models;
using Drrobo.Utils.Constant;
using System.Windows.Input;
using Drrobo.Modules.Shared.Models;

namespace Drrobo.Modules.RemotelyControlled.ViewModels
{
    public class DroneViewModel : BaseViewModel<BaseModel>
    {
        public ICommand MovementDroneCommand => new Command(async (value) => await MovementDroneAsync((string)value));

        INavigationService _serviceNavigation;
        public DroneViewModel(INavigationService serviceNavigation)
        {
            _serviceNavigation = serviceNavigation;
        }

        public async Task MovementDroneAsync(string value)
        {
            try
            {
                Constants.BaseServeLocal.Movement(value).GetAwaiter().GetResult();
            }
            catch(Exception)
            {
                await Application.Current.MainPage.DisplayAlert("AVISO", "Drone desconectado.", "OK");
            }
        }
    }
}