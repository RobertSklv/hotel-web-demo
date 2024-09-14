using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.Json;
using HotelWebDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebDemo.Areas.Admin.Controllers;

public class RoomCategoryController : CrudController<RoomCategory>
{
    private readonly new IRoomCategoryService service;
    private readonly IHotelService hotelService;

    public RoomCategoryController(
        IRoomCategoryService service,
        IAdminPageService adminPageService,
        IHotelService hotelService,
        Serilog.ILogger logger)
        : base(service, adminPageService, logger)
    {
        ListingTitle = "All room categories";
        this.service = service;
        this.hotelService = hotelService;
    }

    public override async Task<IActionResult> Create()
    {
        ViewData["Hotels"] = await hotelService.GetAll();

        return await base.Create();
    }

    public override async Task<IActionResult> Edit(int id)
    {
        ViewData["Hotels"] = await hotelService.GetAll();

        return await base.Edit(id);
    }

    public List<RoomCategoryOption> GetHotelRoomCategories(int hotelId)
    {
        return service.GetAllAsOptions(hotelId);
    }
}
