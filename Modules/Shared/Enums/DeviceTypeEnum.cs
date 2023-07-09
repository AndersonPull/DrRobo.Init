using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Drrobo.Utils.Converters;

namespace Drrobo.Modules.Shared.Enums
{
    [TypeConverter(typeof(PreventStringEnumConverter))]
    public enum DeviceTypeEnum
	{
        [EnumMember(Value = "drone_icon"), Description("RemoTelyControlled")]
        Drone,
        [EnumMember(Value = "spider_icon"), Description("RemoTelyControlled")]
        Spider,
        [EnumMember(Value = "jumper_icon"), Description("RemoTelyControlled")]
        Jumper,
        [EnumMember(Value = "logo_icon"), Description("Camera")]
        Other
    }
}