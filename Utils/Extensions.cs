using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Drrobo.Utils
{
    public static class Extensions
    {
        public static object GetDefaultValueEnum(this Type enumType)
        {
            var attributes =
                (DefaultValueAttribute[])enumType.GetCustomAttributes(typeof(DefaultValueAttribute), false);
            return attributes.Length > 0 ? attributes[0].Value : Activator.CreateInstance(enumType);
        }

        public static string Value(this Enum @enum)
        {
            var attr =
                @enum.GetType().GetMember(@enum.ToString()).FirstOrDefault()?.
                    GetCustomAttributes(false).OfType<EnumMemberAttribute>().
                    FirstOrDefault();
            return attr == null ? @enum.ToString() : attr.Value;
        }

        public static string Description(this Enum @enum)
        {
            var attr =
             @enum.GetType().GetMember(@enum.ToString()).FirstOrDefault()?.
                 GetCustomAttributes(false).OfType<DescriptionAttribute>().
                 FirstOrDefault();
            return attr == null ? @enum.ToString() : attr.Description;
        }
    }
}

