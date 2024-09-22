using HotelWebDemo.Models.Components.Admin.Pages;
using HotelWebDemo.Models.Components.Common;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebDemo.Services;

public interface IAdminPageService
{
    PageActionButton BackAction(
        string? controller,
        string? area = "Admin",
        string? action = "Index",
        Dictionary<string, object>? requestParameters = null);

    PageActionButton BackAction(
        Controller controller,
        string? action = "Index",
        Dictionary<string, object>? requestParameters = null);

    PageActionButton CreateAction(
        string? area,
        string? controller,
        string action = "Create",
        Dictionary<string, object>? requestParameters = null);

    PageActionButton CreateAction(
        Controller controller,
        string action = "Create",
        Dictionary<string, object>? requestParameters = null);
}