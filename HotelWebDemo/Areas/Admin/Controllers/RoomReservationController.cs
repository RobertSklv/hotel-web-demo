using HotelWebDemo.Models.Components.Common;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Services;
using Microsoft.AspNetCore.Mvc;
using StarExplorerMainServer.Areas.Admin.Services;

namespace HotelWebDemo.Areas.Admin.Controllers;

public class RoomReservationController : CrudController<RoomReservation>
{
    private readonly new IRoomReservationService service;
    private readonly ICountryService countryService;

    public RoomReservationController(
        IRoomReservationService service,
        IAdminPageService adminPageService,
        Serilog.ILogger logger,
        ICountryService countryService)
        : base(service, adminPageService, logger)
    {
        this.service = service;
        this.countryService = countryService;
    }

    [HttpGet]
    public async Task<IActionResult> Checkin(int id)
    {
        RoomReservation? roomReservation = await GetViewModel(id);
        if (roomReservation != null)
        {
            await service.PrepareNewCheckin(roomReservation);

            ViewData["Countries"] = countryService.GetAll();

            AddBackAction(
                nameof(BookingController.View),
                "Booking",
                requestParameters: new()
                {
                    { "Id", roomReservation.BookingId }
                });

            return View(roomReservation);
        }

        return RedirectToRoute("/Admin/Booking");
    }

    [HttpPost]
    public async Task<IActionResult> Checkin(RoomReservation roomReservation)
    {
        bool success = await service.Upsert(roomReservation);

        if (!success)
        {
            AddMessage("Failed to check-in. An unknown error occurred.", ColorClass.Danger);
        }

        return RedirectToRoute($"/Admin/Booking/View/{roomReservation.BookingId}");
    }

    [HttpPost]
    public async Task<IActionResult> Checkout(RoomReservation roomReservation)
    {
        bool success = await service.Checkout(roomReservation);

        if (!success)
        {
            AddMessage("Failed to check-out. An unknown error occurred.", ColorClass.Danger);
        }

        return RedirectToRoute($"/Admin/Booking/View/{roomReservation.BookingId}");
    }
}
