namespace Drrobo.Modules.Shared.Components.PopUp;

public partial class ListItemsPopup : CommunityToolkit.Maui.Views.Popup
{
	public ListItemsPopup(List<string> values)
	{
		InitializeComponent();

        PopupListView.ItemsSource = values;

        if (values == null || !values.Any())
            EmptyLabel.IsVisible = true;
    }

    void ClosePopup(object sender, EventArgs args)
    {
        Close();
    }

    void ListViewItemSelected(object sender, SelectedItemChangedEventArgs args)
    {
        Close(args.SelectedItem);
    }
}
