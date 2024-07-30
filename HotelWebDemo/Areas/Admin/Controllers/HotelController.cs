using HotelWebDemo.Models.Database;
using HotelWebDemo.Services;

namespace HotelWebDemo.Areas.Admin.Controllers;

public class HotelController : CrudController<Hotel>
{
    private readonly new IHotelService service;

    public HotelController(IHotelService service, IAdminPageService adminPageService, Serilog.ILogger logger)
        : base(service, adminPageService, logger)
    {
        this.service = service;

        ListingTitle = "All Hotels";
    }
}
