using HotelWebDemo.Models.Database;
using HotelWebDemo.Services;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Data.Repositories;

public class HotelRepository : CrudRepository<Hotel>, IHotelRepository
{
    public override DbSet<Hotel> DbSet => db.Hotels;

    public HotelRepository(AppDbContext db, IEntityFilterService filterService, IEntitySortService sortService)
        : base(db, filterService, sortService)
    {
    }

    public override IQueryable<Hotel> List(DbSet<Hotel> dbSet)
    {
        return dbSet;
    }
}
