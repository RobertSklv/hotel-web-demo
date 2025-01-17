﻿using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;

namespace HotelWebDemo.Services;

public interface IRoomService : ICrudService<Room>
{
    Task<int> MassEnableToggle(List<int> selectedItemIds, bool enable);
}