using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Data.Repositories;

public interface ICheckinInfoRepository : ICrudRepository<CheckinInfo>
{
    Task<List<CustomerCheckinInfo>> GetOrLoadCustomerCheckinInfos(CheckinInfo checkinInfo);

    Task<List<CustomerCheckinInfo>> GetOrLoadCustomerCheckinInfos(Customer customer);
}