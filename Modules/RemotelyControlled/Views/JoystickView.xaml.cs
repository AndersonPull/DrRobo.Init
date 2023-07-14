using Drrobo.Modules.RemotelyControlled.ViewModels;
using Drrobo.Modules.Shared.Services.Device;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace Drrobo.Modules.RemotelyControlled.Views;

public partial class JoystickView : ContentPage
{
    private JoystickViewModel ViewModel => (JoystickViewModel)BindingContext;

    public JoystickView()
	{
		InitializeComponent();
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        SetSafeArea();
        SetDeviceDisplay();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        //if (ViewModel.GetServerCommand.CanExecute(null))
        //    ViewModel.GetServerCommand.Execute(null);
    }

    private void SetDeviceDisplay()
    {
        if (DeviceInfo.Idiom == DeviceIdiom.Desktop)
            return;

        switch (DeviceDisplay.Current.MainDisplayInfo.Orientation)
        {
            case DisplayOrientation.Landscape:
                Joystick.IsVisible = true;
                JoystickWarning.IsVisible = false;
                break;
            case DisplayOrientation.Portrait:
                Joystick.IsVisible = false;
                JoystickWarning.IsVisible = true;
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

        NavigationComponent.Margin = new Thickness(leftArea, topArea, rightArea, bottomArea);
        JoystickComponent.Margin = new Thickness(leftArea, topArea, rightArea, bottomArea);
    }

    void OpenCam(object sender, EventArgs args)
    {
        WebViewCam.IsVisible = true;
        ImageButtonCam.IsVisible = false;
    }
}
