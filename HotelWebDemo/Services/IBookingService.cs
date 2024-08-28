using HotelWebDemo.Models.Components.Admin.Booking;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;

namespace HotelWebDemo.Services;

public interface IBookingService : ICrudService<Booking, BookingViewModel>
{
    Task<ListingModel<Room>> CreateRoomListing(BookingViewModel viewModel);

    Task LoadReservedRoomsAndCalculateTotals(BookingViewModel viewModel);

    Task<List<RoomReservation>> CreateRoomReservations(List<int> roomIds);

    BookingStepContext GenerateBookingStepContext(BookingViewModel? viewModel, string? activeStep = null);
}