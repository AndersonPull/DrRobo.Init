using System;
using System.ComponentModel;

namespace Drrobo.Utils
{
	public class Util
	{
        public static T GetResource<T>(string resource)
        {
            if (Application.Current.Resources.TryGetValue(resource, out var obj))
                return (T)obj;
#if DEBUG
            throw new Exception($"Resource: {resource} não encontrado.");
#else
            return default(T);
#endif
        }

        public static object GetDefaultValueEnum(Type enumType)
        {
            var attributes = (DefaultValueAttribute[])enumType.GetCustomAttributes(typeof(DefaultValueAttribute), false);
            return attributes.Length > 0 ? attributes[0].Value : Activator.CreateInstance(enumType);
        }

        public static bool IsValidUrl(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
                return true;

            return false;
        }
    }
}

