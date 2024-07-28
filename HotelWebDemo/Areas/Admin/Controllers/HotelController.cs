using HotelWebDemo.Models.Database;
using HotelWebDemo.Services;
using HotelWebDemo.Models.Database.Indexing;

namespace HotelWebDemo.Areas.Admin.Controllers;

public class HotelController : CrudController<Hotel, HotelIndex>
{
    private readonly new IHotelService service;

    public HotelController(IHotelService service, IAdminPageService adminPageService)
        : base(service, adminPageService)
    {
        this.service = service;

        ListingTitle = "All Hotels";
    }
}
