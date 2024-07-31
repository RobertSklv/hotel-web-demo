﻿using HotelWebDemo.Models.Database;
using HotelWebDemo.Services;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Data.Repositories;

public class RoomCategoryRepository : CrudRepository<RoomCategory>, IRoomCategoryRepository
{
    public override DbSet<RoomCategory> DbSet => db.RoomCategories;

    public RoomCategoryRepository(
        AppDbContext db,
        IEntityFilterService filterService,
        IEntitySortService sortService,
        IEntitySearchService searchService)
        : base(db, filterService, sortService, searchService)
    {
    }

    public List<RoomCategory> GetAll(int hotelId)
    {
        return db.RoomCategories.Where(e => e.HotelId == hotelId).ToList();
    }
}
