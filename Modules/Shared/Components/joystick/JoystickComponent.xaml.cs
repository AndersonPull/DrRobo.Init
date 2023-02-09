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

    void ButtonPressed(object sender, EventArgs e)
    {
        var button = (Button)sender;
        SendMessage(button.Text);
    }

    void ImageButtonPressedLeft(object sender, EventArgs e)
    {
        SendMessage("L");
    }

    void ImageButtonPressedUp(object sender, EventArgs e)
    {
        SendMessage("U");
    }

    void ImageButtonPressedRight(object sender, EventArgs e)
    {
        SendMessage("R");
    }

    void ImageButtonPressedDown(object sender, EventArgs e)
    {
        SendMessage("D");
    }

    void ButtonReleased(object sender, EventArgs e)
    {
        SendMessage("S");
    }

    void SendMessage(string message)
    {
        if(CommunicationType == CommunicationTypeEnum.Bluetooth)
            MessagingCenter.Send(message, "WriteBluetooth");
        else
            MessagingCenter.Send(message, "WriteWifiDrone");
    }
}