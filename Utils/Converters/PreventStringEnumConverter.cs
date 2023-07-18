using System;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using static System.Enum;

namespace Drrobo.Utils.Converters
{
    public class PreventStringEnumConverter : StringEnumConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                var enumString = (string)reader.Value;
                foreach (var name in GetNames(objectType))
                {
                    var enumMemberAttribute = ((EnumMemberAttribute[])objectType.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true)).SingleOrDefault();
                    if (enumMemberAttribute?.Value == enumString)
                        return Parse(objectType, name, true);
                }
                var parsed = Parse(objectType, enumString, true);
                return parsed;
            }
            catch
            {
                return objectType.GetDefaultValueEnum();
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            try
            {
                var type = value.GetType();
                var memInfo = type.GetMember(value.ToString());
                var attr = memInfo[0].GetCustomAttributes(false).OfType<EnumMemberAttribute>().FirstOrDefault();
                if (attr != null)
                {
                    writer.WriteValue(attr.Value);
                    return;
                }

                writer.WriteValue(Convert.ChangeType(value, GetUnderlyingType(type)));
            }
            catch
            {
                //AsyncErrorHandler.HandleException(new Exception($"[StringEnumConverter] - Write - {e.Message}", e));
            }
        }
    }
}

