namespace HotelWebDemo.Extensions;

public static class DictionaryExtensions
{
    public static Dictionary<string, string> ToStringDictionary(this Dictionary<string, object> dic)
    {
        Dictionary<string, string> result = new();

        foreach (var kvp in dic)
        {
            result.Add(kvp.Key, kvp.Value.ToString() ?? string.Empty);
        }

        return result;
    }
}
