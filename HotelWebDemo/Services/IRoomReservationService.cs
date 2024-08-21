using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Services;

public interface IRoomReservationService : ICrudService<RoomReservation>
{
    Task<CheckinInfo?> GetOrLoadCurrentCheckinInfo(RoomReservation roomReservation);

    Task PrepareNewCheckin(RoomReservation roomReservation);

    Task<bool> Checkout(RoomReservation roomReservation);
}