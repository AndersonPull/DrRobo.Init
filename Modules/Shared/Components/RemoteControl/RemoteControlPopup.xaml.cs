using System.Windows.Input;
using CommunityToolkit.Maui.Views;

namespace Drrobo.Modules.Shared.Components.RemoteControl;

public partial class RemoteControlPopup : Popup
{
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(RemoteControlPopup));
    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(RemoteControlPopup));

    public RemoteControlPopup()
	{
		InitializeComponent();
	}

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }
}
