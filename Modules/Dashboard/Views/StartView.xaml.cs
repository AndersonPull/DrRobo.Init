using Drrobo.Modules.Shared.Services.Device;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace Drrobo.Modules.Dashboard.Views;

public partial class StartView : ContentPage
{
    public StartView()
	{
		InitializeComponent();
    }

    public ContentView GetContent() => this.ContentBody.Content as ContentView;

    public async Task SetContent(ContentView content)
    {
        await Task.Delay(1);
        ContentBody.Content = content;
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        if (DeviceInfo.Idiom != DeviceIdiom.Desktop)
        {
            if (width > height)
            {
                LeftBar.IsVisible = true;
                BottomBar.IsVisible = false;

                if (DeviceInfo.Platform == DevicePlatform.iOS)
                {
                    SetSafeArea();
                    ContentBody.Margin = new Thickness(-50, 0, 0, 0);
                    LeftBar.Margin = new Thickness(40, 0, -50, 0);
                }
            }
            else
            {
                LeftBar.IsVisible = false;
                BottomBar.IsVisible = true;

                if (DeviceInfo.Platform == DevicePlatform.iOS)
                {
                    SetSafeArea();
                    ContentBody.Margin = new Thickness(0, 0, 0, -20);
                }

            }
        }
        else
        {
            if (DeviceInfo.Platform == DevicePlatform.MacCatalyst)
            {
                ContentBody.Margin = new Thickness(-110, 0, 0, 0);
                LeftBar.Margin = new Thickness(50, 0, 0, 0);
            }
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