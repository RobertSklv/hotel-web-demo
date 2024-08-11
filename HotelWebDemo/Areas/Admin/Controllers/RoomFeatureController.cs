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

    public override IActionResult Create()
    {
        ViewData["Hotels"] = hotelService.GetAll();

        return base.Create();
    }

    public override IActionResult Edit(int id)
    {
        ViewData["Hotels"] = hotelService.GetAll();

        return base.Edit(id);
    }
}
