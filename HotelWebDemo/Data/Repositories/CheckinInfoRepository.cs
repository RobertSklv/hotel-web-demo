using HotelWebDemo.Models.Database;
using HotelWebDemo.Services;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Data.Repositories;

public class CheckinInfoRepository : CrudRepository<CheckinInfo>, ICheckinInfoRepository
{
    public override DbSet<CheckinInfo> DbSet => db.CheckinInfos;

    public CheckinInfoRepository(
        AppDbContext db,
        IEntityFilterService filterService,
        IEntitySortService sortService,
        IEntitySearchService searchService)
        : base(db, filterService, sortService, searchService)
    {
    }

    public async Task<List<CheckedInCustomer>> GetOrLoadCustomerCheckinInfos(CheckinInfo checkinInfo)
    {
        checkinInfo.CheckedInCustomers ??= await db.CustomerCheckinInfos
            .Where(e => e.CheckinInfoId == checkinInfo.Id)
            .ToListAsync();

        return checkinInfo.CheckedInCustomers;
    }

    public async Task<List<CheckedInCustomer>> GetOrLoadCustomerCheckinInfos(Customer customer)
    {
        customer.Checkins ??= await db.CustomerCheckinInfos
            .Where(e => e.CustomerId == customer.Id)
            .ToListAsync();

        return customer.Checkins;
    }
}
