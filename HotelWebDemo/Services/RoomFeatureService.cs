using HotelWebDemo.Data.Repositories;
using HotelWebDemo.Models.Components.Admin.Tables;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.Database.Indexing;
using HotelWebDemo.Models.ViewModels;

namespace HotelWebDemo.Services;

public class RoomFeatureService : CrudService<RoomFeature, RoomFeatureIndex>, IRoomFeatureService
{
    public RoomFeatureService(IRoomFeatureRepository repository) : base(repository)
    {
    }

    public override Table<RoomFeatureIndex> CreateListingTable(ListingModel<RoomFeatureIndex> listingModel, PaginatedList<RoomFeatureIndex> items)
    {
        return base.CreateListingTable(listingModel, items)
            .AddRowActions(null, options => options.SetDeleteConfirmationMessage<RoomFeatureIndex>(
                roomFeature => $"Are you sure you want to remove room feature {roomFeature.Code}? This action cannot be undone."));
    }
}
