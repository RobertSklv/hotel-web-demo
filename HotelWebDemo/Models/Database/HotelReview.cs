using System.ComponentModel.DataAnnotations.Schema;

namespace HotelWebDemo.Models.Database;

[Table("HotelReviews")]
public class HotelReview : Review
{
    public Hotel Hotel { get; set; }

    public int HotelId { get; set; }
}
