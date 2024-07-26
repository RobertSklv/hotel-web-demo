namespace HotelWebDemo.Models.Database;

public interface IBaseEntity
{
    int Id { get; set; }

    DateTime CreatedAt { get; set; }

    DateTime UpdatedAt { get; set; }
}