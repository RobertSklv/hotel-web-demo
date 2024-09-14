using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;

namespace HotelWebDemo.Data.Repositories;

public interface IRoomReservationRepository : ICrudRepository<RoomReservation>
{
    Task<CheckinInfo?> GetOrLoadCurrentCheckinInfo(RoomReservation roomReservation);

    Task<int> Checkout(RoomReservation roomReservation);

    Task<List<RoomReservation>> GetReservationsForPeriod(DateTime from, DateTime to);

    Task<List<int>> GetReservedRoomIdsForPeriod(DateTime from, DateTime to);

    Task<List<RoomReservation>> GetAllReservations(int roomId, bool? active = null);

    Task<RoomReservation?> GetCheckedInReservation(int roomId);

    Task<PaginatedList<Room>> GetBookableRooms(BookingViewModel listingModel);

    Task<PaginatedList<Room>> GetBookableRooms(ListingModel listingModel, RoomReservation roomReservation);

    Task<PaginatedList<Room>> GetBookableRooms(ListingModel listingModel, int roomReservationId);
}