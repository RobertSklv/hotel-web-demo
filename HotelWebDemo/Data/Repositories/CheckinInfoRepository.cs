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

    public async Task<List<CustomerCheckinInfo>> GetOrLoadCustomerCheckinInfos(CheckinInfo checkinInfo)
    {
        checkinInfo.CustomerCheckinInfos ??= await db.CustomerCheckinInfos
            .Where(e => e.CheckinInfoId == checkinInfo.Id)
            .ToListAsync();

        return checkinInfo.CustomerCheckinInfos;
    }

    public async Task<List<CustomerCheckinInfo>> GetOrLoadCustomerCheckinInfos(Customer customer)
    {
        customer.CustomerCheckinInfos ??= await db.CustomerCheckinInfos
            .Where(e => e.CustomerId == customer.Id)
            .ToListAsync();

        return customer.CustomerCheckinInfos;
    }
}
