using Drrobo.Modules.Shared.Models;
using Drrobo.Utils;

namespace Drrobo.Modules.Shared.Views;

public partial class ConfigureDevicesView : ContentPage
{
	public ConfigureDevicesView()
	{
		InitializeComponent();

        NameEntry.TextChanged += OnEntryTextChanged;
    }

    private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(NameEntry.Text))
        {
            SaveButton.IsEnabled = true;
            SaveButton.Style = Util.GetResource<Style>("PrimaryButton");

            UpdateButton.IsEnabled = true;
            UpdateButton.Style = Util.GetResource<Style>("PrimaryButton");
        }
        else
        {
            SaveButton.IsEnabled = false;
            SaveButton.Style = Util.GetResource<Style>("DisableButton");

            UpdateButton.IsEnabled = false;
            UpdateButton.Style = Util.GetResource<Style>("DisableButton");
        }
    }
}