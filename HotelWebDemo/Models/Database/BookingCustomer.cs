using System.ComponentModel.DataAnnotations.Schema;

namespace HotelWebDemo.Models.Database;

[Table("BookingCustomers")]
public class BookingCustomer : BaseEntity
{
    public Customer Customer { get; set; }

    public int CustomerId { get; set; }

    public Booking Booking { get; set; }

    public int BookingId { get; set; }
}
