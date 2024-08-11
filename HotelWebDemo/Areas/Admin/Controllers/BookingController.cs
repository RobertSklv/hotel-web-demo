﻿using HotelWebDemo.Extensions;
using HotelWebDemo.Models.Components.Admin.Booking;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;
using HotelWebDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebDemo.Areas.Admin.Controllers;

public class BookingController : CrudController<Booking, BookingRoomSelectListingModel>
{
    private readonly new IBookingService service;
    private readonly IHotelService hotelService;
    private readonly IRoomCategoryService categoryService;

    protected override string DefaultCreateViewName => "Create";

    public BookingController(
        IBookingService service,
        IHotelService hotelService,
        IRoomCategoryService categoryService,
        IAdminPageService adminPageService,
        Serilog.ILogger logger)
        : base(service, adminPageService, logger)
    {
        this.service = service;
        this.hotelService = hotelService;
        this.categoryService = categoryService;
    }

    [HttpGet]
    public override IActionResult Create()
    {
        ViewData["Hotels"] = hotelService.GetAll();
        ViewData["RoomCategories"] = categoryService.GetAll();
        if (!TempData.ContainsKey("StepContext"))
        {
            BookingRoomSelectListingModel? viewModel = TempData.Get<BookingRoomSelectListingModel>(OldModelTempDataKey);
            TempData.Set("StepContext", service.GenerateBookingStepContext(viewModel));
        }

        return base.Create();
    }

    [HttpGet]
    public IActionResult BackToChooseHotel([FromQuery] BookingRoomSelectListingModel viewModel)
    {
        TempData.Set(OldModelTempDataKey, viewModel);
        TempData.Set("StepContext", service.GenerateBookingStepContext(viewModel));

        return RedirectToAction("Create");
    }

    [HttpGet]
    public IActionResult ChooseHotel([FromQuery] BookingRoomSelectListingModel viewModel)
    {
        TempData.Set(OldModelTempDataKey, viewModel);
        TempData.Set("StepContext", service.GenerateBookingStepContext(viewModel, BookingService.ROOM_RESERVATION_STEP_NAME));

        return RedirectToAction("ReserveRooms", viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> ReserveRooms([FromQuery] BookingRoomSelectListingModel viewModel)
    {
        await service.ConvertReservedRoomIdsIfAny(viewModel);

        BookingStepContext bookingStepContext = service.GenerateBookingStepContext(viewModel);

        if (bookingStepContext.GetStep(BookingService.ROOM_RESERVATION_STEP_NAME).Disabled)
        {
            bookingStepContext.ActiveStep = BookingService.HOTEL_SELECTION_STEP_NAME;
            TempData.Set("StepContext", bookingStepContext);
            TempData.Set(OldModelTempDataKey, viewModel);

            return RedirectToAction("Create");
        }

        bookingStepContext.ActiveStep = BookingService.ROOM_RESERVATION_STEP_NAME;
        TempData.Set("StepContext", bookingStepContext);
        TempData.Set(OldModelTempDataKey, viewModel);

        ViewData["RoomListing"] = await service.CreateRoomListing(viewModel);

        return base.Create();
    }

    [HttpGet]
    public async Task<IActionResult> Contact(BookingRoomSelectListingModel viewModel)
    {
        BookingStepContext bookingStepContext = service.GenerateBookingStepContext(viewModel);

        if (bookingStepContext.GetStep(BookingService.CONTACT_STEP_NAME).Disabled)
        {
            bookingStepContext.ActiveStep = BookingService.ROOM_RESERVATION_STEP_NAME;
            TempData.Set("StepContext", bookingStepContext);
            TempData.Set(OldModelTempDataKey, viewModel);

            return RedirectToAction("Create");
        }

        await service.ConvertReservedRoomIdsIfAny(viewModel);
        viewModel.Contact = new();

        bookingStepContext.ActiveStep = BookingService.CONTACT_STEP_NAME;
        TempData.Set("StepContext", bookingStepContext);
        TempData.Set(OldModelTempDataKey, viewModel);

        return base.Create();
    }

    [HttpGet]
    public async Task<IActionResult> BookingSummary(BookingRoomSelectListingModel viewModel)
    {
        BookingStepContext bookingStepContext = service.GenerateBookingStepContext(viewModel);

        if (bookingStepContext.GetStep(BookingService.SUMMARY_STEP_NAME).Disabled)
        {
            bookingStepContext.ActiveStep = BookingService.CONTACT_STEP_NAME;
            TempData.Set("StepContext", bookingStepContext);
            TempData.Set(OldModelTempDataKey, viewModel);

            return RedirectToAction("Create");
        }

        await service.ConvertReservedRoomIdsIfAny(viewModel);
        viewModel.Hotel = hotelService.Get(viewModel.HotelId);

        bookingStepContext.ActiveStep = BookingService.SUMMARY_STEP_NAME;
        TempData.Set("StepContext", bookingStepContext);
        TempData.Set(OldModelTempDataKey, viewModel);

        return base.Create();
    }
}
