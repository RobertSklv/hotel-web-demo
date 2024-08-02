using HotelWebDemo.Models.Components.Common;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebDemo.Areas.Admin.Controllers;

public class RoomController : CrudController<Room>
{
    private readonly new IRoomService service;
    private readonly IHotelService hotelService;
    private readonly IRoomCategoryService roomCategoryService;
    private readonly IRoomFeatureService roomFeatureService;

    public RoomController(
        IRoomService service,
        IAdminPageService adminPageService,
        IHotelService hotelService,
        IRoomCategoryService roomCategoryService,
        IRoomFeatureService roomFeatureService,
        Serilog.ILogger logger)
        : base(service, adminPageService, logger)
    {
        this.service = service;
        this.hotelService = hotelService;
        this.roomCategoryService = roomCategoryService;
        this.roomFeatureService = roomFeatureService;
        ListingTitle = "All rooms";
    }

    public override IActionResult Create()
    {
        ViewData["Hotels"] = hotelService.GetAll();
        ViewData["RoomCategories"] = roomCategoryService.GetAll();
        ViewData["RoomFeatures"] = roomFeatureService.GetAll();

        return base.Create();
    }

    public override IActionResult Edit(int? id)
    {
        ViewData["Hotels"] = hotelService.GetAll();
        ViewData["RoomCategories"] = roomCategoryService.GetAll();
        ViewData["RoomFeatures"] = roomFeatureService.GetAll();

        return base.Edit(id);
    }

    protected override async Task<string> MassAction(string massAction, List<int> selectedItemIds)
    {
        if (selectedItemIds.Count != 0)
        {
            if (massAction == "MassEnable")
            {
                await service.MassEnableToggle(selectedItemIds, enable: true);

                AddMessage($"Successfully enabled {selectedItemIds.Count} room(s).", ColorClass.Success);
            }
            else if (massAction == "MassDisable")
            {
                await service.MassEnableToggle(selectedItemIds, enable: false);

                AddMessage($"Successfully disabled {selectedItemIds.Count} room(s).", ColorClass.Success);
            }
        }

        return await base.MassAction(massAction, selectedItemIds);
    }
}
