using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HotelWebDemo.Services;

public interface IHotelService : ICrudService<Hotel>
{
    Task<ListingModel<Hotel>> CreateHotelListingModel(ViewDataDictionary viewData);
}