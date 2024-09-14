using HotelWebDemo.Models.Database;
using HotelWebDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebDemo.Areas.Admin.Controllers;

public class RoomFeatureController : CrudController<RoomFeature>
{
    private readonly IHotelService hotelService;

    public RoomFeatureController(
        IRoomFeatureService service,
        IAdminPageService adminPageService,
        IHotelService hotelService,
        Serilog.ILogger logger)
        : base(service, adminPageService, logger)
    {
        ListingTitle = "All room features";
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
}
