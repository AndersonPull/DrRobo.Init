using Plugin.BLE.Abstractions.Contracts;
using System.Collections.ObjectModel;
using Mopups.Pages;
using Mopups.Services;

namespace Drrobo.Modules.RemotelyControlled.Components.Popup;

public partial class BluetoothPopup : PopupPage
{
    public BluetoothPopup(ObservableCollection<IDevice> devices)
    {
        InitializeComponent();

        DevicesListView.ItemsSource = devices;
    }

    void Button_Clicked(System.Object sender, System.EventArgs e)
    {
        MopupService.Instance.PopAsync();
    }

    void PopupPage_BackgroundClicked(System.Object sender, System.EventArgs e)
    {
        MopupService.Instance.PopAsync();
    }

    void DevicesListView_ItemSelected(System.Object sender, Microsoft.Maui.Controls.SelectedItemChangedEventArgs e)
    {
        MessagingCenter.Send<IDevice>((IDevice)e.SelectedItem, "DeviceSelectedBluetooth");

        MopupService.Instance.PopAsync();
    }
}