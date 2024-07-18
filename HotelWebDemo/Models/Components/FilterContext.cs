using HotelWebDemo.Models.Attributes;
using System.Reflection;
using HotelWebDemo.Services;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace HotelWebDemo.Models.Components;

public class FilterContext
{
    public Table Table { get; }

    public List<TableColumnData> ColumnDatas { get; }

    public List<ActiveFilter> ActiveFilters { get; set; }

    public readonly List<FilterOperatorOption> TextOperatorOptions;

    public readonly List<FilterOperatorOption> NumericOperatorOptions;

    public FilterContext(Table table, List<TableColumnData> columnDatas)
    {
        Table = table;
        ColumnDatas = columnDatas;
        FindActiveFilters();

        TextOperatorOptions = new()
        {
            CreateOperatorOption(FilterService.OPERATOR_EQUAL),
            CreateOperatorOption(FilterService.OPERATOR_NOT_EQUAL),
            CreateOperatorOption(FilterService.OPERATOR_CONTAINS),
        };

        NumericOperatorOptions = new()
        {
            CreateOperatorOption(FilterService.OPERATOR_EQUAL),
            CreateOperatorOption(FilterService.OPERATOR_NOT_EQUAL),
            CreateOperatorOption(FilterService.OPERATOR_LESS_THAN),
            CreateOperatorOption(FilterService.OPERATOR_LESS_THAN_OR_EQUAL),
            CreateOperatorOption(FilterService.OPERATOR_GREATER_THAN),
            CreateOperatorOption(FilterService.OPERATOR_GREATER_THAN_OR_EQUAL),
            CreateOperatorOption(FilterService.BETWEEN),
        };
    }

    public List<TableFilterControls> GenerateFilterControls()
    {
        List<TableFilterControls> controlsList = new();

        foreach (TableColumnData colData in ColumnDatas)
        {
            if (!colData.Filterable)
            {
                continue;
            }

            Type propType = colData.PropertyType;
            ActiveFilter? activeFilter = ActiveFilters.Find(f => f.PropertyName == colData.PropertyName);

            TableFilterControls controls = new()
            {
                Name = colData.Name,
                PropertyName = colData.PropertyName,
                SelectedOperator = activeFilter?.RawOperator ?? string.Empty,
                Value = activeFilter?.Value,
                SecondaryValue = activeFilter?.SecondaryValue,
                InputType = GetFilterInputType(propType),
                OperatorOptions = GetOperatorOptions(propType),
                IsSelectableFilter = colData.IsSelectable,
                SelectableOptions = GenerateSelectableOptions(colData.SelectableDataSource)
            };

            controlsList.Add(controls);
        }

        return controlsList;
    }

    protected string GetFilterInputType(Type type)
    {
        if (type.Equals(typeof(string))) return "text";
        else if (type.Equals(typeof(long))) return "number";
        else if (type.Equals(typeof(ulong))) return "number";
        else if (type.Equals(typeof(int))) return "number";
        else if (type.Equals(typeof(uint))) return "number";
        else if (type.Equals(typeof(short))) return "number";
        else if (type.Equals(typeof(ushort))) return "number";
        else if (type.Equals(typeof(sbyte))) return "number";
        else if (type.Equals(typeof(byte))) return "number";
        else if (type.Equals(typeof(double))) return "number";
        else if (type.Equals(typeof(float))) return "number";
        else if (type.Equals(typeof(decimal))) return "number";
        else if (type.Equals(typeof(DateTime))) return "date";
        else if (type.Equals(typeof(long?))) return "number";
        else if (type.Equals(typeof(ulong?))) return "number";
        else if (type.Equals(typeof(int?))) return "number";
        else if (type.Equals(typeof(uint?))) return "number";
        else if (type.Equals(typeof(short?))) return "number";
        else if (type.Equals(typeof(ushort?))) return "number";
        else if (type.Equals(typeof(sbyte?))) return "number";
        else if (type.Equals(typeof(byte?))) return "number";
        else if (type.Equals(typeof(double?))) return "number";
        else if (type.Equals(typeof(float?))) return "number";
        else if (type.Equals(typeof(decimal?))) return "number";
        else if (type.Equals(typeof(DateTime?))) return "date";
        else return "text";
    }

    protected List<Option> GenerateSelectableOptions(dynamic selectableDataSource)
    {
        List<Option> options = new();

        if (selectableDataSource != null)
        {
            foreach (object item in selectableDataSource)
            {
                SelectOptionAttribute? selectOptionAttribute = item.GetType().GetCustomAttribute<SelectOptionAttribute>();

                if (selectOptionAttribute == null)
                {
                    throw new Exception($"The specified item found in the list doesn't have the {typeof(SelectOptionAttribute).ShortDisplayName()} attribute.");
                }

                Option option = new()
                {
                    Value = item.GetType().GetProperty(selectOptionAttribute.IdentityProperty).GetValue(item),
                    Label = item.GetType().GetProperty(selectOptionAttribute.LabelProperty).GetValue(item) as string,
                };

                options.Add(option);
            }
        }

        return options;
    }

    protected List<FilterOperatorOption> GetOperatorOptions(Type type)
    {
        if (type.Equals(typeof(string))) return TextOperatorOptions;

        return NumericOperatorOptions;
    }

    protected FilterOperatorOption CreateOperatorOption(string @operator)
    {
        return new FilterOperatorOption(@operator, GetOperatorLabel(@operator));
    }

    protected void FindActiveFilters()
    {
        List<ActiveFilter> filters = new();

        if (Table.TableContext.Filter != null)
        {
            foreach (var filter in Table.TableContext.Filter)
            {
                if (filter.Value == null || string.IsNullOrEmpty(filter.Value.Value))
                {
                    continue;
                }

                TableColumnData colData = Table.FindColumn(filter.Key);

                ActiveFilter activeFilter = new()
                {
                    Name = colData.Name,
                    PropertyName = colData.PropertyName,
                    RawOperator = filter.Value.Operator,
                    Operator = GetOperatorLabel(filter.Value.Operator),
                    Value = filter.Value.Value,
                    SecondaryValue = filter.Value.SecondaryValue,
                };

                filters.Add(activeFilter);
            }
        }

        ActiveFilters = filters;
    }

    public string GetOperatorLabel(string @operator)
    {
        if (string.IsNullOrEmpty(@operator))
        {
            return @operator;
        }

        return FilterService.OperatorLabelMap[@operator];
    }
}