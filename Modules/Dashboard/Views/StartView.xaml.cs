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

        if (DeviceInfo.Platform == DevicePlatform.Android)
        {
            SetSafeArea();
            SetBar();
        }
        DeviceDisplay.MainDisplayInfoChanged += DeviceDisplay_MainDisplayInfoChanged;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (ViewModel.GetDevicesConnectCommand.CanExecute(null))
            ViewModel.GetDevicesConnectCommand.Execute(null);
    }

    public ContentView GetContent() => ContentBody.Content as ContentView;

    public async Task SetContent(ContentView content)
    {
        await Task.Delay(1);
        ContentBody.Content = content;
    }
    private void DeviceDisplay_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
    {
        SetSafeArea();
        SetBar();
    }
    protected override void OnSizeAllocated(double width, double height)
    {
        if (DeviceInfo.Platform != DevicePlatform.Android)
        {
            SetSafeArea();
            SetBar();
        }
    }

    private void SetBar()
    {
        if (DeviceInfo.Idiom == DeviceIdiom.Desktop)
        {
            LeftBar.IsVisible = true;
            BottomBar.IsVisible = false;
            return;
        }

        switch (DeviceDisplay.Current.MainDisplayInfo.Orientation)
        {
            case DisplayOrientation.Landscape:
                LeftBar.IsVisible = true;
                BottomBar.IsVisible = false;
                break;
            case DisplayOrientation.Portrait:
                LeftBar.IsVisible = false;
                BottomBar.IsVisible = true;
                break;
        }
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