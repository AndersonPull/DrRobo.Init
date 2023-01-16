using System.Windows.Input;
using Plugin.BLE.Abstractions.Contracts;

namespace Drrobo.Modules.Shared.Components.NavigationBar;

public partial class NavigationBarComponent : ContentView
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(NavigationBarComponent), string.Empty);
    public static readonly BindableProperty HasBluetoothProperty = BindableProperty.Create(nameof(HasBluetooth), typeof(bool), typeof(NavigationBarComponent),false);

    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(NavigationBarComponent));

    public NavigationBarComponent()
    {
        InitializeComponent();

        MessagingCenter.Subscribe<string>(this, "BluetoothConnected", async (value) =>
        {
            if (value == "true")
                ImageButtonBluetooth.Source = "bluetooth_on_icon.png";
            else
                ImageButtonBluetooth.Source = "bluetooth_icon.png";
        });
    }

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public bool HasBluetooth
    {
        get => (bool)GetValue(HasBluetoothProperty);
        set => SetValue(HasBluetoothProperty, value);
    }

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    void BackPage(object sender, EventArgs args)
    {
        Application.Current.MainPage.Navigation.PopAsync();
    }
}