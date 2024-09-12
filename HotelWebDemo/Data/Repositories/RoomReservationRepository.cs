using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;
using HotelWebDemo.Services;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Data.Repositories;

public class RoomReservationRepository : CrudRepository<RoomReservation>, IRoomReservationRepository
{
    private readonly IRoomService roomService;

    public override DbSet<RoomReservation> DbSet => db.RoomReservations;

    public RoomReservationRepository(
        AppDbContext db,
        IEntityFilterService filterService,
        IEntitySortService sortService,
        IEntitySearchService searchService,
        IRoomService roomService)
        : base(db, filterService, sortService, searchService)
    {
        this.roomService = roomService;
    }

    public override async Task<RoomReservation?> Get(int id)
    {
        RoomReservation? roomReservation = await db.RoomReservations
            .Include(e => e.Booking)
            .Include(e => e.Room)
                .ThenInclude(e => e!.Category)
            .Include(e => e.CheckinInfo!)
                .ThenInclude(e => e.CheckedInCustomers!)
                    .ThenInclude(e => e.Customer)
                        .ThenInclude(e => e!.Citizenship)
            .Include(e => e.CheckinInfo!)
                .ThenInclude(e => e.CheckedInCustomers!)
                    .ThenInclude(e => e.Customer)
                        .ThenInclude(e => e!.Address)
                            .ThenInclude(e => e!.Country)
            .FirstOrDefaultAsync(e => e.Id == id);

        return roomReservation;
    }

    public async Task<CheckinInfo?> GetOrLoadCurrentCheckinInfo(RoomReservation roomReservation)
    {
        roomReservation.CheckinInfo ??= await db.CheckinInfos.FirstOrDefaultAsync(e => e.Id == roomReservation.CheckinInfoId);

        return roomReservation.CheckinInfo;
    }

    public async Task<int> Checkout(RoomReservation roomReservation)
    {
        CheckinInfo checkin = await GetOrLoadCurrentCheckinInfo(roomReservation) ?? throw new Exception("No active check-in found.");

        checkin.CheckoutDate = DateTime.UtcNow;

        db.Update(roomReservation);

        return await db.SaveChangesAsync();
    }

    public async Task<List<RoomReservation>> GetAllReservations(int roomId, bool? active = null)
    {
        IQueryable<RoomReservation> query = db.RoomReservations
            .Include(e => e.CheckinInfo)
            .Where(e => e!.RoomId == roomId)
            .OrderByDescending(e => e.Id);

        if (active != null)
        {
            if ((bool)active)
            {
                query.Where(e => e.CheckinInfo != null && e.CheckinInfo.CheckoutDate == null);
            }
            else
            {
                query.Where(e => e.CheckinInfo == null || e.CheckinInfo.CheckoutDate != null);
            }
        }

        return await query.ToListAsync();
    }

    public async Task<RoomReservation?> GetCheckedInReservation(int roomId)
    {
        return await db.RoomReservations
            .Include(e => e.CheckinInfo)
            .Where(e => e.RoomId == roomId
                && e.CheckinInfo != null
                && e.CheckinInfo.CheckoutDate == null)
            .FirstOrDefaultAsync();
    }

    public async Task<PaginatedList<Room>> GetBookableRooms(ListingModel listingModel, int roomReservationId)
    {
        RoomReservation reservation = await GetStrict(roomReservationId);

        return await roomService.GetBookableRooms(listingModel, reservation);
    }
}
 