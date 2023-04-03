using Drrobo.Modules.Dashboard.Enums;

namespace Drrobo.Modules.Dashboard.Components.Popup;

public partial class LanguagesPopup : CommunityToolkit.Maui.Views.Popup
{
	public LanguagesPopup()
    {
		InitializeComponent();
        LanguagesListView.ItemsSource = Enum.GetValues(typeof(LanguagesEnum));
    }

    void ClosePopup(object sender, EventArgs args)
    {
        Close();
    }

    void LanguagesListViewItemSelected(object sender, SelectedItemChangedEventArgs args)
    {
        Close((LanguagesEnum)args.SelectedItem);
    }
}