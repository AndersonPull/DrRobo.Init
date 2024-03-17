using Unity;
using Drrobo.Modules.Shared.Services.Navigation;
using Drrobo.Modules.Shared.Services.Navigation.Implementations;
using Drrobo.Modules.Dashboard.ViewModels;
using Drrobo.Modules.RemotelyControlled.ViewModels;
using Drrobo.Utils.Bluetooth;
using Drrobo.Utils.Bluetooth.Implementations;
using Drrobo.Modules.Shared.Services.Service;
using Drrobo.Modules.Shared.Services.Service.Implementations;
using Drrobo.Modules.Robo.ViewModels;
using CommunityToolkit.Maui.Media;

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
            _container.RegisterType<IUniversalService, UniversalService>();
            _container.RegisterType<ISpeechToText, SpeechToTextImplementation>();

        }

        private void RegisterViewModels()
        {
            _container.RegisterType<StartViewModel>();
            _container.RegisterType<JoystickViewModel>();
            _container.RegisterType<BaseViewModel>();
            _container.RegisterType<ListDevicesViewModel>();
            _container.RegisterType<ConfigureDevicesViewModel>();
            _container.RegisterType<CameraMonitoringViewModel>();
            _container.RegisterType<RoboViewModel>();
        }
    }
}