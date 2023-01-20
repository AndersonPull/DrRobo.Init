using Drrobo.Modules.Shared.Enums;

namespace Drrobo.Modules.Shared.Components.joystick;

public partial class JoystickComponent : ContentView
{
    public static readonly BindableProperty CommunicationTypeProperty = BindableProperty.Create(nameof(CommunicationType), typeof(CommunicationTypeEnum), typeof(JoystickComponent));

    public JoystickComponent()
	{
		InitializeComponent();
    }

    public CommunicationTypeEnum CommunicationType
    {
        get => (CommunicationTypeEnum)GetValue(CommunicationTypeProperty);
        set => SetValue(CommunicationTypeProperty, value);
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
        if(CommunicationType == CommunicationTypeEnum.Bluetooth)
            MessagingCenter.Send<string>(message, "WriteBluetooth");
        else
            MessagingCenter.Send<string>(message, "WriteWifiDrone");
    }
}