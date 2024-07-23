using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Models.Database;

[Table("BookingCustomers")]
public class BookingCustomer : BaseEntity
{
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Customer Customer { get; set; }

    public int CustomerId { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Booking Booking { get; set; }

    public int BookingId { get; set; }
}
