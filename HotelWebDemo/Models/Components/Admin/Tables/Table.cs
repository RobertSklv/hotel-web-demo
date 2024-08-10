using HotelWebDemo.Models.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;
using HotelWebDemo.Models.Components.Common;

namespace HotelWebDemo.Models.Components.Admin.Tables;

public abstract class Table
{
    public IListingModel ListingModel { get; set; }

    public Type ModelType { get; set; }

    public List<TableColumnData> ColumnDatas { get; private set; }

    public bool IsOrderable { get; set; }

    public bool IsFilterable { get; set; }

    public bool IsSearchable { get; set; }

    public bool IsPageSizeAdjustable { get; set; }

    public abstract bool HasItems { get; }

    public Pagination? Pagination { get; set; }

    public List<RowAction> RowActions { get; set; } = new();

    public FilterContext FilterContext { get; set; }

    public MassActionContext MassActionContext { get; set; }

    public List<Option> YesNoOptions { get; set; }

    public Table(IListingModel listingModel, Type modelType)
    {
        ListingModel = listingModel;
        ModelType = modelType;

        YesNoOptions = new()
        {
            new Option()
            {
                Value = "false",
                Content = "No"
            },
            new Option()
            {
                Value = "true",
                Content = "Yes"
            },
        };

        GenerateTableColumnDatas();
        FilterContext = new(this, ColumnDatas!);

        MassActionContext = new()
        {
            ListingModel = ListingModel
        };
    }

    public abstract List<object?> GetRowData(IBaseEntity item);

    public abstract List<IBaseEntity> GetItems();

    public List<Option> GetPageSizeOptions()
    {
        List<Option> options = new();

        foreach (int pageSize in ViewModels.ListingModel.PageSizes)
        {
            Option option = new()
            {
                Value = pageSize,
                Content = pageSize.ToString(),
                Selected = pageSize == ListingModel.PageSize
            };
            options.Add(option);
        }

        return options;
    }

    public Table SetOrderable(bool orderable)
    {
        IsOrderable = orderable;

        return this;
    }

    public Table SetFilterable(bool filterable)
    {
        IsFilterable = filterable;

        return this;
    }

    public Table SetSearchable(bool searchable)
    {
        IsSearchable = searchable;

        return this;
    }

    public Table SetAdjustablePageSize(bool adjustable)
    {
        IsPageSizeAdjustable = adjustable;

        return this;
    }

    public TableRowActions GenerateRowActions(IBaseEntity item)
    {
        return new TableRowActions(item, RowActions);
    }

    public TableColumnData? FindColumn(string propertyName, bool strict = true)
    {
        TableColumnData? colData = ColumnDatas.Where(cd => cd.PropertyName == propertyName).FirstOrDefault();

        if (colData == null && strict)
        {
            throw new Exception($"No '{propertyName}' column was defined!");
        }

        return colData;
    }

    public object? GetPropertyValue(PropertyInfo property, object obj)
    {
        object? propertyValue = property.GetValue(obj);

        SelectOptionAttribute? selectOptionAttribute = property.PropertyType.GetCustomAttribute<SelectOptionAttribute>();

        if (propertyValue == null)
        {
            if (selectOptionAttribute != null)
            {
                return selectOptionAttribute.UndefinedLabel;
            }

            return null;
        }

        if (selectOptionAttribute != null)
        {
            PropertyInfo? identityProperty = property.PropertyType.GetProperty(selectOptionAttribute.LabelProperty);

            if (identityProperty == null)
            {
                throw new Exception($"Cannot find identity property '{selectOptionAttribute.LabelProperty}' in the object of type {property.PropertyType}.");
            }

            return identityProperty.GetValue(propertyValue);
        }

        return propertyValue;
    }

    public string GetColumnName(PropertyInfo propertyInfo, TableColumnAttribute columnAttr)
    {
        if (!string.IsNullOrEmpty(columnAttr.Name))
        {
            return columnAttr.Name;
        }

        DisplayAttribute? displayAttr = propertyInfo.GetCustomAttribute<DisplayAttribute>();

        if (displayAttr != null && !string.IsNullOrEmpty(displayAttr.Name))
        {
            return displayAttr.Name;
        }

        return propertyInfo.Name;
    }

    public void GenerateTableColumnDatas()
    {
        PropertyInfo[] properties = ModelType.GetProperties();
        ColumnDatas = new();

        foreach (PropertyInfo property in properties)
        {
            TableColumnAttribute? columnAttr = property.GetCustomAttribute<TableColumnAttribute>();

            if (columnAttr == null)
            {
                continue;
            }

            TableColumnData colData = new()
            {
                PropertyName = property.Name,
                PropertyType = property.PropertyType,
                Name = GetColumnName(property, columnAttr),
                DefaultValue = columnAttr.DefaultValue,
                Filterable = columnAttr.Filterable,
                Orderable = columnAttr.Orderable,
                SortOrder = columnAttr.SortOrder,
                IsSelectable = property.PropertyType.IsSubclassOf(typeof(BaseEntity)) && property.PropertyType.GetCustomAttribute<SelectOptionAttribute>() != null
            };
            colData.ValueCallback = (obj) => GetPropertyValue(property, obj) ?? colData.DefaultValue;

            if (property.PropertyType.IsValueType && property.PropertyType.Equals(typeof(bool)))
            {
                colData.IsSelectable = true;
                colData.SelectableDataSource = YesNoOptions;
            }

            ColumnDatas.Add(colData);
        }

        ColumnDatas.Sort((d1, d2) => d1.SortOrder - d2.SortOrder);
    }

    public List<TableHeadingCell> GenerateHeadingCells()
    {
        List<TableHeadingCell> headingCells = new();

        foreach (TableColumnData colData in ColumnDatas)
        {
            TableHeadingCell cell;

            if (IsOrderable && colData.Orderable)
            {
                cell = new TableHeadingCell()
                {
                    Element = CreateLink(colData.Name).SetOrder(colData.PropertyName),
                    State = GetHeadingFilterState(colData.PropertyName)
                };
            }
            else
            {
                cell = new TableHeadingCell()
                {
                    Element = new Element()
                    {
                        Content = colData.Name
                    }
                };
            }

            headingCells.Add(cell);
        }

        return headingCells;
    }

    public HeadingFilterState GetHeadingFilterState(string propertyName)
    {
        if ((ListingModel.OrderBy == null && propertyName == ViewModels.ListingModel.DEFAULT_ORDER_BY) || ListingModel.OrderBy == propertyName)
        {
            if (ListingModel.Direction == null || ListingModel.Direction == ViewModels.ListingModel.DEFAULT_DIRECTION)
            {
                if (propertyName == ViewModels.ListingModel.DEFAULT_ORDER_BY)
                {
                    return HeadingFilterState.None;
                }

                return HeadingFilterState.Descending;
            }

            return HeadingFilterState.Ascending;
        }

        return HeadingFilterState.None;
    }

    public TableLink CreateLink(string content)
    {
        return new(ListingModel, content)
        {
            OrderBy = ListingModel.OrderBy,
            Direction = ListingModel.Direction,
            Page = ListingModel.Page,
            Filters = ListingModel.Filters,
            SearchPhrase = ListingModel.SearchPhrase,
        };
    }
}

public class Table<T> : Table
    where T : IBaseEntity
{
    public delegate object? ColumnValueOverrider(T model, object? originalValue);

    public PaginatedList<T> Items { get; set; }

    public override bool HasItems => Items.Count > 0;

    public Table(IListingModel listingModel, PaginatedList<T> items)
        : base(listingModel, typeof(T))
    {
        Items = items;
    }

    public override List<object?> GetRowData(IBaseEntity item)
    {
        return GetRowData((T)item);
    }

    public override List<IBaseEntity> GetItems()
    {
        List<IBaseEntity> items = new();

        foreach (T item in Items)
        {
            items.Add(item);
        }

        return items;
    }

    public List<object?> GetRowData(T item)
    {
        List<object?> rowData = new();

        foreach (TableColumnData colData in ColumnDatas)
        {
            rowData.Add(colData.ValueCallback(item));
        }

        return rowData;
    }

    public Table<T> OverrideColumnName(string propertyName, string columnName)
    {
        FindColumn(propertyName).Name = columnName;

        return this;
    }

    public Table<T> OverrideColumnValue(string propertyName, ColumnValueOverrider callback)
    {
        TableColumnData colData = FindColumn(propertyName);

        colData.ValueCallback = (obj) =>
        {
            return callback(
                model: (T)obj,
                originalValue: colData.ValueCallback(obj));
        };

        return this;
    }

    public Table<T> OverrideColumnSortOrder(string propertyName, int sortOrder)
    {
        FindColumn(propertyName).SortOrder = sortOrder;

        return this;
    }

    public Table<T> RemoveColumn(string propertyName)
    {
        ColumnDatas.Remove(FindColumn(propertyName));

        return this;
    }

    public Table<T> AddPagination(bool add)
    {
        if (add)
        {
            Pagination = new(Items.PageIndex, Items.TotalPages, this);
        }
        else
        {
            Pagination = null;
        }

        return this;
    }

    public Table<T> AddRowAction(
        string action,
        RequestMethod method = default,
        BootstrapIconType icon = default,
        Func<RowAction, RowAction>? customizationCallback = null)
    {
        Type entityType = typeof(T);
        RowAction rowAction = new()
        {
            Action = action,
            Controller = entityType.Name,
            Method = method,
            Icon = icon,
            Content = action
        };

        if (customizationCallback != null )
        {
            rowAction = customizationCallback(rowAction);
        }

        RowActions.Add(rowAction);

        return this;
    }

    public Table<T> AddRowAction(RowAction? rowAction)
    {
        if (rowAction != null)
        {
            RowActions.Add(rowAction);
        }

        return this;
    }

    public Table<T> AddChainCall(Func<Table<T>, Table<T>> chainCallback)
    {
        chainCallback(this);

        return this;
    }

    public Table<T> AddMassAction(string actionId, string label, ColorClass color = ColorClass.Primary, string? controller = null)
    {
        Type entityType = typeof(T);
        string controllerName = controller ?? entityType.Name;

        MassAction massAction = new()
        {
            ActionId = actionId,
            Label = label,
            Controller = controllerName,
            Color = color
        };

        MassActionContext.Actions.Add(massAction);

        return this;
    }

    public Table<T> AddMassDeleteAction(string? controller = null)
    {
        return AddMassAction("MassDelete", "Delete selected", ColorClass.Danger, controller);
    }

    public Table<T> SetSelectableOptionsSource(string propertyName, dynamic dataSource)
    {
        FindColumn(propertyName).SelectableDataSource = dataSource;

        return this;
    }

    public new Table<T> SetOrderable(bool orderable)
    {
        return (Table<T>)base.SetOrderable(orderable);
    }

    public new Table<T> SetFilterable(bool filterable)
    {
        return (Table<T>)base.SetFilterable(filterable);
    }

    public new Table<T> SetSearchable(bool searchable)
    {
        return (Table<T>)base.SetSearchable(searchable);
    }

    public new Table<T> SetAdjustablePageSize(bool adjustable)
    {
        return (Table<T>)base.SetAdjustablePageSize(adjustable);
    }
}