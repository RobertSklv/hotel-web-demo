using HotelWebDemo.Models.Database;
using HotelWebDemo.Services;
using Microsoft.AspNetCore.Mvc;
using StarExplorerMainServer.Areas.Admin.Services;

namespace HotelWebDemo.Areas.Admin.Controllers;

public class CustomerController : CrudController<Customer>
{
    private readonly new ICustomerService service;
    private readonly ICountryService countryService;

    public CustomerController(ICustomerService service, ICountryService countryService, IAdminPageService adminPageService)
        : base(service, adminPageService)
    {
        this.service = service;
        this.countryService = countryService;

        ListingTitle = "All customers";
    }

    protected override Customer InitializeNew()
    {
        return new()
        {
            CustomerIdentity = new(),
            CustomerAccount = new()
            {
                Address = new()
            },
        };
    }

    protected override Customer? GetEntity(int id)
    {
        return service.GetFull(id);
    }

    public override IActionResult Create()
    {
        ViewData["Countries"] = countryService.GetAll();

        return base.Create();
    }

    public override IActionResult Edit(int? id)
    {
        ViewData["Countries"] = countryService.GetAll();

        return base.Edit(id);
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
