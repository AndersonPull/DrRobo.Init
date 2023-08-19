namespace Drrobo.Modules.Dashboard.Components.Content;

public partial class CmdContent : ContentView
{
    public CmdContent()
	{
		InitializeComponent();
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

    protected override void OnSizeAllocated(double width, double height)
    {
        if (width > height)
            MainGrid.Margin = new Thickness(95, 15, 15, 15);
        else
            MainGrid.Margin = new Thickness(0, 35, 15, 15);
    }
}