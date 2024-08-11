using HotelWebDemo.Models.Components.Common;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;
using HotelWebDemo.Services;
using Microsoft.AspNetCore.Mvc;
using StarExplorerMainServer.Areas.Admin.Services;

namespace HotelWebDemo.Areas.Admin.Controllers;

public class CustomerController : CrudController<Customer, CustomerViewModel>
{
    private readonly new ICustomerService service;
    private readonly ICountryService countryService;

    public CustomerController(
        ICustomerService service,
        ICountryService countryService,
        IAdminPageService adminPageService,
        Serilog.ILogger logger)
        : base(service, adminPageService, logger)
    {
        this.service = service;
        this.countryService = countryService;

        ListingTitle = "All customers";
    }

    public override async Task UpsertMethod(CustomerViewModel model) => await service.Upsert(model, ModelState);

    protected override Customer? GetEntity(int id)
    {
        return service.GetFull(id);
    }

    public override IActionResult Create()
    {
        ViewData["Countries"] = countryService.GetAll();

        return base.Create();
    }

    public override IActionResult Edit(int id)
    {
        ViewData["Countries"] = countryService.GetAll();

        CreateResetPasswordAction(id);

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

    private void CreateResetPasswordAction(int customerId)
    {
        GetOrCreatePageActionButtonsList().Add(new()
        {
            Content = "Reset password",
            Controller = "Customer",
            Action = "ResetPassword",
            Color = ColorClass.Warning,
            RequestParameters = new()
            {
                { "Id", customerId }
            }
        });
    }
}
