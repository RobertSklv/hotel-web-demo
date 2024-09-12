using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;

namespace HotelWebDemo.Services;

public interface IRoomReservationService : ICrudService<RoomReservation>
{
    Task<CheckinInfo?> GetOrLoadCurrentCheckinInfo(RoomReservation roomReservation);

    Task PrepareNewCheckin(RoomReservation roomReservation);

    Task<bool> Checkout(RoomReservation roomReservation);

    Task<bool> ChangeRoom(int roomReservationId, int roomId);

    Task<ListingModel<Room>> CreateChangeRoomListing(ListingModel listingModel, int roomReservationId);

    Task<RoomReservation?> GetCheckedInReservation(int roomId);

    Task<PaginatedList<Room>> GetBookableRooms(ListingModel listingModel, int roomReservationId);
}