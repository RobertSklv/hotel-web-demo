using HotelWebDemo.Models.Database;
using HotelWebDemo.Services;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Data.Repositories;

public class RoomReservationRepository : CrudRepository<RoomReservation>, IRoomReservationRepository
{
    public override DbSet<RoomReservation> DbSet => db.RoomReservations;

    public RoomReservationRepository(AppDbContext db, IEntityFilterService filterService, IEntitySortService sortService, IEntitySearchService searchService)
        : base(db, filterService, sortService, searchService)
    {
    }

    public override RoomReservation? Get(int id)
    {
        return db.RoomReservations
            .Include(e => e.Booking)
            .Include(e => e.Room)
                .ThenInclude(e => e.Category)
            .Include(e => e.CheckinInfos)
                .ThenInclude(e => e.CustomerCheckinInfos)
            .FirstOrDefault(e => e.Id == id);
    }

    public async Task<CheckinInfo?> GetCurrentCheckinInfo(int roomReservationId)
    {
        return await db.CheckinInfos.FirstOrDefaultAsync(e => e.RoomReservationId == roomReservationId && !e.IsCheckedOut);
    }

    public async Task<CheckinInfo?> GetOrLoadCurrentCheckinInfo(RoomReservation roomReservation)
    {
        if (roomReservation.CurrentCheckin == null)
        {
            List<CheckinInfo> checkinInfos = await GetOrLoadCheckinInfos(roomReservation);
            roomReservation.CurrentCheckin ??= checkinInfos.FirstOrDefault(e => !e.IsCheckedOut);
        }

        return roomReservation.CurrentCheckin;
    }

    public async Task<List<CheckinInfo>> GetOrLoadCheckinInfos(RoomReservation roomReservation)
    {
        roomReservation.CheckinInfos ??= await db.CheckinInfos.Where(e => e.RoomReservationId == roomReservation.Id).ToListAsync();

        return roomReservation.CheckinInfos;
    }

    public async Task<int> Checkout(RoomReservation roomReservation)
    {
        List<CheckinInfo> checkinInfos = await GetOrLoadCheckinInfos(roomReservation);

        foreach (CheckinInfo checkin in checkinInfos)
        {
            checkin.CheckoutDate = DateTime.UtcNow;
        }

        db.Update(roomReservation);

        return await db.SaveChangesAsync();
    }
}
