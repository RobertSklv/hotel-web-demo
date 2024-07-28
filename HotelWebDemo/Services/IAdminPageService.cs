using HotelWebDemo.Models.Components.Admin.Pages;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebDemo.Services;

public interface IAdminPageService
{
    PageActionButton BackAction(string? area, string? controller, string? action = "Index");

    PageActionButton BackAction(Controller controller, string? action = "Index");

    PageActionButton CreateAction(string? controller, string action = "Create");

    PageActionButton CreateAction(Controller controller, string action = "Create");
}