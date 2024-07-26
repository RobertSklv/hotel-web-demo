using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.Database.Indexing;
using HotelWebDemo.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HotelWebDemo.Services;

public interface IHotelService : ICrudService<Hotel, HotelIndex>
{
    Task<ListingModel<HotelIndex>> CreateHotelListingModel(ViewDataDictionary viewData);
}