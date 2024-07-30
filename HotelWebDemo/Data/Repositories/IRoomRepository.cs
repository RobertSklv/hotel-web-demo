﻿using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Data.Repositories;

public interface IRoomRepository : ICrudRepository<Room>
{
    List<RoomFeatureRoom> GetOrLoadRoomFeatureRooms(Room room);

    Task UpdateSelectedFeatures(Room room);
}