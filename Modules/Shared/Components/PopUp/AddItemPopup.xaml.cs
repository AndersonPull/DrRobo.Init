using CommunityToolkit.Maui.Views;
using Drrobo.Modules.Shared.Models;

namespace Drrobo.Modules.Shared.Components.PopUp;

public partial class AddItemPopup : Popup
{
	public AddItemPopup(ServerModel server = null)
	{
		InitializeComponent();
		NameEntry.Focus();

        if (server != null)
        {
            NameEntry.Text = server.Name;
            URLEntry.Text = server.URL;
        }
	}

    void ClosePopup(object sender, EventArgs args)
    {
        Close();
    }

    void SaveServer(object sender, EventArgs args)
    {
        List<string> response = new List<string>() { };
        response.Add(NameEntry.Text);
        response.Add(URLEntry.Text);

        Close(response);
    }
}