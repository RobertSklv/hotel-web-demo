using HotelWebDemo.Data.Repositories;
using HotelWebDemo.Models.Components;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.Database.Indexing;
using HotelWebDemo.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HotelWebDemo.Services;

public class RoomFeatureService : CrudService<RoomFeature, RoomFeatureIndex>, IRoomFeatureService
{
    public RoomFeatureService(IRoomFeatureRepository repository) : base(repository)
    {
    }

    public async Task<ListingModel<RoomFeatureIndex>> CreateRoomFeatureListingModel(ViewDataDictionary viewData)
    {
        ListingModel<RoomFeatureIndex> model = new();
        InitializeListingModel(model, viewData);

        PaginatedList<RoomFeatureIndex> items = await List(model);

        Table<RoomFeatureIndex> table = new Table<RoomFeatureIndex>(model, items)
            .SetOrderable(true)
            .SetFilterable(true)
            .AddRowActions(null, options => options.SetDeleteConfirmationMessage<RoomFeatureIndex>(
                hotel => $"Are you sure you want to remove hotel {hotel.Name}? This action cannot be undone."))
            .AddPagination(items);

        model.Table = table;

        return model;
    }

    public override Table<RoomFeatureIndex> CreateListingTable(ListingModel<RoomFeatureIndex> listingModel, PaginatedList<RoomFeatureIndex> items)
    {
        return base.CreateListingTable(listingModel, items)
            .AddRowActions(null, options => options.SetDeleteConfirmationMessage<RoomFeatureIndex>(
                roomFeature => $"Are you sure you want to remove room feature {roomFeature.Code}? This action cannot be undone."));
    }
}
