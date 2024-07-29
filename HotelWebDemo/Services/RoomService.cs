using HotelWebDemo.Data.Repositories;
using HotelWebDemo.Models.Components.Admin.Tables;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;

namespace HotelWebDemo.Services;

public class RoomService : CrudService<Room>, IRoomService
{
    private readonly IHotelService hotelService;
    private readonly IRoomCategoryService roomCategoryService;

    public RoomService(IRoomRepository repository, IHotelService hotelService, IRoomCategoryService roomCategoryService)
        : base(repository)
    {
        this.hotelService = hotelService;
        this.roomCategoryService = roomCategoryService;
    }

    public override Table<Room> CreateListingTable(ListingModel<Room> listingModel, PaginatedList<Room> items)
    {
        return base.CreateListingTable(listingModel, items)
            .SetSelectableOptionsSource(nameof(Room.Hotel), hotelService.GetAll())
            .SetSelectableOptionsSource(nameof(Room.Category), roomCategoryService.GetAll())
            .AddRowActions(null, options => options.IncludesDelete(false));
    }
}
