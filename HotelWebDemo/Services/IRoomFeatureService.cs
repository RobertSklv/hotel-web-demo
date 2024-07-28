using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.Database.Indexing;
using HotelWebDemo.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HotelWebDemo.Services;

public interface IRoomFeatureService : ICrudService<RoomFeature, RoomFeatureIndex>
{
    Task<ListingModel<RoomFeatureIndex>> CreateRoomFeatureListingModel(ViewDataDictionary viewData);
}