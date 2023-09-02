namespace Drrobo.Modules.Dashboard.Components.Content;

public partial class DashBoardContent : ContentView
{
    public static readonly BindableProperty NewWidthFrameProperty = BindableProperty.Create(nameof(NewWidthFrame), typeof(int), typeof(DashBoardContent), 320);
    public static readonly BindableProperty NewHeightFrameProperty = BindableProperty.Create(nameof(NewHeightFrame), typeof(int), typeof(DashBoardContent), 300);

    public DashBoardContent()
	{
		InitializeComponent();

        SetBanner();
        DeviceDisplay.MainDisplayInfoChanged += DeviceDisplay_MainDisplayInfoChanged;

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

    private void DeviceDisplay_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        => SetBanner();

    private void SetBanner()
    {
        if (DeviceInfo.Idiom == DeviceIdiom.Desktop)
        {
            NewWidthFrame = 410;
            NewHeightFrame = 250;
            return;
        }

        switch (DeviceDisplay.Current.MainDisplayInfo.Orientation)
        {
            case DisplayOrientation.Landscape:
                NewWidthFrame = 410;
                NewHeightFrame = 250;
                break;
            case DisplayOrientation.Portrait:
                NewWidthFrame = 320;
                NewHeightFrame = 300;
                break;
        }
    }
}