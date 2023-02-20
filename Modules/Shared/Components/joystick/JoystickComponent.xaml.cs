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

    void ImageButtonPressedLeft(object sender, EventArgs e)
    {
        ExecuteCommand("L");
    }

    void ImageButtonPressedUp(object sender, EventArgs e)
    {
        ExecuteCommand("U");
    }

    void ImageButtonPressedRight(object sender, EventArgs e)
    {
        ExecuteCommand("R");
    }

    void ImageButtonPressedDown(object sender, EventArgs e)
    {
        ExecuteCommand("D");
    }

    void ButtonReleased(object sender, EventArgs e)
    {
        ExecuteCommand("S");
    }

    void ButtonPressed(object sender, EventArgs e)
    {
        var button = (Button)sender;
        ExecuteCommand(button.Text);
    }

    void ExecuteCommand(string value)
    {
        if (this.Command != null)
            this.Command.Execute(value);
    }
}