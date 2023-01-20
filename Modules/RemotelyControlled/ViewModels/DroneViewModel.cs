using System;
using Drrobo.Modules.Shared.ViewModels;
using Drrobo.Modules.Shared.Services.Navigation;
using Drrobo.Modules.RemotelyControlled.Models;
using Drrobo.Utils.Constant;

namespace Drrobo.Modules.RemotelyControlled.ViewModels
{
    public class DroneViewModel : BaseViewModel<DroneModel>
    {
        INavigationService _serviceNavigation;
        public DroneViewModel(INavigationService serviceNavigation)
        {
            _serviceNavigation = serviceNavigation;

            MessagingCenter.Subscribe<string>(this, "WriteWifiDrone", async (text)
                => WriteWifiDroneAsync(text));
        }

        public async Task WriteWifiDroneAsync(string write)
        {
            try
            {
                switch (write)
                {
                    case "U":
                        Constants.BaseServeLocal.Up().GetAwaiter().GetResult();
                        break;
                    case "D":
                        Constants.BaseServeLocal.Down().GetAwaiter().GetResult();
                        break;
                    case "X":
                        Constants.BaseServeLocal.Front().GetAwaiter().GetResult();
                        break;
                    case "B":
                        Constants.BaseServeLocal.Back().GetAwaiter().GetResult();
                        break;
                    case "L":
                        Constants.BaseServeLocal.Left().GetAwaiter().GetResult();
                        break;
                    case "R":
                        Constants.BaseServeLocal.Right().GetAwaiter().GetResult();
                        break;
                    case "S":
                        Constants.BaseServeLocal.Stop().GetAwaiter().GetResult();
                        break;
                }
            }
            catch(Exception)
            {
                await App.Current.MainPage.DisplayAlert("AVISO", "Drone desconectado.", "OK");
            }
        }
    }
}