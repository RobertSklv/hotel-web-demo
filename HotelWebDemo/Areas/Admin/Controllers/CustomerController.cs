using HotelWebDemo.Models.Components;
using HotelWebDemo.Models.Components.Admin.Pages;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Services;
using Microsoft.AspNetCore.Mvc;
using StarExplorerMainServer.Areas.Admin.Services;

namespace HotelWebDemo.Areas.Admin.Controllers;

public class CustomerController : AdminController
{
    private readonly ICustomerService service;
    private readonly ICountryService countryService;
    private readonly IAdminPageService adminPageService;

    public CustomerController(ICustomerService service, ICountryService countryService, IAdminPageService adminPageService)
    {
        this.service = service;
        this.countryService = countryService;
        this.adminPageService = adminPageService;
    }

    public async Task<IActionResult> Index(string orderBy = "Id", string direction = "desc", int page = 1, Dictionary<string, TableFilter>? filters = null)
    {
        ViewData["OrderBy"] = orderBy;
        ViewData["Direction"] = direction;
        ViewData["Page"] = page;
        ViewData["Filter"] = filters;

        return View(await service.CreateCustomerListingModel(ViewData));
    }

    public IActionResult Create()
    {
        Customer customer = new()
        {
            CustomerIdentity = new(),
            CustomerAccount = new(),
        };

        ViewData["Countries"] = countryService.GetAll();
        ViewData["PageActions"] = new List<PageActionButton>()
        {
            adminPageService.CreateBackAction(this)
        };

        return View("Upsert", customer);
    }

    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return RedirectToAction("Index");
        }

        Customer? customer = service.GetFull((int)id);

        if (customer == null)
        {
            return NotFound($"Customer with ID {id} doesn't exist.");
        }

        ViewData["Countries"] = countryService.GetAll();
        ViewData["PageActions"] = new List<PageActionButton>()
        {
            adminPageService.CreateBackAction(this)
        };

        return View("Upsert", customer);
    }

    public async Task<IActionResult> Upsert(Customer customer)
    {
        await service.Upsert(customer, ModelState);

        return RedirectToAction("Edit", new { customer.Id });
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return RedirectToAction("Index");
        }

        Customer? customer = service.GetFull((int)id);

        if (customer == null)
        {
            return NotFound($"Customer with ID {id} doesn't exist.");
        }

        await service.Delete((int)id);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword(int id)
    {
        try
        {
            await service.InitiateResetPasswordAndNotify(id);
        }
        catch (Exception e)
        {
            ModelState.AddModelError(string.Empty, e.Message);
        }

        return RedirectToAction("Edit", new { id });
    }
}
