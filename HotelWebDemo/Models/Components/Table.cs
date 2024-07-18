using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;

namespace HotelWebDemo.Models.Components;

public abstract class Table
{
    public List<string> Headings { get; set; } = new();

    public Dictionary<string, string> HeadingCustomNames { get; set; } = new()
    {
        { "Id", "#" }
    };

    public TableContext TableContext { get; }

    public bool IsOrderable { get; set; }

    public bool HasCreateAction { get; set; } = true;

    public abstract bool HasItems { get; }

    public Pagination? Pagination { get; set; }

    public TableRowActionsOptions? RowActionOptions { get; set; }

    public Table(TableContext tableContext)
    {
        TableContext = tableContext;
    }

    public abstract List<object?> GetRowData(BaseEntity item);

    public abstract List<BaseEntity> GetItems();

    public Table SetOrderable(bool orderable)
    {
        IsOrderable = orderable;

        return this;
    }

    public Table IncludeCreateAction(bool createAction)
    {
        HasCreateAction = createAction;

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

    public List<Element> GenerateHeadingElements()
    {
        List<Element> headingElements = new();

        foreach (string heading in Headings)
        {
            string content = HeadingCustomNames.ContainsKey(heading)
                ? HeadingCustomNames[heading]
                : heading;

            Element headingElement;

            if (IsOrderable)
            {
                headingElement = TableContext.CreateLink(content).SetOrder(heading);
            }
            else
            {
                headingElement = new Element()
                {
                    Content = content
                };
            }

            headingElements.Add(headingElement);
        }

        return headingElements;
    }
}

public class Table<T> : Table
    where T : BaseEntity
{
    private List<Func<T, object?>> CellDataDelegates { get; set; } = new();

    public List<T> Items { get; set; }

    public override bool HasItems => Items.Count > 0;

    public Table(TableContext tableContext, List<T> items)
        : base(tableContext)
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

        foreach (Func<T, object?> callback in CellDataDelegates)
        {
            rowData.Add(callback(item));
        }

        return rowData;
    }

    public Table<T> AddCellData(string propertyName, Func<T, object?> cellData)
    {
        Headings.Add(propertyName);
        CellDataDelegates.Add(cellData);

        return this;
    }

    public Table<T> AddCellData(string propertyName, string customName, Func<T, object?> cellData)
    {
        HeadingCustomNames.Add(propertyName, customName);

        return AddCellData(propertyName, cellData);
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

    public new Table<T> SetOrderable(bool orderable)
    {
        return (Table<T>)base.SetOrderable(orderable);
    }

    public new Table<T> IncludeCreateAction(bool createAction)
    {
        return (Table<T>)base.IncludeCreateAction(createAction);
    }
}