using System;
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
    }
}

