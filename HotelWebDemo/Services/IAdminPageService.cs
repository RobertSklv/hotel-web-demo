using HotelWebDemo.Models.Components.Admin.Pages;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebDemo.Services;

public interface IAdminPageService
{
    PageActionButton CreateBackAction(string? area, string? controller, string? action = "Index");

    PageActionButton CreateBackAction(Controller controller, string? action = "Index");
}