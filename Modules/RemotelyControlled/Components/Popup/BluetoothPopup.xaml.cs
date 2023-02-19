using Plugin.BLE.Abstractions.Contracts;
using System.Collections.ObjectModel;

namespace Drrobo.Modules.RemotelyControlled.Components.Popup;

public partial class BluetoothPopup : CommunityToolkit.Maui.Views.Popup
{
    public BluetoothPopup(ObservableCollection<IDevice> devices)
    {
        InitializeComponent();

        DevicesListView.ItemsSource = devices;
    }

    void ClosePopup(object sender, EventArgs args)
    {
        Close();
    }

    void DevicesListViewItemSelected(object sender, SelectedItemChangedEventArgs args)
    {
        Close((IDevice)args.SelectedItem);
    }
}