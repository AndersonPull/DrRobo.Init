namespace Drrobo.Modules.Dashboard.Components;

public partial class LeftBarComponent : ContentView
{
	public LeftBarComponent()
	{
		InitializeComponent();
	}

    private void SetIcon(object sender, EventArgs args)
    {
        var stackLayout = (StackLayout)sender;
        var icon = stackLayout.Children.Where(x => x is Image).FirstOrDefault();

        IconAlignment(CmdIcon, CmdIconFull);
        IconAlignment(DashboardIcon, DashboardIconFull);
        IconAlignment(ProfileIcon, ProfileIconFull);
        IconAlignment(ProfileIcon, ProfileIconFull);

        if (icon.Equals(CmdIcon))
            IconAlignment(CmdIconFull, CmdIcon);
        else if (icon.Equals(DashboardIcon))
            IconAlignment(DashboardIconFull, DashboardIcon);
        else if (icon.Equals(ProfileIcon))
            IconAlignment(ProfileIconFull, ProfileIcon);
    }

    private void IconAlignment(Image newIcon, Image oldIcon)
    {
        newIcon.IsVisible = true;
        newIcon.VerticalOptions = LayoutOptions.Center;
        newIcon.HorizontalOptions = LayoutOptions.Center;

        oldIcon.IsVisible = false;
        oldIcon.VerticalOptions = LayoutOptions.Start;
        oldIcon.HorizontalOptions = LayoutOptions.Start;
    }
}