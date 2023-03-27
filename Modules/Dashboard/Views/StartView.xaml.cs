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
                    SetSafeArea(0, -50, -50);
                    ContentBody.Margin = new Thickness(-50, 0, 0, -20);
                    LeftBar.Margin = new Thickness(50, 0, -60, 0);
                }
            }
            else
            {
                LeftBar.IsVisible = false;
                BottomBar.IsVisible = true;

                if (DeviceInfo.Platform == DevicePlatform.iOS)
                {
                    SetSafeArea(-50, 0);
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

    private void SetSafeArea(int top, int right, int left = 0)
    {
        var safeInsets = On<iOS>().SafeAreaInsets();
        safeInsets.Left = left;
        safeInsets.Top = top;
        safeInsets.Right = right;
        safeInsets.Bottom = -30;
        Padding = safeInsets;
    }
}