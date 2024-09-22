using HotelWebDemo.Models.Attributes;
using HotelWebDemo.Models.Components.Common;

namespace HotelWebDemo.Models.Components.Admin.Tables;

public class TableColumnData
{
    public string PropertyName { get; set; }

    public Type PropertyType { get; set; }

    public string Name { get; set; }

    public Func<object, object?> ValueCallback { get; set; }

    public object? DefaultValue { get; set; }

    public string? Format { get; set; }

    public TableColumnSpecialFormat SpecialFormat { get; set; }

    public bool Orderable { get; set; }

    public bool Filterable { get; set; }

    public int SortOrder { get; set; }

    public bool IsSelectable { get; set; }

    public dynamic SelectableDataSource { get; set; }

    public Func<object, string>? LinkCallback { get; set; }
}