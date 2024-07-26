using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Models.Components;

public class TableRowActions
{
    public IBaseEntity Item { get; }

    public TableRowActionsOptions Options { get; }

    public string DeleteConfirmMessage => Options.DeleteConfirmMessageCallback(Item);

    public string ConfirmModalId => $"delete{Options.Controller}Modal-{Item.Id}";

    public string ConfirmModalLabelId => $"delete{Options.Controller}ModalLabel-{Item.Id}";

    public TableRowActions(IBaseEntity item, TableRowActionsOptions options)
    {
        Item = item;
        Options = options;
    }
}