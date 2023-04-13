using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace Drrobo.Modules.Dashboard.Components.Content;

public partial class ProfileContent : ContentView
{
    public ProfileContent()
    {
        InitializeComponent();

        SetDeviceDisplay();
        DeviceDisplay.MainDisplayInfoChanged += DeviceDisplay_MainDisplayInfoChanged;
    }

    private void DeviceDisplay_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        => SetDeviceDisplay();
    
    private void SetDeviceDisplay()
    {
        if (DeviceInfo.Idiom == DeviceIdiom.Desktop)
            return;

        switch (DeviceDisplay.Current.MainDisplayInfo.Orientation)
        {
            case DisplayOrientation.Landscape:
                if (DeviceInfo.Platform == DevicePlatform.iOS)
                    ProfileStackLayout.Margin = new Thickness(47);
                break;
            case DisplayOrientation.Portrait:
                if (DeviceInfo.Platform == DevicePlatform.iOS)
                    ProfileStackLayout.Margin = new Thickness(15, 45, 15, 15);
                break;
        }
    }
}