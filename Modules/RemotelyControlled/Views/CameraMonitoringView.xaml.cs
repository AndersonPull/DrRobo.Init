using Drrobo.Modules.RemotelyControlled.ViewModels;
using Drrobo.Modules.Shared.Services.Device;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace Drrobo.Modules.RemotelyControlled.Views;

public partial class CameraMonitoringView : ContentPage
{
    private CameraMonitoringViewModel ViewModel => (CameraMonitoringViewModel)BindingContext;

    public CameraMonitoringView()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (ViewModel.GetDevicesCommand.CanExecute(null))
            ViewModel.GetDevicesCommand.Execute(null);
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        SetSafeArea();
        SetDeviceDisplay();
    }

    private void SetDeviceDisplay()
    {
        if (DeviceInfo.Idiom == DeviceIdiom.Desktop)
            return;

        switch (DeviceDisplay.Current.MainDisplayInfo.Orientation)
        {
            case DisplayOrientation.Landscape:
                StackLayoutCam.IsVisible = true;
                StackLayoutCamWarning.IsVisible = false;
                break;
            case DisplayOrientation.Portrait:
                StackLayoutCam.IsVisible = false;
                StackLayoutCamWarning.IsVisible = true;
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
        StackLayoutCameras.Margin = new Thickness(leftArea + 15, -150, rightArea + 15, bottomArea);
    }
}