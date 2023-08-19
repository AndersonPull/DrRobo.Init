using Drrobo.Modules.Dashboard.ViewModels;
using Drrobo.Modules.Shared.Services.Device;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace Drrobo.Modules.Dashboard.Views;

public partial class StartView : ContentPage
{
    private StartViewModel ViewModel => (StartViewModel)BindingContext;

    public StartView()
	{
		InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (ViewModel.GetDevicesConnectCommand.CanExecute(null))
            ViewModel.GetDevicesConnectCommand.Execute(null);
    }

    public ContentView GetContent() => this.ContentBody.Content as ContentView;

    public async Task SetContent(ContentView content)
    {
        await Task.Delay(1);
        ContentBody.Content = content;
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        SetSafeArea();
    }

    private void SetSafeArea()
    {
        DeviceSafeInsetsService area = new DeviceSafeInsetsService();
        double topArea = area.GetSafeAreaTop();
        double bottomArea = area.GetSafeAreaBottom();
        double rightArea = area.GetSafeAreaRight();
        double leftArea = area.GetSafeAreaLeft();

        var safeInsets = On<iOS>().SafeAreaInsets();
        safeInsets.Left = -leftArea;
        safeInsets.Top = -topArea;
        safeInsets.Right = -rightArea;
        safeInsets.Bottom = -bottomArea;
        Padding = safeInsets;
    }
}