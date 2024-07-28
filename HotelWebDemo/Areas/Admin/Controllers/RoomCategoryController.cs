using HotelWebDemo.Models.Database;
using HotelWebDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebDemo.Areas.Admin.Controllers;

public class RoomCategoryController : CrudController<RoomCategory>
{
    private readonly IHotelService hotelService;

    public RoomCategoryController(IRoomCategoryService service, IAdminPageService adminPageService, IHotelService hotelService)
        : base(service, adminPageService)
    {
        ListingTitle = "All room categories";
        this.hotelService = hotelService;
    }

    public override IActionResult Create()
    {
        ViewData["Hotels"] = hotelService.GetAll();

        return base.Create();
    }

    public override IActionResult Edit(int? id)
    {
        ViewData["Hotels"] = hotelService.GetAll();

        return base.Edit(id);
    }
}
