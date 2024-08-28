using HotelWebDemo.Data.Repositories;
using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Services;

public class RoomReservationService : CrudService<RoomReservation>, IRoomReservationService
{
    private readonly IRoomReservationRepository repository;
    private readonly ICustomerService customerService;
    private readonly ICheckinInfoService checkinService;

    public RoomReservationService(
        IRoomReservationRepository repository,
        ICustomerService customerService,
        ICheckinInfoService checkinService)
        : base(repository)
    {
        this.repository = repository;
        this.customerService = customerService;
        this.checkinService = checkinService;
    }

    public async Task<CheckinInfo?> GetOrLoadCurrentCheckinInfo(RoomReservation roomReservation)
    {
        return await repository.GetOrLoadCurrentCheckinInfo(roomReservation);
    }

    public async Task PrepareNewCheckin(RoomReservation roomReservation)
    {
        await GetOrLoadCurrentCheckinInfo(roomReservation);

        roomReservation.CurrentCheckin ??= new();
        roomReservation.CurrentCheckin.CustomerCheckinInfos ??= new();

        for (int i = 0; i < roomReservation.Room.Capacity; i++)
        {
            CustomerCheckinInfo cch = new()
            {
                Customer = new()
            };
            roomReservation.CurrentCheckin.CustomerCheckinInfos.Add(cch);
        }
    }

    public async Task<List<CustomerCheckinInfo>> ProcessCheckedInCustomers(CheckinInfo checkinInfo)
    {
        List<CustomerCheckinInfo> checkedInCustomers = new();
        List<int> knownCustomerIds = new();

        foreach (CustomerCheckinInfo checkedInCustomer in checkinInfo.CustomerCheckinInfos)
        {
            if (checkedInCustomer.CustomerId != 0)
            {
                knownCustomerIds.Add(checkedInCustomer.CustomerId);
            }
            else
            {
                checkedInCustomers.Add(checkedInCustomer);
            }
        }

        List<Customer> knownCustomers = await customerService.GetByIds(knownCustomerIds);
        List<CustomerCheckinInfo> knownCustomersCheckinInfos = CreateCustomerCheckinInfos(knownCustomers);
        checkedInCustomers.AddRange(knownCustomersCheckinInfos);

        return checkedInCustomers;
    }

    public List<CustomerCheckinInfo> CreateCustomerCheckinInfos(List<Customer> customers)
    {
        List<CustomerCheckinInfo> customerCheckinInfos = new();

        foreach (Customer customer in customers)
        {
            CustomerCheckinInfo checkedInCustomer = new()
            {
                CustomerId = customer.Id
            };
            customerCheckinInfos.Add(checkedInCustomer);
        }

        return customerCheckinInfos;
    }

    public async Task AddOrUpdateCurrentCheckin(RoomReservation roomReservation)
    {
        if (roomReservation.CurrentCheckin == null)
        {
            throw new Exception("Current check-in is null.");
        }

        roomReservation.CurrentCheckin.RoomReservationId = roomReservation.Id;

        List<CustomerCheckinInfo> checkedInCustomers = await ProcessCheckedInCustomers(roomReservation.CurrentCheckin);

        if (roomReservation.CurrentCheckin.Id > 0)
        {
            (await checkinService.GetOrLoadCustomerCheckinInfos(roomReservation.CurrentCheckin)).Clear();
        }

        roomReservation.CurrentCheckin.CustomerCheckinInfos = checkedInCustomers;
        await checkinService.Upsert(roomReservation.CurrentCheckin);
    }

    public override async Task<bool> Upsert(RoomReservation entity)
    {
        await AddOrUpdateCurrentCheckin(entity);

        return await base.Upsert(entity);
    }

    public async Task<bool> Checkout(RoomReservation roomReservation)
    {
        return await repository.Checkout(roomReservation) > 0;
    }
}
