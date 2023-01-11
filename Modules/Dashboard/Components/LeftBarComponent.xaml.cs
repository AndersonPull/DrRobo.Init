namespace Drrobo.Modules.Dashboard.Components;

public partial class LeftBarComponent : ContentView
{
	public LeftBarComponent()
	{
		InitializeComponent();
	}

    async void SetIcon(object sender, EventArgs args)
    {
        var stackLayout = (StackLayout)sender;
        var icon = stackLayout.Children.Where(x => x is Image).FirstOrDefault();

        if (icon.Equals(CmdIcon))
            await SetIcon(CmdIcon, CmdIconFull, args);
        else if(icon.Equals(DashboardIcon))
            await SetIcon(DashboardIcon, DashboardIconFull, args);
        else if(icon.Equals(ProfileIcon))
            await SetIcon(ProfileIcon, ProfileIconFull, args);
    }

    async Task SetIcon(Image icon, Image iconFull, EventArgs args)
    {
        CmdIcon.IsVisible = true;
        CmdIconFull.IsVisible = false;

        DashboardIcon.IsVisible = true;
        DashboardIconFull.IsVisible = false;

        ProfileIcon.IsVisible = true;
        ProfileIconFull.IsVisible = false;

        icon.IsVisible = false;
        iconFull.IsVisible = true;
    }
}