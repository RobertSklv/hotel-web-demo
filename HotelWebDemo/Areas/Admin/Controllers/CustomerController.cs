using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.Utilities;
using HotelWebDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebDemo.Areas.Admin.Controllers;

public class CustomerController : AdminController
{
    private readonly ICustomerService service;

    public CustomerController(ICustomerService service)
    {
        this.service = service;
    }

    public async Task<IActionResult> Index(string orderBy = "Id", string direction = "desc", int page = 1)
    {
        ViewData["OrderBy"] = orderBy;
        ViewData["Direction"] = direction;
        ViewData["Page"] = page;

        PaginatedList<Customer> customers = await service.GetCustomers(orderBy, direction, page, 10);

        return View(customers);
    }
}
