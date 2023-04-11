using CommunityToolkit.Maui.Views;
using Drrobo.Modules.Shared.Models;
using Drrobo.Utils;

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

        NameEntry.TextChanged += OnEntryTextChanged;
        URLEntry.TextChanged += OnEntryTextChanged;
    }

    private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(NameEntry.Text) &&
            Uri.TryCreate(URLEntry.Text, UriKind.Absolute, out Uri uriResult) &&
            (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
        {
            SaveButton.IsEnabled = true;
            SaveButton.Style = Util.GetResource<Style>("PrimaryButton");
        }
        else
        {
            SaveButton.IsEnabled = false;
            SaveButton.Style = Util.GetResource<Style>("DisableButton");
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