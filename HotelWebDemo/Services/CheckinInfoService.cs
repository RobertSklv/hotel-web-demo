using HotelWebDemo.Data.Repositories;
using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Services;

public class CheckinInfoService : CrudService<CheckinInfo>, ICheckinInfoService
{
    private readonly new ICheckinInfoRepository repository;

    public CheckinInfoService(ICheckinInfoRepository repository)
        : base(repository)
    {
        this.repository = repository;
    }

    public async Task<List<CustomerCheckinInfo>> GetOrLoadCustomerCheckinInfos(CheckinInfo checkinInfo)
    {
        return await repository.GetOrLoadCustomerCheckinInfos(checkinInfo);
    }

    public async Task<List<CustomerCheckinInfo>> GetOrLoadCustomerCheckinInfos(Customer customer)
    {
        return await repository.GetOrLoadCustomerCheckinInfos(customer);
    }
}
