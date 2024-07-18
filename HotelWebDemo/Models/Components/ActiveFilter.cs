namespace HotelWebDemo.Models.Components;

public class ActiveFilter
{
    public string PropertyName { get; set; }

    public string Name { get; set; }

    public string RawOperator { get; set; }

    public string Operator { get; set; }

    public string Value { get; set; }

    public string? SecondaryValue { get; set; }
}