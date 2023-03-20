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
        if(DeviceInfo.Idiom != DeviceIdiom.Desktop)
            if(width > height)
            {
                LeftBar.IsVisible = true;
                BottomBar.IsVisible = false;

                var safeInsets = On<iOS>().SafeAreaInsets();
                safeInsets.Top = 0;
                safeInsets.Right = -50;

                Padding = safeInsets;
            }
            else
            {
                LeftBar.IsVisible = false;
                BottomBar.IsVisible = true;

                var safeInsets = On<iOS>().SafeAreaInsets();
                safeInsets.Top = -50;
                safeInsets.Right = 0;

                Padding = safeInsets;
            }
    }
}