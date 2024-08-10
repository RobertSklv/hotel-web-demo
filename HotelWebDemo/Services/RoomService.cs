using HotelWebDemo.Data.Repositories;
using HotelWebDemo.Models.Components.Admin.Tables;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;

namespace HotelWebDemo.Services;

public class RoomService : CrudService<Room>, IRoomService
{
    private readonly IRoomRepository repository;
    private readonly IHotelService hotelService;
    private readonly IRoomCategoryService roomCategoryService;

    public RoomService(IRoomRepository repository, IHotelService hotelService, IRoomCategoryService roomCategoryService)
        : base(repository)
    {
        this.repository = repository;
        this.hotelService = hotelService;
        this.roomCategoryService = roomCategoryService;
    }

    public override Table<Room> CreateListingTable(ListingModel<Room> listingModel, PaginatedList<Room> items)
    {
        return base.CreateListingTable(listingModel, items)
            .SetAdjustablePageSize(true)
            .SetSelectableOptionsSource(nameof(Room.Hotel), hotelService.GetAll())
            .SetSelectableOptionsSource(nameof(Room.Category), roomCategoryService.GetAll())
            .AddMassAction("MassEnable", "Enable selected")
            .AddMassAction("MassDisable", "Disable selected");
    }

    public override Table<Room> CreateDeleteRowAction(Table<Room> table)
    {
        return table;
    }

    public async Task<int> MassEnableToggle(List<int> selectedItemIds, bool enable)
    {
        return await repository.MassEnableToggle(selectedItemIds, enable);
    }

    public async Task<PaginatedList<Room>> GetBookableRooms(BookingRoomSelectListingModel listingModel)
    {
        return await repository.GetBookableRooms(listingModel);
    }
}
