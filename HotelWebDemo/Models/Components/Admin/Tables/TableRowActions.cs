using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Models.Components.Admin.Tables;

public class TableRowActions
{
    public IBaseEntity Item { get; }

    public List<RowAction> Actions { get; set; } = new();

    public TableRowActions(IBaseEntity item, List<RowAction> actions)
    {
        Item = item;
        Actions = actions;
    }
}