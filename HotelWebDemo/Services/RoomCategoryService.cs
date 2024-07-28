using HotelWebDemo.Data.Repositories;
using HotelWebDemo.Models.Components;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;

namespace HotelWebDemo.Services;

public class RoomCategoryService : CrudService<RoomCategory>, IRoomCategoryService
{
    private readonly IHotelService hotelService;

    public RoomCategoryService(IRoomCategoryRepository repository, IHotelService hotelService)
        : base(repository)
    {
        this.hotelService = hotelService;
    }

    public override Table<RoomCategory> CreateListingTable(ListingModel<RoomCategory> listingModel, PaginatedList<RoomCategory> items)
    {
        return base.CreateListingTable(listingModel, items)
            .SetSelectableOptionsSource("Hotel", hotelService.GetAll());
    }
}
