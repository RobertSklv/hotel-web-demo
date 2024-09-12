using HotelWebDemo.Models.Components.Common;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;
using HotelWebDemo.Services;
using Microsoft.AspNetCore.Mvc;
using StarExplorerMainServer.Areas.Admin.Services;

namespace HotelWebDemo.Areas.Admin.Controllers;

public class RoomReservationController : CrudController<RoomReservation>
{
    private readonly new IRoomReservationService service;
    private readonly IRoomService roomService;
    private readonly ICountryService countryService;

    public RoomReservationController(
        IRoomReservationService service,
        IRoomService roomService,
        IAdminPageService adminPageService,
        Serilog.ILogger logger,
        ICountryService countryService)
        : base(service, adminPageService, logger)
    {
        this.service = service;
        this.roomService = roomService;
        this.countryService = countryService;
    }

    [HttpGet]
    public async Task<IActionResult> Checkin(int id)
    {
        RoomReservation? roomReservation = await GetViewModel(id);
        if (roomReservation != null)
        {
            await service.PrepareNewCheckin(roomReservation);

            ViewData["Countries"] = await countryService.GetAll();

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
    public async Task<IActionResult> SubmitCheckin([FromBody] RoomReservation roomReservation)
    {
        bool success = await service.Upsert(roomReservation);

        if (!success)
        {
            AddMessage("Failed to check-in. An unknown error occurred.", ColorClass.Danger);
        }

        return RedirectToRoute($"/Admin/Booking/View/{roomReservation.BookingId}");
    }

    [HttpPost]
    public async Task<IActionResult> Checkout(int roomReservationId)
    {
        RoomReservation? roomReservation = await service.Get(roomReservationId);

        if (roomReservation != null)
        {
            bool success = await service.Checkout(roomReservation);

            if (!success)
            {
                AddMessage("Failed to check-out. An unknown error occurred.", ColorClass.Danger);
            }
        }
        else
        {
            AddMessage("Failed to check-out. Room reservation not found.", ColorClass.Danger);

            return RedirectToRoute($"/Admin/Booking");
        }

        return RedirectToRoute($"/Admin/Booking/View/{roomReservation.BookingId}");
    }

    [HttpGet]
    [Route("/Admin/RoomReservation/ChangeRoom/{roomReservationId}")]
    public async Task<IActionResult> ChangeRoom([FromRoute] int roomReservationId, [FromQuery] ListingModel listingModel)
    {
        return View(await service.CreateChangeRoomListing(listingModel, roomReservationId));
    }

    [HttpPost]
    [Route("/Admin/RoomReservation/ChangeRoom/{roomReservationId}/Room/{roomId}")]
    public async Task<IActionResult> ChangeRoom([FromRoute] int roomReservationId, [FromRoute] int roomId)
    {
        try
        {
            bool success = await service.ChangeRoom(roomReservationId, roomId);

            if (success)
            {
                AddMessage($"Room successfully changed.", ColorClass.Success);
            }
            else
            {
                throw new Exception("An unknown error occurred.");
            }
        }
        catch (Exception e)
        {
            logger.Fatal(e, $"Failed to change room of room reservation with ID {roomReservationId}.");
            AddMessage($"Failed to change the room: {e.Message}", ColorClass.Danger);
        }

        return RedirectToRoute($"/Admin/RoomReservation/ChangeRoom/{roomReservationId}");
    }
}
