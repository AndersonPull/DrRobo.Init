using Drrobo.Modules.Shared.Services.Navigation;
using Drrobo.Modules.Shared.ViewModels;

namespace Drrobo;

public partial class App : Application
{
    public App()
	{
		InitializeComponent();

        InitNavigation();
    }

    public Task InitNavigation()
    {
        var navigationService = LocatorViewModel.Instance.Resolve<INavigationService>();
        return navigationService.InitializeAsync();
    }
}