﻿using HotelWebDemo.Models.Database;
using HotelWebDemo.Services;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Data.Repositories;

public class RoomRepository : CrudRepository<Room>, IRoomRepository
{
    public override DbSet<Room> DbSet => db.Rooms;

    public RoomRepository(AppDbContext db, IEntityFilterService filterService, IEntitySortService sortService)
        : base(db, filterService, sortService)
    {
    }

    public override Room? Get(int id)
    {
        Room? room = DbSet.Where(e => e.Id == id).Include(e => e.Features).FirstOrDefault();

        if (room != null)
        {
            room.SelectedFeatureIds = room.Features?.ConvertAll(f => f.Id) ?? new();
        }

        return room;
    }

    public override async Task<int> Update(Room entity)
    {
        await UpdateSelectedFeatures(entity);

        return await base.Update(entity);
    }

    public List<RoomFeatureRoom> GetOrLoadRoomFeatureRooms(Room room)
    {
        room.RoomFeatureRooms ??= db.RoomFeatureRooms.Where(e => e.RoomId == room.Id).ToList();

        return room.RoomFeatureRooms;
    }

    public async Task UpdateSelectedFeatures(Room room)
    {
        GetOrLoadRoomFeatureRooms(room);

        //Remove missing
        await db.RoomFeatureRooms.Where(e => e.RoomId == room.Id && !room.SelectedFeatureIds.Contains(e.RoomFeatureId)).ExecuteDeleteAsync();

        //Insert new ones
        foreach (int featureId in room.SelectedFeatureIds)
        {
            if (!room.RoomFeatureRooms.Any(e => e.RoomFeatureId == featureId))
            {
                RoomFeatureRoom @new = new()
                {
                    Room = room,
                    RoomId = room.Id,
                    RoomFeatureId = featureId
                };
                room.RoomFeatureRooms.Add(@new);
            }
        }
    }
}