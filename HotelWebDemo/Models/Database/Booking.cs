using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotelWebDemo.Models.Attributes;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Models.Database;

[Table("Bookings")]
public class Booking : BaseEntity
{
    [ForeignKey(nameof(BookingPaymentId))]
    public BookingPayment? BookingPayment { get; set; }

    public int? BookingPaymentId { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    public BookingContact? Contact { get; set; }

    public int? ContactId { get; set; }

    [TableColumn]
    [Display(Name = "Check-in date")]
    public DateTime CheckInDate { get; set; }

    [TableColumn]
    [Display(Name = "Check-out date")]
    public DateTime CheckOutDate { get; set; }

    public List<BookingCustomer>? BookingCustomers { get; set; }

    public List<RoomReservation>? ReservedRooms { get; set; }

    [TableColumn(Name = "Contact name")]
    public string? Contact_FullName => Contact?.FullName;

    [TableColumn(Name = "Contact phone")]
    public string? Contact_Phone => Contact?.Phone;

    [TableColumn(Name = "Contact e-mail")]
    public string? Contact_Email => Contact?.Email;
}
