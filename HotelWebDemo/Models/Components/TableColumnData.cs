namespace HotelWebDemo.Models.Components;

public class TableColumnData
{
    public string PropertyName { get; set; }

    public Type PropertyType { get; set; }

    public string Name { get; set; }

    public Func<object, object?> ValueCallback { get; set; }

    public object? DefaultValue { get; set; }

    public bool Orderable { get; set; }

    public bool Filterable { get; set; }

    public int SortOrder { get; set; }
}