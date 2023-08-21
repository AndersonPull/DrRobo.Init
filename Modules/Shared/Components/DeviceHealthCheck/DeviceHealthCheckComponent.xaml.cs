using LiveChartsCore;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace Drrobo.Modules.Shared.Components.DeviceHealthCheck;

public partial class DeviceHealthCheckComponent : ContentView
{
    public static readonly BindableProperty BatteryValueProperty = BindableProperty.Create(nameof(BatteryValue), typeof(int), typeof(DeviceHealthCheckComponent));
    public static readonly BindableProperty BatteryPercentageProperty = BindableProperty.Create(nameof(BatteryPercentage), typeof(IEnumerable<ISeries>), typeof(DeviceHealthCheckComponent));

    public DeviceHealthCheckComponent()
    {
        InitializeComponent();
    }

    public int BatteryValue
    {
        get => (int)GetValue(BatteryValueProperty);
        set => SetValue(BatteryValueProperty, value);
    }

    public IEnumerable<ISeries> BatteryPercentage
    {
        get => (IEnumerable<ISeries>)GetValue(BatteryPercentageProperty);
        set => SetValue(BatteryPercentageProperty, value);
    }

    protected override void OnPropertyChanged(string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
        if (nameof(BatteryValue).Equals(propertyName))
        {
            BatteryPercentage = new GaugeBuilder()
             .WithInnerRadius(120)
             .WithBackgroundInnerRadius(120)
             .WithBackground(new SolidColorPaint(new SKColor(30, 30, 30)))
             .WithLabelsPosition(PolarLabelsPosition.ChartCenter)
             .AddValue(BatteryValue, null, new SKColor(47, 193, 44), SKColors.Transparent)
             .BuildSeries();
        }
    }
}