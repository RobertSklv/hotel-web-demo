using HotelWebDemo.Models.Components.Common;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebDemo.Areas.Admin.Controllers;

public class RoomController : CrudController<Room>
{
    private readonly new IRoomService service;
    private readonly IRoomReservationService roomReservationService;
    private readonly IHotelService hotelService;
    private readonly IRoomCategoryService roomCategoryService;
    private readonly IRoomFeatureService roomFeatureService;

    public RoomController(
        IRoomService service,
        IRoomReservationService roomReservationService,
        IAdminPageService adminPageService,
        IHotelService hotelService,
        IRoomCategoryService roomCategoryService,
        IRoomFeatureService roomFeatureService,
        Serilog.ILogger logger)
        : base(service, adminPageService, logger)
    {
        this.service = service;
        this.roomReservationService = roomReservationService;
        this.hotelService = hotelService;
        this.roomCategoryService = roomCategoryService;
        this.roomFeatureService = roomFeatureService;
        ListingTitle = "All rooms";
    }

    public override async Task<IActionResult> Create()
    {
        ViewData["Hotels"] = await hotelService.GetAll();
        ViewData["RoomCategories"] = await roomCategoryService.GetAll();
        ViewData["RoomFeatures"] = await roomFeatureService.GetAll();

        return await base.Create();
    }

    public override async Task<IActionResult> Edit(int id)
    {
        ViewData["Hotels"] = await hotelService.GetAll();
        ViewData["RoomCategories"] = await roomCategoryService.GetAll();
        ViewData["RoomFeatures"] = await roomFeatureService.GetAll();
        ViewData["ActiveReservations"] = await roomReservationService.GetAllReservations(id, active: true);

        return await base.Edit(id);
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
