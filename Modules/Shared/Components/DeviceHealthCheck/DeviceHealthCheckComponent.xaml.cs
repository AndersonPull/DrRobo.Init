using LiveChartsCore;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace Drrobo.Modules.Shared.Components.DeviceHealthCheck;

public partial class DeviceHealthCheckComponent : ContentView
{
    public static readonly BindableProperty BatteryProperty = BindableProperty.Create(nameof(Battery), typeof(int), typeof(DeviceHealthCheckComponent));
    public static readonly BindableProperty BatteryPercentageProperty = BindableProperty.Create(nameof(BatteryPercentage), typeof(IEnumerable<ISeries>), typeof(DeviceHealthCheckComponent));

    public DeviceHealthCheckComponent()
    {
        InitializeComponent();

        seting();
    }

    public int Battery
    {
        get => (int)GetValue(BatteryProperty);
        set => SetValue(BatteryProperty, value);
    }

    public IEnumerable<ISeries> BatteryPercentage
    {
        get => (IEnumerable<ISeries>)GetValue(BatteryPercentageProperty);
        set => SetValue(BatteryPercentageProperty, value);
    }

    private void seting()
    {
        BatteryPercentage = new GaugeBuilder()
            .WithInnerRadius(120)
            .WithBackgroundInnerRadius(120)
            .WithBackground(new SolidColorPaint(new SKColor(30, 30, 30)))
            .WithLabelsPosition(PolarLabelsPosition.ChartCenter)
            .AddValue(Battery, null, new SKColor(47, 193, 44), SKColors.Transparent)
            .BuildSeries();
    }
}