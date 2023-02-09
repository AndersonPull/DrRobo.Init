namespace Drrobo.Modules.Dashboard.Components.Content;

public partial class CmdContent : ContentView
{
    public CmdContent()
	{
		InitializeComponent();
    }

    void EntryCMD_Unfocused(object sender, FocusEventArgs e)
    {
        EntryCMD.Focus();
    }
}