using HotelWebDemo.Models.Database;
using HotelWebDemo.Services;

namespace HotelWebDemo.Areas.Admin.Controllers;

public class BookingController : CrudController<Booking>
{
    public BookingController(IBookingService service, IAdminPageService adminPageService, Serilog.ILogger logger)
        : base(service, adminPageService, logger)
    {
    }
}
