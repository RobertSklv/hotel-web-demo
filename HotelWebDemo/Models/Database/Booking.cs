using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotelWebDemo.Models.Attributes;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Models.Database;

[Table("Bookings")]
public class Booking : BaseEntity
{
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public BookingContact? Contact { get; set; }

    public int? ContactId { get; set; }

    [TableColumn]
    [Display(Name = "Check-in date")]
    public DateTime CheckInDate { get; set; }

    [TableColumn]
    [Display(Name = "Check-out date")]
    public DateTime CheckOutDate { get; set; }

    [ForeignKey(nameof(TotalsId))]
    public BookingTotals? Totals { get; set; }

    public int? TotalsId { get; set; }

    [ForeignKey(nameof(BookingPaymentId))]
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public BookingPayment? BookingPayment { get; set; }

    public int? BookingPaymentId { get; set; }

    [ForeignKey(nameof(BookingCancellationId))]
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public BookingCancellation? BookingCancellation { get; set; }

    public int? BookingCancellationId { get; set; }

    [StringLength(32)]
    [Column(TypeName = "VARCHAR")]
    [TableColumn(Name = "Status")]
    public string BookingStatus { get; set; }

    public List<BookingItem>? BookingItems { get; set; }

    public List<BookingCustomer>? BookingCustomers { get; set; }

    public List<RoomReservation>? ReservedRooms { get; set; }

    public List<BookingEventLog>? BookingTimeline { get; set; }

    [TableColumn(Name = "Contact name")]
    public string? Contact_FullName => Contact?.FullName;

    [TableColumn(Name = "Contact phone")]
    public string? Contact_Phone => Contact?.Phone;

    [TableColumn(Name = "Contact e-mail")]
    public string? Contact_Email => Contact?.Email;

    [NotMapped]
    public BookingStatus Status
    {
        get => Enum.TryParse(BookingStatus, out BookingStatus result) ? result : default;
        set => BookingStatus = value.ToString();
    }

    public bool IsCancelled => BookingCancellationId != null;

    public bool IsPaid => BookingPayment != null;
}
