namespace HotelWebDemo.Models.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class TableColumnAttribute : Attribute
{
    /// <summary>
    /// The name of the column displayed in the table. If left null or empty and there is 
    /// a DisplayAttribute attached to the property, the name from the DisplayAttribute is
    /// taken, otherwise, the property name is used as the last option.
    /// </summary>
    public string? Name { get; set; }

    public object? DefaultValue { get; set; }

    public bool Orderable { get; set; } = true;

    public bool Filterable { get; set; } = true;

    public int SortOrder { get; set; }
}
