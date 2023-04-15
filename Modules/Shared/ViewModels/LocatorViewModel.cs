using System;
using Unity;
using Drrobo.Modules.Shared.Services.Navigation;
using Drrobo.Modules.Shared.Services.Navigation.Implementations;
using Drrobo.Modules.Dashboard.ViewModels;
using Drrobo.Modules.RemotelyControlled.ViewModels;
using Drrobo.Utils.Bluetooth;
using Drrobo.Utils.Bluetooth.Implementations;

namespace Drrobo.Modules.Shared.ViewModels
{
	public class LocatorViewModel
	{
        private readonly IUnityContainer _container;

        private static readonly LocatorViewModel _instance = new LocatorViewModel();

        public static LocatorViewModel Instance
        {
            get { return _instance; }
        }

        public LocatorViewModel()
        {
            _container = new UnityContainer();

            RegisterInterfaces();
            RegisterViewModels();
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }

        private void RegisterInterfaces()
        {
            _container.RegisterType<INavigationService, NavigationService>();
            _container.RegisterType<IBluetoothUtil, BluetoothUtil>();
        }

        private void RegisterViewModels()
        {
            _container.RegisterType<StartViewModel>();
            _container.RegisterType<JumperViewModel>();
            _container.RegisterType<DroneViewModel>();
            _container.RegisterType<JoystickViewModel>();
            _container.RegisterType<BaseViewModel>();
            _container.RegisterType<ConfigureServerViewModel>();
        }
    }
}