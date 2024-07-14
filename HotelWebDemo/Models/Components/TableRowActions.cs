namespace HotelWebDemo.Models.Components;

public class TableRowActions
{
    public string Controller { get; }

    public int ItemId { get; }

    public bool ConfirmDelete { get; }

    public string ConfirmMessage { get; set; } = "Are you sure you want to delete this item?";

    public string ConfirmModalId => $"delete{Controller}Modal-{ItemId}";

    public string ConfirmModalLabelId => $"delete{Controller}ModalLabel-{ItemId}";

    public TableRowActions(string controller, int itemId, bool confirmDelete = true)
    {
        Controller = controller;
        ItemId = itemId;
        ConfirmDelete = confirmDelete;
    }
}