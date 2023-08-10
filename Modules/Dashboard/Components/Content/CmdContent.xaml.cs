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
}