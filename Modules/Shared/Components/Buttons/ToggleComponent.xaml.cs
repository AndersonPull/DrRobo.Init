using Drrobo.Utils;

namespace Drrobo.Modules.Shared.Components.Buttons;

public partial class ToggleComponent : ContentView
{
    public static readonly BindableProperty FirstTextProperty = BindableProperty.Create(nameof(FirstText), typeof(string), typeof(ToggleComponent));
    public static readonly BindableProperty SecondTextProperty = BindableProperty.Create(nameof(SecondText), typeof(string), typeof(ToggleComponent));
    public static readonly BindableProperty SelectedProperty = BindableProperty.Create(nameof(Selected), typeof(bool), typeof(ToggleComponent));

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

    void PrimaryToggleClick(object sender, TappedEventArgs args)
    {
        if (Selected)
            return;

        Selected = true;
        SetToggle();
    }

    void SecondaryToggleClick(object sender, TappedEventArgs args)
    {
        if (!Selected)
            return;

        Selected = false;
        SetToggle();
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
}
