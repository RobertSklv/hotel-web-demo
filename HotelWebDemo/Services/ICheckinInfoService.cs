using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Services;

public interface ICheckinInfoService : ICrudService<CheckinInfo>
{
    Task<List<CustomerCheckinInfo>> GetOrLoadCustomerCheckinInfos(CheckinInfo checkinInfo);

    Task<List<CustomerCheckinInfo>> GetOrLoadCustomerCheckinInfos(Customer customer);
}