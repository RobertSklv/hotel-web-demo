using System.Security.Claims;
using HotelWebDemo.Extensions;
using HotelWebDemo.Models.Components.Admin.Booking;
using HotelWebDemo.Models.Components.Admin.Pages;
using HotelWebDemo.Models.Components.Common;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;
using HotelWebDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebDemo.Areas.Admin.Controllers;

public class BookingController : CrudController<Booking, BookingViewModel>
{
    private readonly new IBookingService service;
    private readonly IHotelService hotelService;
    private readonly IRoomCategoryService categoryService;
    private readonly IAdminUserService adminUserService;

    protected override string DefaultCreateViewName => "Create";

    public BookingController(
        IBookingService service,
        IHotelService hotelService,
        IRoomCategoryService categoryService,
        IAdminPageService adminPageService,
        IAdminUserService adminUserService,
        Serilog.ILogger logger)
        : base(service, adminPageService, logger)
    {
        this.service = service;
        this.hotelService = hotelService;
        this.categoryService = categoryService;
        this.adminUserService = adminUserService;
    }

    public override IActionResult View(int id)
    {
        CreateViewPageActions(id);

        return base.View(id);
    }

    [HttpGet]
    public override IActionResult Create()
    {
        List<Hotel> hotels = hotelService.GetAll();
        ViewData["Hotels"] = hotels;
        ViewData["RoomCategories"] = categoryService.GetAll();

        int currentAdminHotelId = GetCurrentAdminHotelId();
        BookingViewModel? viewModel = TempData.Get<BookingViewModel>(OldModelTempDataKey);

        if (currentAdminHotelId != 0)
        {
            ViewData["FixedHotelId"] = currentAdminHotelId;
            viewModel.Hotel = hotels.FirstOrDefault(e => e.Id == currentAdminHotelId);
        }

        if (!TempData.ContainsKey("StepContext"))
        {
            TempData.Set("StepContext", service.GenerateBookingStepContext(viewModel));
        }

        return base.Create();
    }

    [HttpGet]
    public IActionResult BackToChooseHotel([FromQuery] BookingViewModel viewModel)
    {
        TempData.Set(OldModelTempDataKey, viewModel);
        TempData.Set("StepContext", service.GenerateBookingStepContext(viewModel));

        return RedirectToAction("Create");
    }

    [HttpGet]
    public IActionResult ChooseHotel([FromQuery] BookingViewModel viewModel)
    {
        TempData.Set(OldModelTempDataKey, viewModel);
        TempData.Set("StepContext", service.GenerateBookingStepContext(viewModel, BookingService.ROOM_RESERVATION_STEP_NAME));

        return RedirectToAction("ReserveRooms", viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> ReserveRooms([FromQuery] BookingViewModel viewModel)
    {
        await service.LoadReservedRoomsAndCalculateTotals(viewModel);

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
    public async Task<IActionResult> Contact(BookingViewModel viewModel)
    {
        BookingStepContext bookingStepContext = service.GenerateBookingStepContext(viewModel);

        if (bookingStepContext.GetStep(BookingService.CONTACT_STEP_NAME).Disabled)
        {
            bookingStepContext.ActiveStep = BookingService.ROOM_RESERVATION_STEP_NAME;
            TempData.Set("StepContext", bookingStepContext);
            TempData.Set(OldModelTempDataKey, viewModel);

            return RedirectToAction("Create");
        }

        await service.LoadReservedRoomsAndCalculateTotals(viewModel);
        viewModel.Contact ??= new();

        bookingStepContext.ActiveStep = BookingService.CONTACT_STEP_NAME;
        TempData.Set("StepContext", bookingStepContext);
        TempData.Set(OldModelTempDataKey, viewModel);

        return base.Create();
    }

    [HttpGet]
    public async Task<IActionResult> BookingSummary(BookingViewModel viewModel)
    {
        BookingStepContext bookingStepContext = service.GenerateBookingStepContext(viewModel);

        if (bookingStepContext.GetStep(BookingService.SUMMARY_STEP_NAME).Disabled)
        {
            bookingStepContext.ActiveStep = BookingService.CONTACT_STEP_NAME;
            TempData.Set("StepContext", bookingStepContext);
            TempData.Set(OldModelTempDataKey, viewModel);

            return RedirectToAction("Create");
        }

        await service.LoadReservedRoomsAndCalculateTotals(viewModel);
        viewModel.Hotel = hotelService.Get(viewModel.HotelId);

        bookingStepContext.ActiveStep = BookingService.SUMMARY_STEP_NAME;
        TempData.Set("StepContext", bookingStepContext);
        TempData.Set(OldModelTempDataKey, viewModel);

        return base.Create();
    }

    public override Task<IActionResult> Create(BookingViewModel model)
    {
        model.AdminUser = adminUserService.Get(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

        return base.Create(model);
    }

    private int GetCurrentAdminHotelId()
    {
        return int.Parse(User.Claims.First(c => c.Type == "HotelId").Value);
    }

    private void CreateViewPageActions(int bookingId)
    {
        List<PageActionButton> btnList = GetOrCreatePageActionButtonsList();

        btnList.Add(new PageActionButton
        {
            Content = "Create payment",
            Color = ColorClass.Primary,
            Controller = "Booking",
            Action = "CreatePayment",
            IsLink = true,
            RequestParameters = new()
            {
                { "Id", bookingId }
            }
        });

        btnList.Add(new PageActionButton
        {
            Content = "Cancel",
            Color = ColorClass.Danger,
            Controller = "Booking",
            Action = "Cancel",
            IsLink = false,
            RequestParameters = new()
            {
                { "Id", bookingId }
            }
        });
    }
}
