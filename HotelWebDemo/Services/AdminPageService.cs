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
            AreaName = area,
            ControllerName = controller,
            ActionName = action,
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
        string? area,
        string? controller,
        string action = "Create",
        Dictionary<string, object>? requestParameters = null)
    {
        return new()
        {
            Content = "Create New",
            AreaName = area,
            ControllerName = controller,
            ActionName = action,
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
        string? area = controller.ControllerContext.RouteData.Values["area"] as string;
        string? controllerName = controller.ControllerContext.ActionDescriptor.ControllerName;

        return CreateAction(area, controllerName, action, requestParameters);
    }
}
