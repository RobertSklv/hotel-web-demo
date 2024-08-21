using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Data.Repositories;

public interface IRoomReservationRepository : ICrudRepository<RoomReservation>
{
    Task<CheckinInfo?> GetCurrentCheckinInfo(int roomReservationId);

    Task<CheckinInfo?> GetOrLoadCurrentCheckinInfo(RoomReservation roomReservation);

    Task<List<CheckinInfo>> GetOrLoadCheckinInfos(RoomReservation roomReservation);

    Task<int> Checkout(RoomReservation roomReservation);
}