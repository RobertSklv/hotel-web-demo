using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Models.Components;

public class TableRowActionsOptions
{
    public string Controller { get; }

    public bool ConfirmDelete { get; set; }

    public bool Edit { get; set; } = true;

    public bool Delete { get; set; } = true;

    public Func<BaseEntity, string> DeleteConfirmMessageCallback { get; set; } = m => $"Are you sure you want to delete item of type {m.GetType().Name} and ID {m.Id}?";

    public TableRowActionsOptions(string controller, bool confirmDelete = true)
    {
        Controller = controller;
        ConfirmDelete = confirmDelete;
    }

    public TableRowActionsOptions SetDeleteConfirmationMessage(Func<BaseEntity, string> deleteConfirmMessageCallback)
    {
        ConfirmDelete = true;
        DeleteConfirmMessageCallback = deleteConfirmMessageCallback;

        return this;
    }

    public TableRowActionsOptions SetDeleteConfirmationMessage<T>(Func<T, string> deleteConfirmMessageCallback)
        where T : BaseEntity
    {
        ConfirmDelete = true;
        DeleteConfirmMessageCallback = m => deleteConfirmMessageCallback((T)m);

        return this;
    }

    public TableRowActionsOptions IncludesEdit(bool edit)
    {
        Edit = edit;

        return this;
    }

    public TableRowActionsOptions IncludesDelete(bool delete)
    {
        Delete = delete;

        return this;
    }
}
