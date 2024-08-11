﻿using HotelWebDemo.Models.Components.Admin.Booking;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;

namespace HotelWebDemo.Services;

public interface IBookingService : ICrudService<Booking, BookingRoomSelectListingModel>
{
    Task<ListingModel<Room>> CreateRoomListing(BookingRoomSelectListingModel viewModel);

    Task ConvertReservedRoomIdsIfAny(BookingRoomSelectListingModel viewModel);

    RoomReservation CreateRoomReservation(int roomId);

    BookingStepContext GenerateBookingStepContext(BookingRoomSelectListingModel? viewModel, string? activeStep = null);
}