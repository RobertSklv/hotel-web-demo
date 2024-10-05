using HotelWebDemo.Models.Components.Admin.Booking;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;

namespace HotelWebDemo.Services;

public interface IBookingService : ICrudService<Booking, BookingViewModel>
{
    Task<ListingModel<Room>> CreateRoomListing(BookingViewModel viewModel);

    Task LoadReservedRoomsAndCalculateTotals(BookingViewModel viewModel);

    Task GenerateReservationsAndBookingItems(BookingViewModel viewModel, Booking booking);

    BookingStepContext GenerateBookingStepContext(BookingViewModel? viewModel, string? activeStep = null);

    Task<bool> IsNoShow(Booking booking);

    Task<bool> IsPendingCheckin(Booking booking);

    Task<bool> IsPendingCheckout(Booking booking);

    Task<bool> IsCheckedIn(Booking booking);

    Task<bool> IsCheckedOut(Booking booking);

    Task<BookingStatus> GetStatus(Booking booking);

    Task<bool> CanBeCancelled(Booking booking);

    Task<bool> Cancel(int bookingId, BookingCancellation cancellation);
}