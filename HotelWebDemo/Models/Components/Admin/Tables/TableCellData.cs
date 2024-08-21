using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace HotelWebDemo.Models.Components.Admin.Tables;

public class TableCellData
{
    public TableColumnData ColumnData { get; set; }

    public object? Value { get; set; }

    public string? GetFormattedValue(string? format)
    {
        if (Value != null && format != null)
        {
            Type valueType = Value.GetType();
            Type[] parameters = new Type[]
            {
                typeof(string)
            };
            MethodInfo? toStringMethod = valueType.GetMethod(nameof(ToString), parameters);

            if (toStringMethod != null)
            {
                return toStringMethod.Invoke(Value, new object[] { format })?.ToString();
            }
        }

        return Value?.ToString();
    }
}
