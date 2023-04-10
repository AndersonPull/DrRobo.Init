using CommunityToolkit.Maui.Views;

namespace Drrobo.Modules.Shared.Components.PopUp;

public partial class AddItemPopup : Popup
{
	public AddItemPopup()
	{
		InitializeComponent();
		NameEntry.Focus();
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