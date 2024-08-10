using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Models.Database;

[Table("Bookings")]
public class Booking : BaseEntity
{
    [ForeignKey(nameof(BookingPaymentId))]
    public BookingPayment? BookingPayment { get; set; }

    public int? BookingPaymentId { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    public BookingContact Contact { get; set; }

    public int ContactId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime ExpirationDate { get; set; }

    public List<BookingCustomer>? BookingCustomers { get; set; }

    public List<RoomReservation>? ReservedRooms { get; set; }
}
