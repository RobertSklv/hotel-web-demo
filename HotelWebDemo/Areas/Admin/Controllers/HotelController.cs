using HotelWebDemo.Models.Components.Admin.Pages;
using HotelWebDemo.Models.Components;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebDemo.Areas.Admin.Controllers;

public class HotelController : AdminController
{
    private readonly IHotelService service;
    private readonly IAdminPageService adminPageService;

    public HotelController(IHotelService service, IAdminPageService adminPageService)
    {
        this.service = service;
        this.adminPageService = adminPageService;
    }

    public async Task<IActionResult> Index(string orderBy = "Id", string direction = "desc", int page = 1, Dictionary<string, TableFilter>? filters = null)
    {
        ViewData["OrderBy"] = orderBy;
        ViewData["Direction"] = direction;
        ViewData["Page"] = page;
        ViewData["Filter"] = filters;

        return View(await service.CreateHotelListingModel(ViewData));
    }

    public IActionResult Create()
    {
        Hotel hotel = new();

        AddBackAction();

        return View("Upsert", hotel);
    }

    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return RedirectToAction("Index");
        }

        Hotel? hotel = service.Get((int)id);

        if (hotel == null)
        {
            return NotFound($"Hotel with ID {id} doesn't exist.");
        }

        AddBackAction();

        return View("Upsert", hotel);
    }

    [HttpPost]
    public async Task<IActionResult> Upsert(Hotel hotel)
    {
        bool isCreate = hotel.Id == 0;

        await service.Upsert(hotel);

        if (isCreate)
        {
            return RedirectToAction("Index");
        }

        return RedirectToAction("Edit", new { hotel.Id });
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return RedirectToAction("Index");
        }

        Hotel? hotel = service.Get((int)id);

        if (hotel == null)
        {
            return NotFound($"Hotel with ID {id} doesn't exist.");
        }

        await service.Delete((int)id);

        return RedirectToAction("Index");
    }

    private void AddBackAction()
    {
        ViewData["PageActions"] = new List<PageActionButton>()
        {
            adminPageService.CreateBackAction(this)
        };
    }
}
