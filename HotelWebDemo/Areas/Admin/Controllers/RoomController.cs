using HotelWebDemo.Models.Components.Admin.Pages;
using HotelWebDemo.Models.Components.Common;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;
using HotelWebDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebDemo.Areas.Admin.Controllers;

public class RoomController : CrudController<Room>
{
    public const string ROOM_INFO_SIDEBAR_LINK = "room_info";
    public const string ACTIVE_RESERVATIONS_SIDEBAR_LINK = "active_reservations";
    public const string RESERVATIONS_HISTORY_SIDEBAR_LINK = "reservation_history";

    private readonly new IRoomService service;
    private readonly IRoomReservationService roomReservationService;
    private readonly IHotelService hotelService;
    private readonly IRoomCategoryService roomCategoryService;
    private readonly IRoomFeatureService roomFeatureService;

    public RoomController(
        IRoomService service,
        IRoomReservationService roomReservationService,
        IAdminPageService adminPageService,
        IHotelService hotelService,
        IRoomCategoryService roomCategoryService,
        IRoomFeatureService roomFeatureService,
        Serilog.ILogger logger)
        : base(service, adminPageService, logger)
    {
        this.service = service;
        this.roomReservationService = roomReservationService;
        this.hotelService = hotelService;
        this.roomCategoryService = roomCategoryService;
        this.roomFeatureService = roomFeatureService;
        ListingTitle = "All rooms";
    }

    public override async Task<IActionResult> Create()
    {
        ViewData["Hotels"] = await hotelService.GetAll();
        ViewData["RoomCategories"] = await roomCategoryService.GetAll();
        ViewData["RoomFeatures"] = await roomFeatureService.GetAll();

        return await base.Create();
    }

    public override async Task<IActionResult> Edit(int id)
    {
        ViewData["Hotels"] = await hotelService.GetAll();
        ViewData["RoomCategories"] = await roomCategoryService.GetAll();
        ViewData["RoomFeatures"] = await roomFeatureService.GetAll();

        CreateSidebarLinks(id, ROOM_INFO_SIDEBAR_LINK);

        return await base.Edit(id);
    }

    [HttpGet]
    public async Task<IActionResult> ActiveReservations(int id)
    {
        CreateSidebarLinks(id, ACTIVE_RESERVATIONS_SIDEBAR_LINK);
        AddBackAction();

        return View(await roomReservationService.GetAllReservations(id, active: true));
    }

    [HttpGet]
    public async Task<IActionResult> ReservationHistory(ListingModel listingQuery, int id)
    {
        CreateSidebarLinks(id, RESERVATIONS_HISTORY_SIDEBAR_LINK);
        AddBackAction();

        return View(await roomReservationService.CreateReservationHistoryListing(listingQuery, id));
    }

    protected override async Task<string> MassAction(string massAction, List<int> selectedItemIds)
    {
        if (selectedItemIds.Count != 0)
        {
            if (massAction == "MassEnable")
            {
                await service.MassEnableToggle(selectedItemIds, enable: true);

                AddMessage($"Successfully enabled {selectedItemIds.Count} room(s).", ColorClass.Success);
            }
            else if (massAction == "MassDisable")
            {
                await service.MassEnableToggle(selectedItemIds, enable: false);

                AddMessage($"Successfully disabled {selectedItemIds.Count} room(s).", ColorClass.Success);
            }
        }

        return await base.MassAction(massAction, selectedItemIds);
    }

    public void CreateSidebarLinks(int roomId, string activeLink)
    {
        SidebarLinkGroup gr = GetOrCreateSidebarLinkGroup();
        gr.ActiveLinkId = activeLink;

        gr.Add(new SidebarLink
        {
            Id = ROOM_INFO_SIDEBAR_LINK,
            Content = "General information",
            Area = "Admin",
            Controller = "Room",
            Action = "Edit",
            RequestParameters = new()
            {
                { "Id", roomId }
            }
        });

        gr.Add(new SidebarLink
        {
            Id = ACTIVE_RESERVATIONS_SIDEBAR_LINK,
            Content = "Active reservations",
            Area = "Admin",
            Controller = "Room",
            Action = "ActiveReservations",
            RequestParameters = new()
            {
                { "Id", roomId }
            }
        });

        gr.Add(new SidebarLink
        {
            Id = RESERVATIONS_HISTORY_SIDEBAR_LINK,
            Content = "Reservation history",
            Area = "Admin",
            Controller = "Room",
            Action = "ReservationHistory",
            RequestParameters = new()
            {
                { "Id", roomId }
            }
        });
    }
}
