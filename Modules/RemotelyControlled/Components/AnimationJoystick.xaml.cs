﻿namespace Drrobo.Modules.RemotelyControlled.Components;

public partial class AnimationJoystick : ContentView
{
	public AnimationJoystick()
	{
		InitializeComponent();

        _ = StartAnimationJoystick();
    }

    private async Task StartAnimationJoystick()
    {
        await Task.Delay(500);
        await DeviceImage.RotateTo(-90, 700);
        await Task.Delay(500);
        DeviceImage.IsVisible = false;
        JoystickImage.IsVisible = true;
    }
}