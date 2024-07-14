using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.Utilities;
using HotelWebDemo.Services;
using Microsoft.AspNetCore.Mvc;
using StarExplorerMainServer.Areas.Admin.Services;

namespace HotelWebDemo.Areas.Admin.Controllers;

public class CustomerController : AdminController
{
    private readonly ICustomerService service;
    private readonly ICountryService countryService;

    public CustomerController(ICustomerService service, ICountryService countryService)
    {
        this.service = service;
        this.countryService = countryService;
    }

    public async Task<IActionResult> Index(string orderBy = "Id", string direction = "desc", int page = 1)
    {
        ViewData["OrderBy"] = orderBy;
        ViewData["Direction"] = direction;
        ViewData["Page"] = page;

        PaginatedList<Customer> customers = await service.GetCustomers(orderBy, direction, page, 10);

        return View(customers);
    }

    public IActionResult Create()
    {
        Customer customer = new()
        {
            CustomerIdentity = new(),
            CustomerAccount = new(),
        };

        ViewData["Countries"] = countryService.GetAll();

        return View("Upsert", customer);
    }

    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return RedirectToAction("Index");
        }

        Customer? customer = service.Get((int)id);

        if (customer == null)
        {
            return NotFound($"Customer with ID {id} doesn't exist.");
        }

        ViewData["Countries"] = countryService.GetAll();

        return View("Upsert", customer);
    }

    public async Task<IActionResult> Upsert(Customer customer)
    {
        int recordsSaved = await service.Upsert(customer);

        if (recordsSaved == 0)
        {
            return BadRequest($"Failed to save customer: {customer.Id}.");
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return RedirectToAction("Index");
        }

        Customer? customer = service.Get((int)id);

        if (customer == null)
        {
            return NotFound($"Customer with ID {id} doesn't exist.");
        }

        await service.Delete((int)id);

        return RedirectToAction("Index");
    }
}
