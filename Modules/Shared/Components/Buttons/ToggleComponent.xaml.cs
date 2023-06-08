using System.Windows.Input;
using Drrobo.Utils;

namespace Drrobo.Modules.Shared.Components.Buttons;

public partial class ToggleComponent : ContentView
{
    public static readonly BindableProperty FirstTextProperty = BindableProperty.Create(nameof(FirstText), typeof(string), typeof(ToggleComponent));
    public static readonly BindableProperty SecondTextProperty = BindableProperty.Create(nameof(SecondText), typeof(string), typeof(ToggleComponent));
    public static readonly BindableProperty SelectedProperty = BindableProperty.Create(nameof(Selected), typeof(bool), typeof(ToggleComponent),false);
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ToggleComponent));
    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(ToggleComponent));

    public ToggleComponent()
	{
		InitializeComponent();
        SetToggle();
    }

    public string FirstText
    {
        get => (string)GetValue(FirstTextProperty);
        set => SetValue(FirstTextProperty, value);
    }

    public string SecondText
    {
        get => (string)GetValue(SecondTextProperty);
        set => SetValue(SecondTextProperty, value);
    }

    public bool Selected
    {
        get => (bool)GetValue(SelectedProperty);
        set => SetValue(SelectedProperty, value);
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

    protected override void OnPropertyChanged(string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
        if (nameof(Selected).Equals(propertyName))
            SetToggle();
    }

    void PrimaryToggleClick(object sender, TappedEventArgs args)
    {
        if (Selected)
            return;

        Selected = true;
        ExecuteCommand();
    }

    void SecondaryToggleClick(object sender, TappedEventArgs args)
    {
        if (!Selected)
            return;

        Selected = false;
        ExecuteCommand();
    }

    private void SetToggle()
    {
        if (Selected)
        {
            PrimaryFrame.BackgroundColor = Util.GetResource<Color>("Primary");
            PrimaryLabel.Style = Util.GetResource<Style>("Label14BlackBold");

            SecondaryFrame.BackgroundColor = Util.GetResource<Color>("SecondaryBackground");
            SecondaryLabel.Style = Util.GetResource<Style>("Label14WhiteBold");
        }
        else
        {
            SecondaryFrame.BackgroundColor = Util.GetResource<Color>("Primary");
            SecondaryLabel.Style = Util.GetResource<Style>("Label14BlackBold");

            PrimaryFrame.BackgroundColor = Util.GetResource<Color>("SecondaryBackground");
            PrimaryLabel.Style = Util.GetResource<Style>("Label14WhiteBold");

        }
    }

    void ExecuteCommand()
    {
        if (this.Command != null)
            this.Command.Execute(Selected);
    }
}
