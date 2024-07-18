namespace HotelWebDemo.Models.Components;

public class FilterOperatorOption
{
    public string Value { get; set; }

    public string Label { get; set; }

    public FilterOperatorOption(string value, string label)
    {
        Value = value;
        Label = label;
    }
}