using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace HotelWebDemo.Extensions;

public static class TempDataDictionaryExtensions
{
    public static void Set<T>(this ITempDataDictionary tempData, string key, T value)
    {
        try
        {
            tempData[key] = JsonConvert.SerializeObject(value);
        }
        catch (Exception)
        {
        }
    }

    public static T? Get<T>(this ITempDataDictionary tempData, string key, string @default = "null")
    {
        try
        {
            return JsonConvert.DeserializeObject<T>(tempData[key] as string ?? @default);
        }
        catch (Exception)
        {
            tempData.Remove(key);

            return default;
        }
    }

    public static T? Pop<T>(this ITempDataDictionary tempData, string key, string @default = "null")
    {
        T? t = tempData.Get<T>(key, @default);
        tempData.Remove(key);

        return t;
    }
}
