using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Models.Database;

[Table("BookingPayments")]
public class BookingPayment : BaseEntity
{
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Booking? Booking { get; set; }

    public int BookingId { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Customer? Customer { get; set; }

    public int CustomerId { get; set; }

    [ForeignKey(nameof(BookingCancellationId))]
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public BookingCancellation? BookingCancellation { get; set; }

    public int? BookingCancellationId { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal GrandTotal { get; set; }

    [StringLength(64)]
    public string StreetLine1 { get; set; }

    [StringLength(64)]
    public string StreetLine2 { get; set; }

    [StringLength(64)]
    public string StreetLine3 { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Country? Country { get; set; }

    public int CountryId { get; set; }

    [StringLength(32)]
    public string City { get; set; }

    [StringLength(16)]
    public string PostalCode { get; set; }

    [StringLength(16)]
    public string Phone { get; set; }

    public bool IsCancelled => BookingCancellationId != null;
}
