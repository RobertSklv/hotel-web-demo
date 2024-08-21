using HotelWebDemo.Models.Components.Admin.Pages;
using HotelWebDemo.Models.Components.Common;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebDemo.Services;

public class AdminPageService : IAdminPageService
{
    public PageActionButton BackAction(
        string? controller,
        string? area = "Admin",
        string? action = "Index",
        Dictionary<string, object>? requestParameters = null)
    {
        return new()
        {
            Area = area,
            Controller = controller,
            Action = action,
            IsLink = true,
            SortOrder = -1,
            Content = "Back",
            Color = ColorClass.Secondary,
            AlignToLeft = true,
            RequestParameters = requestParameters
        };
    }

    public PageActionButton BackAction(
        Controller controller,
        string? action = "Index",
        Dictionary<string, object>? requestParameters = null)
    {
        string? area = controller.ControllerContext.RouteData.Values["area"] as string;
        string? controllerName = controller.ControllerContext.ActionDescriptor.ControllerName;

        return BackAction(controllerName, area, action, requestParameters);
    }

    public PageActionButton CreateAction(
        string? controller,
        string action = "Create",
        Dictionary<string, object>? requestParameters = null)
    {
        return new()
        {
            Content = "Create New",
            Controller = controller,
            Action = action,
            Color = ColorClass.Primary,
            IsLink = true,
            RequestParameters = requestParameters
        };
    }

    public PageActionButton CreateAction(
        Controller controller,
        string action = "Create",
        Dictionary<string, object>? requestParameters = null)
    {
        string? controllerName = controller.ControllerContext.ActionDescriptor.ControllerName;

        return CreateAction(controllerName, action, requestParameters);
    }
}
