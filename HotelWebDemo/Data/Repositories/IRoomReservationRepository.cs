using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;

namespace HotelWebDemo.Data.Repositories;

public interface IRoomReservationRepository : ICrudRepository<RoomReservation>
{
    Task<CheckinInfo?> GetOrLoadCurrentCheckinInfo(RoomReservation roomReservation);

    Task<int> Checkout(RoomReservation roomReservation);

    Task<List<RoomReservation>> GetReservationsForPeriod(DateTime from, DateTime to);

    Task<List<int>> GetReservedRoomIdsForPeriod(DateTime from, DateTime to);

    IQueryable<RoomReservation> AddRoomIdFilter(IQueryable<RoomReservation> query, int roomId);

    IQueryable<RoomReservation> AddActiveReservationFilter(IQueryable<RoomReservation> query, bool active);

    Task<List<RoomReservation>> GetReservationsForRoom(int roomId, bool? active = null);

    Task<RoomReservation?> GetCheckedInReservationForRoom(int roomId);

    Task<PaginatedList<Room>> GetBookableRooms(BookingViewModel listingModel);

    Task<PaginatedList<Room>> GetBookableRooms(ListingModel listingModel, RoomReservation roomReservation);

    Task<PaginatedList<Room>> GetBookableRooms(ListingModel listingModel, int roomReservationId);

    Task<PaginatedList<RoomReservation>> GetReservationsForRoomPaginated(
        ListingModel listingModel,
        int roomId,
        bool? active = null);
}