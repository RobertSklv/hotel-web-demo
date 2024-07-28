using HotelWebDemo.Models.Database;
using HotelWebDemo.Services;
using HotelWebDemo.Models.Database.Indexing;

namespace HotelWebDemo.Areas.Admin.Controllers;

public class RoomFeatureController : CrudController<RoomFeature, RoomFeatureIndex>
{
    public RoomFeatureController(IRoomFeatureService service, IAdminPageService adminPageService)
        : base(service, adminPageService)
    {
        ListingTitle = "All room features";
    }
}
