﻿using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.Json;
using HotelWebDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebDemo.Areas.Admin.Controllers;

public class RoomCategoryController : CrudController<RoomCategory>
{
    private readonly new IRoomCategoryService service;
    private readonly IHotelService hotelService;

    public RoomCategoryController(
        IRoomCategoryService service,
        IAdminPageService adminPageService,
        IHotelService hotelService,
        Serilog.ILogger logger)
        : base(service, adminPageService, logger)
    {
        ListingTitle = "All room categories";
        this.service = service;
        this.hotelService = hotelService;
    }

    public override IActionResult Create()
    {
        ViewData["Hotels"] = hotelService.GetAll();

        return base.Create();
    }

    public override IActionResult Edit(int? id)
    {
        ViewData["Hotels"] = hotelService.GetAll();

        return base.Edit(id);
    }

    public List<RoomCategoryOption> GetHotelRoomCategories(int hotelId)
    {
        return service.GetAllAsOptions(hotelId);
    }
}