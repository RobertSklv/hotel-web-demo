using HotelWebDemo.Data.Repositories;
using HotelWebDemo.Models.Components.Admin.Tables;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;

namespace HotelWebDemo.Services;

public class BookingService : CrudService<Booking, BookingRoomSelectListingModel>, IBookingService
{
    private readonly IRoomService roomService;

    public BookingService(IBookingRepository repository, IRoomService roomService)
        : base(repository)
    {
        this.roomService = roomService;
    }

    public async Task<ListingModel<Room>> CreateRoomListing(BookingRoomSelectListingModel? viewModel)
    {
        BookingRoomSelectListingModel listingModel = new();
        listingModel.Copy(viewModel);
        listingModel.ActionName = "ReserveRooms";

        PaginatedList<Room> items = await roomService.GetBookableRooms(listingModel);

        listingModel.Table = new Table<Room>(listingModel, items)
            .SetOrderable(true)
            .SetFilterable(true)
            .AddPagination(true)
            .RemoveColumn("Id")
            .RemoveColumn("Enabled")
            .RemoveColumn("Hotel")
            .RemoveColumn("CreatedAt")
            .RemoveColumn("UpdatedAt")
            .AddRowAction("Reserve");

        return listingModel;
    }

    public override BookingRoomSelectListingModel EntityToViewModel(Booking entity)
    {
        return new BookingRoomSelectListingModel
        {
            StartDate = entity.StartDate,
            ExpirationDate = entity.ExpirationDate,
        };
    }

    public override Booking ViewModelToEntity(BookingRoomSelectListingModel model)
    {
        return new Booking
        {
            StartDate = model.StartDate,
            ExpirationDate = model.ExpirationDate,
        };
    }

    public async Task ConvertReservedRoomIdsIfAny(BookingRoomSelectListingModel viewModel)
    {
        if (viewModel.RoomsToReserve == null || viewModel.RoomsToReserve.Count == 0)
        {
            return;
        }

        viewModel.ReservedRooms = await roomService.GetByIds(viewModel.RoomsToReserve);
    }

    public RoomReservation CreateRoomReservation(int roomId)
    {
        Room room = roomService.Get(roomId) ?? throw new Exception($"No such room with ID {roomId} was found in the database.");

        return new RoomReservation
        {
            RoomId = roomId,
            Room = room,
        };
    }
}
