namespace Drrobo.Modules.RemotelyControlled.Components;

public partial class AnimationCam : ContentView
{
	public AnimationCam()
	{
		InitializeComponent();
        _ = StartAnimationJoystick();
    }

    private async Task StartAnimationJoystick()
    {
        await Task.Delay(500);
        await DeviceImage.RotateTo(-90, 700);
        await Task.Delay(500);
    }
}
