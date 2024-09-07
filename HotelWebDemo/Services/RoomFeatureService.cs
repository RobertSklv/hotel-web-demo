using HotelWebDemo.Data.Repositories;
using HotelWebDemo.Models.Components.Admin.Tables;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;

namespace HotelWebDemo.Services;

public class RoomFeatureService : CrudService<RoomFeature>, IRoomFeatureService
{
    private readonly IHotelService hotelService;

    public RoomFeatureService(IRoomFeatureRepository repository, IHotelService hotelService)
        : base(repository)
    {
        this.hotelService = hotelService;
    }

    public override async Task<Table<RoomFeature>> CreateListingTable(ListingModel<RoomFeature> listingModel, PaginatedList<RoomFeature> items)
    {
        return (await base.CreateListingTable(listingModel, items))
            .SetSearchable(false)
            .SetSelectableOptionsSource(nameof(RoomFeature.Hotel), await hotelService.GetAll());
    }

    public override RowAction CustomizeDeleteRowAction(RowAction action)
    {
        return base.CustomizeDeleteRowAction(action)
            .SetConfirmationMessage<RoomFeature>(roomFeature => $"Are you sure you want to remove room feature {roomFeature.Code}? This action cannot be undone.");
    }
}
