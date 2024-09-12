using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Services;

public interface ICheckinInfoService : ICrudService<CheckinInfo>
{
    Task<List<CheckedInCustomer>> GetOrLoadCustomerCheckinInfos(CheckinInfo checkinInfo);

    Task<List<CheckedInCustomer>> GetOrLoadCustomerCheckinInfos(Customer customer);
}