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

    public override Table<RoomFeature> CreateListingTable(ListingModel<RoomFeature> listingModel, PaginatedList<RoomFeature> items)
    {
        return base.CreateListingTable(listingModel, items)
            .SetSearchable(false)
            .SetSelectableOptionsSource(nameof(RoomFeature.Hotel), hotelService.GetAll())
            .AddRowActions(null, options => options.SetDeleteConfirmationMessage<RoomFeature>(
                roomFeature => $"Are you sure you want to remove room feature {roomFeature.Code}? This action cannot be undone."));
    }
}
