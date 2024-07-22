﻿using HotelWebDemo.Models.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;

namespace HotelWebDemo.Models.Components;

public abstract class Table
{
    public TableContext TableContext { get; }

    public Type ModelType { get; }

    public List<TableColumnData> ColumnDatas { get; private set; }

    public bool IsOrderable { get; set; }

    public bool IsFilterable { get; set; }

    public bool HasCreateAction { get; set; } = true;

    public abstract bool HasItems { get; }

    public Pagination? Pagination { get; set; }

    public TableRowActionsOptions? RowActionOptions { get; set; }

    public FilterContext FilterContext { get; set; }

    public Table(TableContext tableContext, Type modelType)
    {
        TableContext = tableContext;
        ModelType = modelType;

        GenerateTableColumnDatas();
        FilterContext = new(this, ColumnDatas!);
    }

    public abstract List<object?> GetRowData(BaseEntity item);

    public abstract List<BaseEntity> GetItems();

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

    public TableRowActions GenerateRowActions(BaseEntity item)
    {
        if (RowActionOptions == null)
        {
            throw new Exception($"No row action options were defined!");
        }

        return new TableRowActions(item, RowActionOptions);
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
            colData.ValueCallback = (object obj) => GetPropertyValue(property, obj) ?? colData.DefaultValue;

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
                    Element = TableContext.CreateLink(colData.Name).SetOrder(colData.PropertyName),
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
        if (TableContext.OrderBy == propertyName)
        {
            if (TableContext.Direction == "desc")
            {
                return HeadingFilterState.Descending;
            }

            return HeadingFilterState.Ascending;
        }

        return HeadingFilterState.None;
    }
}

public class Table<T> : Table
    where T : BaseEntity
{
    public delegate object? ColumnValueOverrider(T model, object? originalValue);

    public List<T> Items { get; set; }

    public override bool HasItems => Items.Count > 0;

    public Table(TableContext tableContext, List<T> items)
        : base(tableContext, typeof(T))
    {
        Items = items;
    }

    public override List<object?> GetRowData(BaseEntity item)
    {
        return GetRowData((T)item);
    }

    public override List<BaseEntity> GetItems()
    {
        List<BaseEntity> items = new();

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

        colData.ValueCallback = (object obj) =>
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

    public Table<T> AddPagination(PaginatedList<T> paginatedList)
    {
        Pagination = TableContext.CreatePagination(paginatedList);

        return this;
    }

    public Table<T> AddRowActions(string controller, Func<TableRowActionsOptions, TableRowActionsOptions> options)
    {
        TableRowActionsOptions opt = new(controller);
        RowActionOptions = options(opt);

        return this;
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
}