using HotelWebDemo.Data.Repositories;
using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Services;

public class CheckinInfoService : CrudService<CheckinInfo>, ICheckinInfoService
{
    private readonly ICheckinInfoRepository repository;

    public CheckinInfoService(ICheckinInfoRepository repository)
        : base(repository)
    {
        this.repository = repository;
    }

    public async Task<List<CheckedInCustomer>> GetOrLoadCustomerCheckinInfos(CheckinInfo checkinInfo)
    {
        return await repository.GetOrLoadCustomerCheckinInfos(checkinInfo);
    }

    public async Task<List<CheckedInCustomer>> GetOrLoadCustomerCheckinInfos(Customer customer)
    {
        return await repository.GetOrLoadCustomerCheckinInfos(customer);
    }

    public CheckinInfo? CloneCheckinInfoRecord(CheckinInfo? checkinInfo)
    {
        if (checkinInfo == null)
        {
            return null;
        }

        CheckinInfo clone = new()
        {
            CheckedInCustomers = new()
        };

        if (checkinInfo.CheckedInCustomers == null)
        {
            throw new Exception("Checked-in customers not loaded.");
        }

        if (checkinInfo.CheckedInCustomers.Count > 0)
        {
            foreach (CheckedInCustomer customer in checkinInfo.CheckedInCustomers)
            {
                clone.CheckedInCustomers.Add(new CheckedInCustomer
                {
                    CheckinInfo = clone,
                    CustomerId = customer.CustomerId,
                });
            }
        }

        return clone;
    }
}
