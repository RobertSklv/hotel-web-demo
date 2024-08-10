using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Models.ViewModels;

public interface IBookingViewModel : IModel
{
    int Id { get; set; }

    Hotel Hotel { get; set; }

    int HotelId { get; set; }

    List<int>? RoomsToReserve { get; set; }
}
