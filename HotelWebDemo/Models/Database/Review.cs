using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Models.Database;

[Table("Reviews")]
public class Review : BaseEntity
{
    public CustomerAccount Author { get; set; }

    public int AuthorId { get; set; }

    public Booking Booking { get; set; }

    public int BookingId { get; set; }

    [StringLength(64, MinimumLength = 3)]
    public string Title { get; set; }

    [StringLength(2048, MinimumLength = 10)]
    public string Content { get; set; }

    [Range(0, 5)]
    public int Rate { get; set; }
}
