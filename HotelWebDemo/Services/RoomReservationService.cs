using HotelWebDemo.Data.Repositories;
using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Services;

public class RoomReservationService : CrudService<RoomReservation>, IRoomReservationService
{
    private readonly IRoomReservationRepository repository;
    private readonly ICustomerService customerService;
    private readonly ICheckinInfoService checkinService;
    private readonly IRoomService roomService;
    private readonly IBookingLogService bookingLogService;
    private readonly IAdminUserService adminUserService;
    private readonly Serilog.ILogger logger;

    public RoomReservationService(
        IRoomReservationRepository repository,
        ICustomerService customerService,
        ICheckinInfoService checkinService,
        IRoomService roomService,
        IBookingLogService bookingLogService,
        IAdminUserService adminUserService,
        Serilog.ILogger logger)
        : base(repository)
    {
        this.repository = repository;
        this.customerService = customerService;
        this.checkinService = checkinService;
        this.roomService = roomService;
        this.bookingLogService = bookingLogService;
        this.adminUserService = adminUserService;
        this.logger = logger;
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

    public async Task<bool> AddOrUpdateCurrentCheckin(RoomReservation roomReservation)
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

        return await checkinService.Upsert(roomReservation.CurrentCheckin);
    }

    public override async Task<bool> Upsert(RoomReservation entity)
    {
        bool isCreate = entity.Id == 0;
        bool success = await AddOrUpdateCurrentCheckin(entity);
        bool baseSuccess = await base.Upsert(entity);
        bool finalSuccess = success && baseSuccess;

        if (finalSuccess)
        {
            try
            {
                AdminUser admin = adminUserService.GetCurrentAdmin();
                Room room = await roomService.GetStrict(entity.RoomId);

                if (isCreate)
                {
                    bookingLogService.CreateLog(entity.BookingId, $"{admin.RoleAndName} checked in customers for room {room.Number}.");
                }
                else
                {
                    bookingLogService.CreateLog(entity.BookingId, $"{admin.RoleAndName} updated existing customers check-in for room {room.Number}.");
                }
            }
            catch (Exception e)
            {
                logger.Fatal(e, $"Failed logging check-in event for room reservation: {entity.Id}.");
            }
        }

        return finalSuccess;
    }

    public async Task<bool> Checkout(RoomReservation roomReservation)
    {
        bool success = await repository.Checkout(roomReservation) > 0;

        if (success)
        {
            try
            {
                AdminUser admin = adminUserService.GetCurrentAdmin();
                Room room = await roomService.GetStrict(roomReservation.RoomId);

                bookingLogService.CreateLog(roomReservation.BookingId, $"{admin.RoleAndName} checked out customers for room {room.Number}.");
                }
            catch (Exception e)
            {
                logger.Fatal(e, $"Failed logging checkout event for room reservation: {roomReservation.Id}.");
            }
        }

        return success;
    }
}
