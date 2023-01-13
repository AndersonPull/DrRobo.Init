namespace Drrobo.Modules.Dashboard.Components.Content;

public partial class DashBoardContent : ContentView
{
    public static readonly BindableProperty NewWidthFrameProperty = BindableProperty.Create(nameof(NewWidthFrame), typeof(int), typeof(DashBoardContent), 320);
    public static readonly BindableProperty NewHeightFrameProperty = BindableProperty.Create(nameof(NewHeightFrame), typeof(int), typeof(DashBoardContent), 300);

    public DashBoardContent()
	{
		InitializeComponent();
	}

    public int NewWidthFrame
    {
        get => (int)GetValue(NewWidthFrameProperty);
        set => SetValue(NewWidthFrameProperty, value);
    }

    public int NewHeightFrame
    {
        get => (int)GetValue(NewHeightFrameProperty);
        set => SetValue(NewHeightFrameProperty, value);
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        if (width > 500)
        {
            NewWidthFrame = 410;
            NewHeightFrame = 250;
        }
        else
        {
            NewWidthFrame = 320;
            NewHeightFrame = 300;
        }
    }
}