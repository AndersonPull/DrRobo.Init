using System.ComponentModel;
using System.Runtime.Serialization;
using Drrobo.Utils.Converters;

namespace Drrobo.Modules.Dashboard.Enums
{
    [TypeConverter(typeof(PreventStringEnumConverter))]
    public enum TerminalCommandEnum
	{
        [EnumMember(Value = "devices"), Description("devices")]
        Devices,

        [EnumMember(Value = "connect")]
        Connect,

        [EnumMember(Value = "disconnect")]
        Disconnect,

        [EnumMember(Value = "open_cam")]
        OpenCam,

        [EnumMember(Value = "close_cam")]
        CloseCam,

        [EnumMember(Value = "create")]
        Create,

        [EnumMember(Value = "remove")]
        Remove,

        [EnumMember(Value = "clear")]
        Clear,

        [EnumMember(Value = "move")]
        Move,
    }
}