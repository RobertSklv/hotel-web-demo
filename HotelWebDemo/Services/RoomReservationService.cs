using HotelWebDemo.Data.Repositories;
using HotelWebDemo.Models.Components.Admin.Tables;
using HotelWebDemo.Models.Components.Common;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;

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

        if (roomReservation.CheckinInfo == null)
        {
            roomReservation.CheckinInfo = new()
            {
                CheckedInCustomers = new()
            };

            for (int i = 0; i < roomReservation.Room.Capacity; i++)
            {
                CheckedInCustomer cch = new()
                {
                    Customer = new()
                };
                roomReservation.CheckinInfo.CheckedInCustomers.Add(cch);
            }
        }
    }

    public async Task<List<CheckedInCustomer>> ProcessCheckedInCustomers(CheckinInfo checkinInfo)
    {
        List<CheckedInCustomer> checkedInCustomers = new();
        List<int> knownCustomerIds = new();

        foreach (CheckedInCustomer checkedInCustomer in checkinInfo.CheckedInCustomers)
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
        List<CheckedInCustomer> knownCustomersCheckinInfos = CreateCustomerCheckinInfos(knownCustomers);
        checkedInCustomers.AddRange(knownCustomersCheckinInfos);

        return checkedInCustomers;
    }

    public List<CheckedInCustomer> CreateCustomerCheckinInfos(List<Customer> customers)
    {
        List<CheckedInCustomer> customerCheckinInfos = new();

        foreach (Customer customer in customers)
        {
            CheckedInCustomer checkedInCustomer = new()
            {
                CustomerId = customer.Id
            };
            customerCheckinInfos.Add(checkedInCustomer);
        }

        return customerCheckinInfos;
    }

    public async Task<bool> AddOrUpdateCurrentCheckin(RoomReservation roomReservation)
    {
        if (roomReservation.CheckinInfo == null)
        {
            throw new Exception("Current check-in is null.");
        }

        List<CheckedInCustomer> checkedInCustomers = await ProcessCheckedInCustomers(roomReservation.CheckinInfo);
        if (roomReservation.CheckinInfo.Id > 0)
        {
            (await checkinService.GetOrLoadCustomerCheckinInfos(roomReservation.CheckinInfo)).Clear();
        }
        roomReservation.CheckinInfo.CheckedInCustomers = checkedInCustomers;


        return await checkinService.Upsert(roomReservation.CheckinInfo);
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

        return success;
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

    public async Task<bool> ChangeRoom(int roomReservationId, int roomId)
    {
        RoomReservation roomReservation = await repository.GetStrict(roomReservationId);
        Room room = await roomService.GetStrict(roomId);

        RoomReservation? activeReservation = await repository.GetCheckedInReservation(roomId);

        if (activeReservation != null)
        {
            throw new Exception($"An active reservation already exists for the selected room.");
        }

        roomReservation.Room = room;
        roomReservation.RoomId = roomId;

        return await repository.Update(roomReservation) > 0;
    }

    public async Task<ChangeRoomViewModel> CreateChangeRoomListing(ListingModel listingQuery, int roomReservationId)
    {
        ChangeRoomViewModel listing = new();
        listing.CopyFrom(listingQuery);

        PaginatedList<Room> rooms = await GetBookableRooms(listing, roomReservationId);

        listing.Table = new Table<Room>(listingQuery, rooms, area: "Admin")
            .AddPagination(true)
            .RemoveColumn(nameof(Room.Id))
            .RemoveColumn(nameof(Room.Enabled))
            .RemoveColumn(nameof(Room.Hotel))
            .RemoveColumn(nameof(Room.CreatedAt))
            .RemoveColumn(nameof(Room.UpdatedAt))
            .AddRowAction(
                "Change",
                RequestMethod.Post,
                customizationCallback: action => action
                    .AddConfirmationPopup(true)
                    .SetConfirmationMessage<Room>(r => $"Are you sure you want to switch to room {r.Number}?")
                    .SetConfirmationTitle("Switch room?")
                    .SetRoute(id => $"/Admin/RoomReservation/{roomReservationId}/ChangeRoom/{id}")
                    .SetColor(ColorClass.Primary));

        return listing;
    }

    public Task<List<RoomReservation>> GetAllReservations(int roomId, bool? active = null)
    {
        return repository.GetAllReservations(roomId, active);
    }

    public Task<RoomReservation?> GetCheckedInReservation(int roomId)
    {
        return repository.GetCheckedInReservation(roomId);
    }

    public Task<PaginatedList<Room>> GetBookableRooms(ListingModel listingModel, int roomReservationId)
    {
        return repository.GetBookableRooms(listingModel, roomReservationId);
    }

    public async Task<PaginatedList<Room>> GetBookableRooms(BookingViewModel listingModel)
    {
        return await repository.GetBookableRooms(listingModel);
    }

    public Task<PaginatedList<Room>> GetBookableRooms(ListingModel listingModel, RoomReservation roomReservation)
    {
        return repository.GetBookableRooms(listingModel, roomReservation);
    }
}
