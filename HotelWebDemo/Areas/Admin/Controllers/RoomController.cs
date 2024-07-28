using HotelWebDemo.Models.Database;
using HotelWebDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebDemo.Areas.Admin.Controllers;

public class RoomController : CrudController<Room>
{
    private readonly IHotelService hotelService;
    private readonly IRoomCategoryService roomCategoryService;

    public RoomController(IRoomService service, IAdminPageService adminPageService, IHotelService hotelService, IRoomCategoryService roomCategoryService)
        : base(service, adminPageService)
    {
        this.hotelService = hotelService;
        this.roomCategoryService = roomCategoryService;

        ListingTitle = "All rooms";
    }

    public override IActionResult Create()
    {
        ViewData["Hotels"] = hotelService.GetAll();
        ViewData["RoomCategories"] = roomCategoryService.GetAll();

        return base.Create();
    }

    public override IActionResult Edit(int? id)
    {
        ViewData["Hotels"] = hotelService.GetAll();
        ViewData["RoomCategories"] = roomCategoryService.GetAll();

        return base.Edit(id);
    }
}
