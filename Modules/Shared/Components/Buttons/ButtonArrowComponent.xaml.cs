using System.Windows.Input;

namespace Drrobo.Modules.Shared.Components.Buttons;

public partial class ButtonArrowComponent : ContentView
{
    public static readonly BindableProperty TextComponentProperty = BindableProperty.Create(nameof(TextComponent), typeof(string), typeof(ButtonArrowComponent));
    public static readonly BindableProperty SubTextComponentProperty = BindableProperty.Create(nameof(SubTextComponent), typeof(string), typeof(ButtonArrowComponent));
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ButtonArrowComponent));
    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(ButtonArrowComponent));

    public ButtonArrowComponent()
    {
        InitializeComponent();
    }

    public string TextComponent
    {
        get => (string)GetValue(TextComponentProperty);
        set => SetValue(TextComponentProperty, value);
    }

    public string SubTextComponent
    {
        get => (string)GetValue(SubTextComponentProperty);
        set => SetValue(SubTextComponentProperty, value);
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
