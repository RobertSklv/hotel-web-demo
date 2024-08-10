using HotelWebDemo.Extensions;
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
        if (TempData["Step"] == null)
        {
            TempData["Step"] = 0;
        }

        return base.Create();
    }

    [HttpGet]
    public IActionResult ChooseHotel(int hotelId, DateTime startDate, DateTime expirationDate)
    {
        BookingRoomSelectListingModel viewModel = new()
        {
            HotelId = hotelId,
            StartDate = startDate,
            ExpirationDate = expirationDate
        };
        TempData.Set(OldModelTempDataKey, viewModel);

        TempData["Step"] = 1;

        return RedirectToAction("ReserveRooms", viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> ReserveRooms([FromQuery] BookingRoomSelectListingModel viewModel)
    {
        ViewData["Hotels"] = hotelService.GetAll();
        ViewData["RoomCategories"] = categoryService.GetAll();
        await service.ConvertReservedRoomIdsIfAny(viewModel);
        TempData.Set(OldModelTempDataKey, viewModel);

        if (viewModel.HotelId == 0 || viewModel.StartDate == default || viewModel.ExpirationDate == default)
        {
            TempData["Step"] = 0;

            return RedirectToAction("Create");
        }

        TempData["Step"] = 1;

        ViewData["RoomListing"] = await service.CreateRoomListing(viewModel);

        return base.Create();
    }

    [HttpPost]
    public IActionResult BookingSummary(BookingRoomSelectListingModel viewModel)
    {
        service.ConvertReservedRoomIdsIfAny(viewModel);

        TempData["Step"] = 2;

        return base.Create();
    }
}
