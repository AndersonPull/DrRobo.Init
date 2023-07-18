using Drrobo.Modules.Shared.Services.Navigation;
using Drrobo.Modules.Shared.ViewModels;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;

namespace Drrobo;

public partial class App : Application
{
    public record City(string Name, double Population);

    public App()
	{
        InitializeComponent();
        InitNavigation();

        LiveCharts.Configure(config =>
                config
                .AddSkiaSharp()
                .AddDefaultMappers()
                .AddLightTheme());
    }

    public Task InitNavigation()
    {
        var navigationService = LocatorViewModel.Instance.Resolve<INavigationService>();
        return navigationService.InitializeAsync();
    }
}