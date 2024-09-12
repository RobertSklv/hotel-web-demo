using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Data.Repositories;

public interface ICheckinInfoRepository : ICrudRepository<CheckinInfo>
{
    Task<List<CheckedInCustomer>> GetOrLoadCustomerCheckinInfos(CheckinInfo checkinInfo);

    Task<List<CheckedInCustomer>> GetOrLoadCustomerCheckinInfos(Customer customer);
}