using System.ComponentModel.DataAnnotations.Schema;

namespace HotelWebDemo.Models.Database;

[Table("Bookings")]
public class Booking : BaseEntity
{
    [ForeignKey(nameof(BookingPaymentId))]
    public BookingPayment BookingPayment { get; set; }

    public int BookingPaymentId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime ExpirationDate { get; set; }

    public List<BookingItem> BookingItems { get; set; }

    public List<BookingCustomer> BookingCustomers { get; set; }
}
