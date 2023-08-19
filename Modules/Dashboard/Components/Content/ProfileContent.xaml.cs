using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace Drrobo.Modules.Dashboard.Components.Content;

public partial class ProfileContent : ContentView
{
    public ProfileContent()
    {
        InitializeComponent();
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        if (width > height)
            ProfileStackLayout.Margin = new Thickness(120, 15, 55, 15);
        else
            ProfileStackLayout.Margin = new Thickness(15, 55, 15, 15);
    }
}