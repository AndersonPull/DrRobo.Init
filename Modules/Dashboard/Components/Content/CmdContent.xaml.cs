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
        Device.BeginInvokeOnMainThread(async () =>
        {
            // Update your children here, add more or remove.
            // todo

            if (Device.RuntimePlatform == Device.Android)
            {
                await ScrollTerminal.ScrollToAsync(0, StackLayoutTerminal.Height, false);
            }
            else
            {
                await Task.Delay(10); //UI will be updated by Xamarin
                await ScrollTerminal.ScrollToAsync(StackLayoutTerminal, ScrollToPosition.End, false);
            }

        });
    }
}