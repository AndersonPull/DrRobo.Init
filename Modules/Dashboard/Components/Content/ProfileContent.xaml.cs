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
        {
            ProfileStackLayout.Margin = new Thickness(120, 15, 55, 15);
            return;
        }

        switch (DeviceDisplay.Current.MainDisplayInfo.Orientation)
        {
            case DisplayOrientation.Landscape:
                ProfileStackLayout.Margin = new Thickness(120, 15, 55, 15);
                break;
            case DisplayOrientation.Portrait:
                ProfileStackLayout.Margin = new Thickness(15, 55, 15, 15);
                break;
        }
    }
}