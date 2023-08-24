namespace Drrobo.Modules.Dashboard.Components.Content;

public partial class CmdContent : ContentView
{
    public CmdContent()
	{
		InitializeComponent();
        SetDeviceDisplay();
        DeviceDisplay.MainDisplayInfoChanged += DeviceDisplay_MainDisplayInfoChanged;
    }

    void EntryCMD_Completed(object sender, EventArgs e)
    {
        EntryCMD.Focus();
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            if (DeviceInfo.Platform == DevicePlatform.Android)
                await ScrollTerminal.ScrollToAsync(0, StackLayoutTerminal.Height, false);
            else
            {
                await Task.Delay(10);
                await ScrollTerminal.ScrollToAsync(StackLayoutTerminal, ScrollToPosition.End, false);
            }
        });
    }

    private void DeviceDisplay_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
         => SetDeviceDisplay();

    private void SetDeviceDisplay()
    {
        if (DeviceInfo.Idiom == DeviceIdiom.Desktop)
            MainGrid.Margin = new Thickness(95, 25, 15, 15);

        switch (DeviceDisplay.Current.MainDisplayInfo.Orientation)
        {
            case DisplayOrientation.Landscape:
                MainGrid.Margin = new Thickness(95, 25, 15, 15);
                break;
            case DisplayOrientation.Portrait:
                if (DeviceInfo.Platform == DevicePlatform.iOS)
                    MainGrid.Margin = new Thickness(0, 35, 15, 15);
                break;
        }
    }
}