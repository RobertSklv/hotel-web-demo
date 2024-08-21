using HotelWebDemo.Models.Components.Common;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebDemo.Areas.Admin.Controllers;

public class RoomReservationController : CrudController<RoomReservation>
{
    private readonly new IRoomReservationService service;

    public RoomReservationController(IRoomReservationService service, IAdminPageService adminPageService, Serilog.ILogger logger)
        : base(service, adminPageService, logger)
    {
        this.service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Checkin(int id)
    {
        if (GetEntity(id, out RoomReservation? roomReservation))
        {
            await service.PrepareNewCheckin(roomReservation);

            AddBackAction(
                nameof(BookingController.View),
                "Booking",
                requestParameters: new()
                {
                    { "Id", roomReservation.BookingId }
                });

            return View(roomReservation);
        }

        return RedirectToAction(nameof(BookingController.Index), nameof(BookingController));
    }

    [HttpPost]
    public async Task<IActionResult> Checkin(RoomReservation roomReservation)
    {
        bool success = await service.Update(roomReservation);

        if (!success)
        {
            AddMessage("Failed to check-in. An unknown error occurred.", ColorClass.Danger);
        }

        return RedirectToAction(nameof(BookingController.View), nameof(BookingController), new { roomReservation.BookingId });
    }

    [HttpPost]
    public async Task<IActionResult> Checkout(RoomReservation roomReservation)
    {
        bool success = await service.Checkout(roomReservation);

        if (!success)
        {
            AddMessage("Failed to check-out. An unknown error occurred.", ColorClass.Danger);
        }

        return RedirectToAction(nameof(BookingController.View), nameof(BookingController), new { roomReservation.BookingId });
    }
}
