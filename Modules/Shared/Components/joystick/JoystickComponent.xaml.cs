using System.Windows.Input;

namespace Drrobo.Modules.Shared.Components.joystick;

public partial class JoystickComponent : ContentView
{
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(JoystickComponent));
    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(JoystickComponent));

    public JoystickComponent()
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