using HotelWebDemo.Models.Components.Admin.Pages;
using HotelWebDemo.Models.Components.Common;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebDemo.Services;

public class AdminPageService : IAdminPageService
{
    public PageActionButton CreateBackAction(string? controller, string? area = "Admin", string? action = "Index")
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
        };
    }

    public PageActionButton CreateBackAction(Controller controller, string? action = "Index")
    {
        string? area = controller.ControllerContext.RouteData.Values["area"] as string;
        string? controllerName = controller.ControllerContext.ActionDescriptor.ControllerName;

        return CreateBackAction(controllerName, area, action);
    }
}
