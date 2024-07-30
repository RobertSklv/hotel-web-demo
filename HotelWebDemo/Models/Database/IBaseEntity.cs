namespace HotelWebDemo.Models.Database;

public interface IBaseEntity : IModel
{
    DateTime CreatedAt { get; set; }

    DateTime UpdatedAt { get; set; }
}