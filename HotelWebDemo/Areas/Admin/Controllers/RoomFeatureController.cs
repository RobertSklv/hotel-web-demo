using HotelWebDemo.Models.Components.Admin.Pages;
using HotelWebDemo.Models.Components;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebDemo.Areas.Admin.Controllers;

public class RoomFeatureController : AdminController
{
    private readonly IRoomFeatureService service;
    private readonly IAdminPageService adminPageService;

    public RoomFeatureController(IRoomFeatureService service, IAdminPageService adminPageService)
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

        return View(await service.CreateListingModel(ViewData));
    }

    public IActionResult Create()
    {
        RoomFeature roomFeature = new();

        AddBackAction();

        return View("Upsert", roomFeature);
    }

    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return RedirectToAction("Index");
        }

        RoomFeature? roomFeature = service.Get((int)id);

        if (roomFeature == null)
        {
            return NotFound($"Room feature with ID {id} doesn't exist.");
        }

        AddBackAction();

        return View("Upsert", roomFeature);
    }

    [HttpPost]
    public async Task<IActionResult> Upsert(RoomFeature roomFeature)
    {
        bool isCreate = roomFeature.Id == 0;

        await service.Upsert(roomFeature);

        if (isCreate)
        {
            return RedirectToAction("Index");
        }

        return RedirectToAction("Edit", new { roomFeature.Id });
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return RedirectToAction("Index");
        }

        RoomFeature? roomFeature = service.Get((int)id);

        if (roomFeature == null)
        {
            return NotFound($"Room feature with ID {id} doesn't exist.");
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
