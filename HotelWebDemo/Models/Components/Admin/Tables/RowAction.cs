using HotelWebDemo.Models.Components.Common;
using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Models.Components.Admin.Tables;

public class RowAction : RouteElement
{
    public delegate string DynamicConfirmationMessage<T>(T entity) where T : IBaseEntity;
    public delegate string DynamicRoute(int id);

    public DynamicRoute? DynamicRouteCallback { get; set; }

    public RequestMethod Method { get; set; }

    public bool HasConfirmationPopup { get; set; }

    public string ConfirmationTitle { get; set; } = "Action confirmation";

    public string ConfirmationMessage { get; set; } = "Are you sure you want to perform this action on this item?";

    public int SortOrder { get; set; } = 10;

    public Func<IBaseEntity, string> ConfirmMessageCallback { get; set; } = m => $"Are you sure you want to perform this action on item of type {m.GetType().Name} and ID {m.Id}?";

    public BootstrapIconType Icon { get; set; }

    public RowAction SetId(string id)
    {
        Id = id;

        return this;
    }

    public RowAction SetColor(ColorClass color)
    {
        Color = color;

        return this;
    }

    public RowAction SetContent(string content)
    {
        Content = content;

        return this;
    }

    public RowAction SetRoute(string route)
    {
        Route = route;

        return this;
    }

    public RowAction SetAction(string action)
    { 
        Action = action;

        return this;
    }

    public RowAction SetController(string controller)
    {
        Controller = controller;

        return this;
    }

    public RowAction SetArea(string area)
    {
        Area = area;

        return this;
    }

    public RowAction SetMethod(RequestMethod method)
    {
        Method = method;

        return this;
    }

    public RowAction SetSortOrder(int sortOrder)
    {
        SortOrder = sortOrder;

        return this;
    }

    public RowAction AddConfirmationPopup(bool add)
    {
        HasConfirmationPopup = add;

        return this;
    }

    public RowAction SetConfirmationTitle(string confirmTitle)
    {
        ConfirmationTitle = confirmTitle;

        return AddConfirmationPopup(true);
    }

    public RowAction SetConfirmationMessage(string confirmMessage)
    {
        ConfirmationMessage = confirmMessage;

        return AddConfirmationPopup(true);
    }

    public RowAction SetConfirmationMessage(Func<IBaseEntity, string> confirmMessageCallback)
    {
        ConfirmMessageCallback = confirmMessageCallback;

        return AddConfirmationPopup(true);
    }

    public RowAction SetConfirmationMessage<T>(DynamicConfirmationMessage<T> confirmMessageCallback)
        where T : IBaseEntity
    {
        ConfirmMessageCallback = m => confirmMessageCallback((T)m);

        return AddConfirmationPopup(true);
    }

    public RowAction SetIcon(BootstrapIconType icon)
    {
        Icon = icon;

        return this;
    }

    public RowAction SetRoute(DynamicRoute dynamicRoute)
    {
        DynamicRouteCallback = dynamicRoute;

        return this;
    }

    public override string GetRoute(int id)
    {
        if (DynamicRouteCallback != null)
        {
            return DynamicRouteCallback(id);
        }

        return base.GetRoute(id);
    }
}
