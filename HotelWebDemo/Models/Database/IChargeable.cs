namespace HotelWebDemo.Models.Database;

public interface IChargeable
{
    public decimal Price { get; set; }

    public bool IsPricePerNight { get; set; }
}