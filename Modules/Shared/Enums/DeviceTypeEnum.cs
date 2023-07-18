using System.ComponentModel;
using System.Runtime.Serialization;
using Drrobo.Utils.Converters;

namespace Drrobo.Modules.Shared.Enums
{
    [TypeConverter(typeof(PreventStringEnumConverter))]
    public enum DeviceTypeEnum
	{
        [EnumMember(Value = "drone_icon"), Description("RemoTely Controlled")]
        Drone,
        [EnumMember(Value = "spider_icon"), Description("RemoTely Controlled")]
        Spider,
        [EnumMember(Value = "jumper_icon"), Description("RemoTely Controlled")]
        Jumper,
        [EnumMember(Value = "logo_icon"), Description("universal control")]
        UniversalControl,
        [EnumMember(Value = "logo_icon"), Description("Other")]
        Other
    }
}