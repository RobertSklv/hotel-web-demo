using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using HotelWebDemo.Models.Components.Common;

namespace HotelWebDemo.Models.Components.Admin.Tables;

public class TableCellData
{
    public TableColumnData ColumnData { get; set; }

    public IModel Item { get; set; }

    public object? GetValue()
    {
        return ColumnData.ValueCallback(Item);
    }

    public string? GetFormattedValue(string? format)
    {
        object? value = GetValue();
        if (value != null && format != null)
        {
            Type valueType = value.GetType();
            Type[] parameters = new Type[]
            {
                typeof(string)
            };
            MethodInfo? toStringMethod = valueType.GetMethod(nameof(ToString), parameters);

            if (toStringMethod != null)
            {
                return toStringMethod.Invoke(value, new object[] { format })?.ToString();
            }
        }

        return value?.ToString();
    }

    public string? GetLink()
    {
        return ColumnData.LinkCallback?.Invoke(Item);
    }
}
