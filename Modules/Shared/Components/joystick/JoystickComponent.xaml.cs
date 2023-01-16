namespace Drrobo.Modules.Shared.Components.joystick;

public partial class JoystickComponent : ContentView
{
    public JoystickComponent()
	{
		InitializeComponent();
    }
    
    void ButtonPressed(object sender, System.EventArgs e)
    {
        var button = (Button)sender;
        SendMessage(button.Text);
    }

    void ImageButtonPressedLeft(System.Object sender, System.EventArgs e)
    {
        SendMessage("L");
    }

    void ImageButtonPressedUp(System.Object sender, System.EventArgs e)
    {
        SendMessage("U");
    }

    void ImageButtonPressedRight(System.Object sender, System.EventArgs e)
    {
        SendMessage("R");
    }

    void ImageButtonPressedDown(System.Object sender, System.EventArgs e)
    {
        SendMessage("D");
    }

    void ButtonReleased(object sender, System.EventArgs e)
    {
        SendMessage("S");
    }

    void SendMessage(string message)
    {
        MessagingCenter.Send<string>(message, "WriteBluetooth");
    }
}