using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;

namespace HotelWebDemo.Models.Components;

public abstract class Table
{
    public List<string> Headings { get; set; } = new();

    public TableContext TableContext { get; }

    public string Controller { get; }

    public abstract bool HasItems { get; }

    public Pagination? Pagination { get; set; }

    public TableRowActionsOptions? RowActionOptions { get; set; }

    public Table(TableContext tableContext, string controller)
    {
        TableContext = tableContext;
        Controller = controller;
    }

    public abstract List<object?> GetRowData(BaseEntity item);

    public TableRowActions GenerateRowActions(BaseEntity item)
    {
        if (RowActionOptions == null)
        {
            throw new Exception($"No row action options were defined!");
        }

        return new TableRowActions(item, RowActionOptions);
    }

    public abstract List<BaseEntity> GetItems();

    public List<TableLink> GenerateHeadingLinks()
    {
        List<TableLink> links = new();

        foreach (string heading in Headings)
        {
            string content = heading == "Id" ? "#" : heading;
            TableLink link = TableContext.CreateLink(content).SetOrder(heading);
            links.Add(link);
        }

        return links;
    }
}

public class Table<T> : Table
    where T : BaseEntity
{
    private List<Func<T, object?>> CellDataDelegates { get; set; } = new();

    public List<T> Items { get; set; }

    public override bool HasItems => Items.Count > 0;

    public Table(TableContext tableContext, string controller, List<T> items)
        : base(tableContext, controller)
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

    public Table<T> AddCellData(string columnName, Func<T, object?> cellData)
    {
        Headings.Add(columnName);
        CellDataDelegates.Add(cellData);

        return this;
    }

    public Table<T> AddPagination(PaginatedList<T> paginatedList)
    {
        Pagination = TableContext.CreatePagination(paginatedList);

        return this;
    }

    public Table<T> AddRowActions(Func<TableRowActionsOptions, TableRowActionsOptions> options)
    {
        TableRowActionsOptions opt = new(Controller);
        RowActionOptions = options(opt);

        return this;
    }
}