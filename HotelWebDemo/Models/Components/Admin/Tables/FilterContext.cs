using HotelWebDemo.Models.Attributes;
using System.Reflection;
using HotelWebDemo.Services;
using Microsoft.EntityFrameworkCore.Infrastructure;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.Components.Common;

namespace HotelWebDemo.Models.Components.Admin.Tables;

public class FilterContext
{
    public Table Table { get; }

    public List<TableColumnData> ColumnDatas { get; }

    public List<ActiveFilter> ActiveFilters { get; set; }

    public List<TableFilterControls> FilterControls { get; set; }

    public List<FilterOperatorOption> TextOperatorOptions { get; set; }

    public List<FilterOperatorOption> NumericOperatorOptions { get; set; }

    public List<FilterOperatorOption> DateOperatorOptions { get; set; }

    public List<FilterOperatorOption> ObjectOperatorOptions { get; set; }

    public List<FilterOperatorOption> BooleanOperatorOptions { get; set; }

    public FilterContext(Table table, List<TableColumnData> columnDatas)
    {
        Table = table;
        ColumnDatas = columnDatas;
    }

    public void Init()
    {
        TextOperatorOptions = new()
        {
            CreateOperatorOption(EntityFilterService.OPERATOR_EQUAL),
            CreateOperatorOption(EntityFilterService.OPERATOR_NOT_EQUAL),
            CreateOperatorOption(EntityFilterService.OPERATOR_CONTAINS),
        };

        NumericOperatorOptions = new()
        {
            CreateOperatorOption(EntityFilterService.OPERATOR_EQUAL),
            CreateOperatorOption(EntityFilterService.OPERATOR_NOT_EQUAL),
            CreateOperatorOption(EntityFilterService.OPERATOR_LESS_THAN),
            CreateOperatorOption(EntityFilterService.OPERATOR_LESS_THAN_OR_EQUAL),
            CreateOperatorOption(EntityFilterService.OPERATOR_GREATER_THAN),
            CreateOperatorOption(EntityFilterService.OPERATOR_GREATER_THAN_OR_EQUAL),
            CreateOperatorOption(EntityFilterService.BETWEEN),
        };

        DateOperatorOptions = new()
        {
            CreateOperatorOption(EntityFilterService.OPERATOR_EQUAL),
            CreateOperatorOption(EntityFilterService.OPERATOR_NOT_EQUAL),
            CreateOperatorOption(EntityFilterService.OPERATOR_BEFORE),
            CreateOperatorOption(EntityFilterService.OPERATOR_AFTER),
            CreateOperatorOption(EntityFilterService.BETWEEN),
        };

        BooleanOperatorOptions= new()
        {
            CreateOperatorOption(EntityFilterService.OPERATOR_EQUAL),
            CreateOperatorOption(EntityFilterService.OPERATOR_NOT_EQUAL),
        };

        ObjectOperatorOptions = new()
        {
            CreateOperatorOption(EntityFilterService.OPERATOR_EQUAL),
            CreateOperatorOption(EntityFilterService.OPERATOR_NOT_EQUAL),
        };

        FindActiveFilters();
        GenerateFilterControls();
    }

    protected void FindActiveFilters()
    {
        List<ActiveFilter> filters = new();

        if (Table.ListingModel.SearchPhrase != null)
        {
            ActiveFilter activeFilter = new()
            {
                PropertyName = nameof(ViewModels.ListingModel.SearchPhrase),
                Name = "Keywords",
                Value = Table.ListingModel.SearchPhrase,
                RawValue = Table.ListingModel.SearchPhrase,
            };

            filters.Add(activeFilter);
        }

        if (Table.ListingModel.Filters != null)
        {
            foreach (var filter in Table.ListingModel.Filters)
            {
                if (filter.Value == null || string.IsNullOrEmpty(filter.Value.Value))
                {
                    continue;
                }

                TableColumnData? colData = Table.FindColumn(filter.Key, strict: false);

                if (colData == null)
                {
                    continue;
                }

                if (filter.Value.Value == "0" && colData.PropertyType.IsSubclassOf(typeof(BaseEntity)))
                {
                    continue;
                }

                ActiveFilter activeFilter = new()
                {
                    Name = colData.Name,
                    PropertyName = colData.PropertyName,
                    RawOperator = filter.Value.Operator,
                    Operator = GetOperatorLabel(filter.Value.Operator),
                    Value = GetOptionLabel(colData.SelectableDataSource, filter.Value.Value) ?? filter.Value.Value,
                    RawValue = filter.Value.Value,
                    SecondaryValue = filter.Value.SecondaryValue,
                };

                filters.Add(activeFilter);
            }
        }

        ActiveFilters = filters;
    }

    public void GenerateFilterControls()
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
                Value = activeFilter?.RawValue,
                SecondaryValue = activeFilter?.SecondaryValue,
                InputType = GetFilterInputType(propType),
                OperatorOptions = GetOperatorOptions(propType),
                IsSelectableFilter = colData.IsSelectable,
                SelectableOptions = GenerateSelectableOptions(colData.SelectableDataSource)
            };

            controlsList.Add(controls);
        }

        FilterControls = controlsList;
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
                if (item is Option opt)
                {
                    options.Add(opt);

                    continue;
                }

                SelectOptionAttribute? selectOptionAttribute = item.GetType().GetCustomAttribute<SelectOptionAttribute>();

                if (selectOptionAttribute == null)
                {
                    throw new Exception($"The specified item found in the list doesn't have the {typeof(SelectOptionAttribute).ShortDisplayName()} attribute.");
                }

                Option option = new()
                {
                    Value = item.GetType().GetProperty(selectOptionAttribute.IdentityProperty).GetValue(item),
                    Content = item.GetType().GetProperty(selectOptionAttribute.LabelProperty).GetValue(item) as string,
                };

                options.Add(option);
            }
        }

        return options;
    }

    protected string? GetOptionLabel(dynamic selectableDataSource, object? value)
    {
        if (selectableDataSource != null)
        {
            foreach (object item in selectableDataSource)
            {
                if (item is Option option)
                {
                    return option.Content;
                }

                SelectOptionAttribute? selectOptionAttribute = item.GetType().GetCustomAttribute<SelectOptionAttribute>();

                if (selectOptionAttribute == null || item.GetType().GetProperty(selectOptionAttribute.IdentityProperty)?.GetValue(item)?.ToString() != value?.ToString())
                {
                    continue;
                }

                return item.GetType().GetProperty(selectOptionAttribute.LabelProperty)?.GetValue(item) as string;
            }
        }

        return null;
    }

    protected List<FilterOperatorOption> GetOperatorOptions(Type type)
    {
        if (type.Equals(typeof(string))) return TextOperatorOptions;
        if (type.Equals(typeof(DateTime))) return DateOperatorOptions;
        if (type.Equals(typeof(bool))) return BooleanOperatorOptions;
        else if (type.IsValueType) return NumericOperatorOptions;

        return ObjectOperatorOptions;
    }

    protected FilterOperatorOption CreateOperatorOption(string @operator)
    {
        return new FilterOperatorOption(@operator, GetOperatorLabel(@operator));
    }

    public string GetOperatorLabel(string @operator)
    {
        if (string.IsNullOrEmpty(@operator))
        {
            return @operator;
        }

        return EntityFilterService.OperatorLabelMap[@operator];
    }
}