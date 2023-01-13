using System.Windows.Input;

namespace Drrobo.Modules.Shared.Components.NavigationBar;

public partial class NavigationBarComponent : ContentView
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(NavigationBarComponent), string.Empty);
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(NavigationBarComponent));

    public NavigationBarComponent()
    {
        InitializeComponent();
    }

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
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