using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Data.Repositories;

public interface IBookingRepository : ICrudRepository<Booking>
{
    Task<BookingCancellation?> GetOrLoadBookingCancellation(Booking booking);

    Task<List<RoomReservation>?> GetOrLoadReservedRooms(Booking booking);
}