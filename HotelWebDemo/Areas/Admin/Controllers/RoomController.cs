using HotelWebDemo.Models.Database;
using HotelWebDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebDemo.Areas.Admin.Controllers;

public class RoomController : CrudController<Room>
{
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
}
