using HotelWebDemo.Areas.Admin.Controllers;
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
                    await bookingLogService.Log(entity.BookingId, $"{admin.RoleAndName} checked in customers for room {room.Number}.");
                }
                else
                {
                    await bookingLogService.Log(entity.BookingId, $"{admin.RoleAndName} updated existing customers check-in for room {room.Number}.");
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

                await bookingLogService.Log(
                    roomReservation.BookingId,
                    $"{admin.RoleAndName} checked out customers for room {room.Number}.");
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

        if (!room.Enabled)
        {
            throw new Exception("The room is disabled.");
        }

        if (roomReservation.Booking == null)
        {
            throw new Exception("Booking not loaded.");
        }

        List<int> reservedRoomIdsForPeriod = (await repository.GetReservationsForPeriod(DateTime.UtcNow, roomReservation.Booking.CheckoutDate)).ConvertAll(r => r.RoomId);

        if (reservedRoomIdsForPeriod.Contains(roomId))
        {
            throw new Exception($"The selected room has an active reservation for the given period.");
        }

        CheckinInfo? currentCheckinInfo = await GetOrLoadCurrentCheckinInfo(roomReservation);
        bool checkoutSuccess = currentCheckinInfo == null || await repository.Checkout(roomReservation) > 0;
        bool success;

        if (currentCheckinInfo != null)
        {
            RoomReservation newReservation = new()
            {
                BookingId = roomReservation.BookingId,
                Room = room,
                RoomId = roomId,
                BookingItemId = roomReservation.BookingItemId,
                CheckinInfo = checkinService.CloneCheckinInfoRecord(roomReservation.CheckinInfo)
            };

            success = await repository.Upsert(newReservation) > 0;

            if (success)
            {
                try
                {
                    AdminUser admin = adminUserService.GetCurrentAdmin();
                    Room oldRoom = await roomService.GetStrict(roomReservation.RoomId);

                    await bookingLogService.Log(
                        roomReservation.BookingId,
                        $"{admin.RoleAndName} moved customers from room {oldRoom.Number} to room {room.Number}.");
                }
                catch (Exception e)
                {
                    logger.Fatal(e, $"Failed logging change room event for room reservation: {roomReservation.Id}.");
                }
            }
        }
        else
        {
            roomReservation.RoomId = roomId;
            roomReservation.Room = room;

            success = await repository.Update(roomReservation) > 0;
        }

        return success;
    }

    public async Task<ChangeRoomViewModel> CreateChangeRoomListing(ListingModel listingQuery, int roomReservationId)
    {
        ChangeRoomViewModel listing = new();
        listing.CopyFrom(listingQuery);
        listing.Area = "Admin";
        listing.Controller = "RoomReservation";
        listing.Action = "ChangeRoom";
        listing.RequestParameters = new()
        {
            { "roomReservationId", roomReservationId }
        };

        PaginatedList<Room> rooms = await GetBookableRooms(listing, roomReservationId);

        listing.Table = new Table<Room>(listingQuery, rooms)
            .AddPagination(true)
            .SetFilterable(true)
            .SetAdjustablePageSize(true)
            .SetSearchable(true)
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
        return repository.GetReservationsForRoom(roomId, active);
    }

    public Task<RoomReservation?> GetCheckedInReservation(int roomId)
    {
        return repository.GetCheckedInReservationForRoom(roomId);
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

    public async Task<ListingModel<RoomReservation>> CreateReservationHistoryListing(ListingModel listingQuery, int roomId)
    {
        ListingModel<RoomReservation> listingModel = new();
        listingModel.CopyFrom(listingQuery);
        listingModel.Route = $"/Admin/Room/ReservationHistory/{roomId}";

        PaginatedList<RoomReservation> items = await repository.GetReservationsForRoomPaginated(listingModel, roomId);

        listingModel.Table = new Table<RoomReservation>(listingModel, items)
            .AddPagination(true)
            .SetOrderable(true)
            .SetFilterable(true)
            .SetAdjustablePageSize(true)
            .RemoveColumn(nameof(RoomReservation.Room))
            .OverrideColumnSortOrder(nameof(RoomReservation.Period), 1000)
            .OverrideColumnValue(nameof(RoomReservation.Booking), r => "#" + r.BookingId)
            .OverrideColumnSortOrder(nameof(RoomReservation.Booking), 1001)
            .AddColumnLink(nameof(RoomReservation.Booking), r => $"/Admin/Booking/View/{r.BookingId}");

        return listingModel;
    }
}
