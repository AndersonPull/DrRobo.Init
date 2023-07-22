using Drrobo.Modules.Dashboard.ViewModels;
using Drrobo.Modules.Dashboard.Views;
using Drrobo.Modules.Shared.ViewModels;
using Drrobo.Modules.RemotelyControlled.ViewModels;
using Drrobo.Modules.RemotelyControlled.Views;
using Drrobo.Modules.Shared.Views;

namespace Drrobo.Modules.Shared.Services.Navigation.Implementations
{
	public class NavigationService : INavigationService
    {
        protected readonly Dictionary<Type, Type> _mappings;

        protected Application CurrentApplication
        {
            get { return Application.Current; }
        }

        public NavigationService()
        {
            _mappings = new Dictionary<Type, Type>();
            CreateViewModelMappings();
        }

        public Task InitializeAsync()
            => NavigateToAsync<StartViewModel>();

        public Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel
            => InternalNavigateToAsync(typeof(TViewModel), null);

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel
            => InternalNavigateToAsync(typeof(TViewModel), parameter);

        public Task NavigateToAsync(Type viewModelType)
            => InternalNavigateToAsync(viewModelType, null);

        public Task NavigateToAsync(Type viewModelType, object parameter)
            => InternalNavigateToAsync(viewModelType, parameter);

        private static NavigationService instance = null;
        private static readonly object padlock = new object();

        public static NavigationService Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new NavigationService();

                    return instance;
                }
            }
        }

        public virtual async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreateAndBindPage(viewModelType, parameter);

            if (page is StartView)
                CurrentApplication.MainPage = new NavigationPage(page);
            else
            {
                var nav = CurrentApplication.MainPage as NavigationPage;
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await nav.PushAsync(page);
                });
            }

            await (page.BindingContext as BaseViewModel).InitializeAsync(parameter);
        }

        protected Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!_mappings.ContainsKey(viewModelType))
                throw new KeyNotFoundException($"não existe mapeamento para ${viewModelType} por isso a navegação não está funcionando, mapeie a view model no método CreatePageViewModelMappings");

            return _mappings[viewModelType];
        }

        public Page CreateAndBindPage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
                throw new Exception($"A view model {viewModelType} não esta mapeado para uma page");

            Page page = Activator.CreateInstance(pageType) as Page;
            BaseViewModel viewModel = LocatorViewModel.Instance.Resolve(viewModelType) as BaseViewModel;
            page.BindingContext = viewModel;
            return page;
        }

        public void CreateViewModelMappings()
        {
            DashboardMaps();
            RemotelyMaps();
            SharedMaps();
        }

        private void DashboardMaps()
        {
            _mappings.Add(typeof(StartViewModel), typeof(StartView));
        }

        private void RemotelyMaps()
        {
            _mappings.Add(typeof(JoystickViewModel), typeof(JoystickView));
            _mappings.Add(typeof(CameraMonitoringViewModel), typeof(CameraMonitoringView));
        }

        private void SharedMaps()
        {
            _mappings.Add(typeof(ListDevicesViewModel), typeof(ListDevicesView));
            _mappings.Add(typeof(ConfigureDevicesViewModel), typeof(ConfigureDevicesView));
        }
    }
}