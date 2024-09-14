using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;

namespace HotelWebDemo.Services;

public interface IRoomReservationService : ICrudService<RoomReservation>
{
    Task<CheckinInfo?> GetOrLoadCurrentCheckinInfo(RoomReservation roomReservation);

    Task PrepareNewCheckin(RoomReservation roomReservation);

    Task<bool> Checkout(RoomReservation roomReservation);

    Task<bool> ChangeRoom(int roomReservationId, int roomId);

    Task<ChangeRoomViewModel> CreateChangeRoomListing(ListingModel listingModel, int roomReservationId);

    Task<List<RoomReservation>> GetAllReservations(int roomId, bool? active = null);

    Task<RoomReservation?> GetCheckedInReservation(int roomId);

    Task<PaginatedList<Room>> GetBookableRooms(ListingModel listingModel, int roomReservationId);

    Task<PaginatedList<Room>> GetBookableRooms(BookingViewModel listingModel);

    Task<PaginatedList<Room>> GetBookableRooms(ListingModel listingModel, RoomReservation roomReservation);
}