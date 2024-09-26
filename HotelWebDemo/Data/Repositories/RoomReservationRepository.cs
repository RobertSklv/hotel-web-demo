using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;
using HotelWebDemo.Services;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Data.Repositories;

public class RoomReservationRepository : CrudRepository<RoomReservation>, IRoomReservationRepository
{
    private readonly IRoomRepository roomRepository;

    public override DbSet<RoomReservation> DbSet => db.RoomReservations;

    public RoomReservationRepository(
        AppDbContext db,
        IEntityFilterService filterService,
        IEntitySortService sortService,
        IEntitySearchService searchService,
        IRoomRepository roomRepository)
        : base(db, filterService, sortService, searchService)
    {
        this.roomRepository = roomRepository;
    }

    public override IQueryable<RoomReservation> List(DbSet<RoomReservation> dbSet)
    {
        return base.List(dbSet)
            .Include(e => e.CheckinInfo)
            .Include(e => e.Booking)
                .ThenInclude(e => e!.Totals);
    }

    public override async Task<RoomReservation?> Get(int id)
    {
        RoomReservation? roomReservation = await db.RoomReservations
            .Include(e => e.Booking)
            .Include(e => e.BookingItem)
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

    public async Task<List<RoomReservation>> GetReservationsForPeriod(DateTime from, DateTime to)
    {
        return await DbSet
            .Include(e => e.Booking)
            .Where(e =>
                e.Booking!.BookingCancellationId == null &&
                e.Booking.CheckinDate < to &&
                from < e.Booking.CheckoutDate)
            .ToListAsync();
    }

    public async Task<List<int>> GetReservedRoomIdsForPeriod(DateTime from, DateTime to)
    {
        List<RoomReservation> reservationsForPeriod = await GetReservationsForPeriod(from, to);

        return reservationsForPeriod.ConvertAll(r => r.RoomId);
    }

    public IQueryable<RoomReservation> AddRoomIdFilter(IQueryable<RoomReservation> query, int roomId)
    {
        return query.Where(e => e!.RoomId == roomId);
    }

    public IQueryable<RoomReservation> AddActiveReservationFilter(IQueryable<RoomReservation> query, bool active)
    {
        if (active)
        {
            query = query.Where(e => e.CheckinInfo != null && e.CheckinInfo.CheckoutDate == null);
        }
        else
        {
            query = query.Where(e => e.CheckinInfo == null || e.CheckinInfo.CheckoutDate != null);
        }

        return query;
    }

    public async Task<List<RoomReservation>> GetReservationsForRoom(int roomId, bool? active = null)
    {
        IQueryable<RoomReservation> query = DbSet
            .Include(e => e.CheckinInfo)
            .Include(e => e.Booking)
            .OrderByDescending(e => e.Id);

        query = AddRoomIdFilter(query, roomId);

        if (active != null)
        {
            query = AddActiveReservationFilter(query, (bool)active);
        }

        return await query.ToListAsync();
    }

    public async Task<RoomReservation?> GetCheckedInReservationForRoom(int roomId)
    {
        return await DbSet
            .Include(e => e.CheckinInfo)
            .Include(e => e.Booking)
            .Where(e => e.RoomId == roomId
                && e.CheckinInfo != null
                && e.CheckinInfo.CheckoutDate == null)
            .FirstOrDefaultAsync();
    }

    public async Task<PaginatedList<Room>> GetBookableRooms(BookingViewModel listingModel)
    {
        List<int> reservedRoomsIds = await GetReservedRoomIdsForPeriod(listingModel.CheckInDate, listingModel.CheckOutDate);

        return await roomRepository.List(listingModel, query =>
        {
            query = roomRepository.AddEnabledFilter(query, true);
            query = roomRepository.AddHotelIdFilter(query, listingModel.HotelId);
            query = roomRepository.AddRoomIdFilter(query, reservedRoomsIds, includes: false);

            if (listingModel.RoomsToReserve != null)
            {
                query = roomRepository.AddRoomIdFilter(query, listingModel.RoomsToReserve, includes: false);
            }

            return query;
        });
    }

    public async Task<PaginatedList<Room>> GetBookableRooms(ListingModel listingModel, RoomReservation roomReservation)
    {
        if (roomReservation.Room == null)
        {
            throw new Exception("Room not loaded.");
        }

        if (roomReservation.Booking == null)
        {
            throw new Exception("Booking not loaded.");
        }

        if (roomReservation.BookingItem == null)
        {
            throw new Exception("BookingItem not loaded.");
        }

        List<int> reservedRoomsIds = await GetReservedRoomIdsForPeriod(DateTime.UtcNow, roomReservation.Booking.CheckoutDate);

        return await roomRepository.List(listingModel, query =>
        {
            query = roomRepository.AddEnabledFilter(query, true);
            query = roomRepository.AddRoomIdFilter(query, reservedRoomsIds, includes: false);
            //query = roomRepository.AddRoomCategoryIdFilter(query, new()
            //{
            //    roomReservation.BookingItem.RoomCategoryId
            //}, includes: true);
            query = query.Where(e => e.Capacity == roomReservation.BookingItem.TargetCapacity);

            return query;
        });
    }

    public async Task<PaginatedList<Room>> GetBookableRooms(ListingModel listingModel, int roomReservationId)
    {
        RoomReservation reservation = await GetStrict(roomReservationId);

        return await GetBookableRooms(listingModel, reservation);
    }

    public async Task<PaginatedList<RoomReservation>> GetReservationsForRoomPaginated(
        ListingModel listingModel,
        int roomId,
        bool? active = null)
    {
        return await List(listingModel, query =>
        {
            query = AddRoomIdFilter(query, roomId);

            if (active != null)
            {
                query = AddActiveReservationFilter(query, (bool)active);
            }

            return query;
        });
    }
}
 