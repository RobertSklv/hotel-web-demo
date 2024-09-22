using HotelWebDemo.Areas.Admin.Controllers;
using HotelWebDemo.Data.Repositories;
using HotelWebDemo.Models.Components.Admin.Booking;
using HotelWebDemo.Models.Components.Admin.Tables;
using HotelWebDemo.Models.Components.Common;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;

namespace HotelWebDemo.Services;

public class BookingService : CrudService<Booking, BookingViewModel>, IBookingService
{
    public const string HOTEL_SELECTION_STEP_NAME = "hotel_selection";
    public const string ROOM_RESERVATION_STEP_NAME = "room_reservation";
    public const string CONTACT_STEP_NAME = "contact";
    public const string SUMMARY_STEP_NAME = "summary";

    private readonly IRoomService roomService;
    private readonly IRoomReservationService roomReservationService;
    private readonly IRoomCategoryService categoryService;
    private readonly IBookingTotalsService totalsService;
    private readonly IBookingLogService logService;

    public BookingService
        (IBookingRepository repository,
        IRoomService roomService,
        IRoomReservationService roomReservationService,
        IRoomCategoryService categoryService,
        IBookingTotalsService totalsService,
        IBookingLogService logService)
        : base(repository)
    {
        this.roomService = roomService;
        this.roomReservationService = roomReservationService;
        this.categoryService = categoryService;
        this.totalsService = totalsService;
        this.logService = logService;
    }

    public async Task<ListingModel<Room>> CreateRoomListing(BookingViewModel? viewModel)
    {
        BookingViewModel listingModel = new();
        listingModel.CopyFrom(viewModel);
        listingModel.AreaName = "Admin";
        listingModel.ControllerName = "Booking";
        listingModel.ActionName = "ReserveRooms";

        PaginatedList<Room> items = await roomReservationService.GetBookableRooms(listingModel);

        listingModel.Table = new Table<Room>(listingModel, items)
            .SetOrderable(true)
            .SetFilterable(true)
            .AddPagination(true)
            .RemoveColumn(nameof(Room.Id))
            .RemoveColumn(nameof(Room.Enabled))
            .RemoveColumn(nameof(Room.Hotel))
            .RemoveColumn(nameof(Room.CreatedAt))
            .RemoveColumn(nameof(Room.UpdatedAt))
            .AddRowAction("Reserve", customizationCallback: action => action.SetRoute("/"))
            .SetSelectableOptionsSource(nameof(Room.Category), await categoryService.GetAll());

        return listingModel;
    }

    public override async Task<Table<Booking>> CreateListingTable(ListingModel<Booking> listingModel, PaginatedList<Booking> items)
    {
        return (await base.CreateListingTable(listingModel, items))
            .RemoveColumn(nameof(BaseEntity.UpdatedAt))
            .AddRowAction("View", RequestMethod.Get, customizationCallback: a => a.SetSortOrder(0));
    }

    public override Table<Booking> CreateEditRowAction(Table<Booking> table)
    {
        return table;
    }

    public override async Task<BookingViewModel> EntityToViewModelAsync(Booking entity)
    {
        if (entity.Totals == null)
        {
            throw new Exception("Totals not loaded.");
        }

        BookingViewModel viewModel = new()
        {
            Id = entity.Id,
            CheckInDate = entity.CheckinDate,
            CheckOutDate = entity.CheckoutDate,
            Contact = entity.Contact,
            Totals = entity.Totals,
            HasCustomGrandTotal = entity.Totals.HasCustomGrandTotal,
            CustomGrandTotal = entity.Totals.CustomGrandTotal,
            RoomsToReserve = new(),
            ReservedRooms = new(),
            RoomReservations = entity.ReservedRooms,
            Status = entity.Status,
            Timeline = entity.BookingTimeline,
            RoomsPrice = await totalsService.CalculateTotals<TotalsCategoryModifier>(entity.Totals),
            RoomFeaturesPrice = await totalsService.CalculateTotals<TotalsFeatureModifier>(entity.Totals),
            GrandTotal = await totalsService.CalculateGrandTotal(entity.Totals),
        };

        if (entity.ReservedRooms == null || entity.ReservedRooms.Count == 0)
        {
            throw new Exception($"ReservedRooms is null or empty");
        }

        viewModel.Hotel = entity.ReservedRooms[0].Room?.Category?.Hotel;
        viewModel.HotelId = viewModel.Hotel.Id;

        foreach (RoomReservation rr in entity.ReservedRooms)
        {
            viewModel.ReservedRooms.Add(rr.Room);
            viewModel.RoomsToReserve.Add(rr.RoomId);
        }

        return viewModel;
    }

    public override async Task<Booking> ViewModelToEntityAsync(BookingViewModel viewModel)
    {
        if (viewModel.RoomsToReserve == null || viewModel.RoomsToReserve.Count == 0)
        {
            throw new Exception($"RoomsToReserve is null or empty");
        }

        if (viewModel.AdminUser == null)
        {
            throw new Exception($"AdminUser is null");
        }

        await LoadReservedRoomsAndCalculateTotals(viewModel);

        if (viewModel.ReservedRooms == null)
        {
            throw new Exception($"ReservedRooms is null");
        }

        Booking booking = new()
        {
            CheckinDate = viewModel.CheckInDate,
            CheckoutDate = viewModel.CheckOutDate,
            Contact = viewModel.Contact ?? throw new Exception("Contact is null."),
            Totals = viewModel.Totals,
            BookingTimeline = new()
            {
                logService.CreateLog($"{viewModel.AdminUser.RoleAndName} created the Booking.")
            },
            Status = BookingStatus.New,
        };

        await GenerateReservationsAndBookingItems(viewModel, booking);

        return booking;
    }

    public async Task LoadReservedRoomsAndCalculateTotals(BookingViewModel viewModel)
    {
        if (viewModel.RoomsToReserve == null || viewModel.RoomsToReserve.Count == 0)
        {
            return;
        }

        viewModel.ReservedRooms = await roomService.GetByIds(viewModel.RoomsToReserve);

        viewModel.Totals = totalsService.CalculateTotals(viewModel);
        viewModel.RoomsPrice = await totalsService.CalculateTotals<TotalsCategoryModifier>(viewModel.Totals);
        viewModel.RoomFeaturesPrice = await totalsService.CalculateTotals<TotalsFeatureModifier>(viewModel.Totals);
        viewModel.GrandTotal = await totalsService.CalculateGrandTotal(viewModel.Totals);
    }

    public async Task GenerateReservationsAndBookingItems(BookingViewModel viewModel, Booking booking)
    {
        List<RoomReservation> roomReservations = new();
        List<Room> rooms = await roomService.GetByIds(viewModel.RoomsToReserve ?? new());
        List<BookingItem> bookingItems = new();

        foreach (Room room in rooms)
        {
            RoomReservation reservation = CreateRoomReservation(room);
            BookingItem item = CreateBookingItem(room);
            item.RoomReservations = new()
            {
                reservation
            };
            
            roomReservations.Add(reservation);
            bookingItems.Add(item);
        }

        booking.ReservedRooms = roomReservations;
        booking.BookingItems = SquashBookingItems(bookingItems);
    }

    public RoomReservation CreateRoomReservation(Room room)
    {
        return new RoomReservation
        {
            RoomId = room.Id,
            Room = room,
        };
    }

    public BookingItem CreateBookingItem(Room room)
    {
        return new BookingItem
        {
            RoomCategoryId = room.CategoryId,
            TargetCapacity = room.Capacity,
            Price = room.Price,
            Quantity = 1,
            DesiredFeatures = CreateBookingItemRoomFeatures(room),
        };
    }

    public List<BookingItem> SquashBookingItems(List<BookingItem> items)
    {
        int maxIterations = items.Count;

        List<BookingItem> squashedItems = new();

        for (int i = 0; items.Count > 0 && i < maxIterations; i++)
        {
            List<BookingItem> identical = GetIdenticalItems(items[0], items);

            int totalQty = identical.Sum(i => i.Quantity);
            identical[0].Quantity = totalQty;

            List<RoomReservation> mergedReservations = new();

            foreach (BookingItem item in identical)
            {
                if (item.RoomReservations == null) continue;
                mergedReservations.AddRange(item.RoomReservations);
            }

            identical[0].RoomReservations = mergedReservations;

            squashedItems.Add(identical[0]);
            items.RemoveAll(identical.Contains);
        }

        return squashedItems;
    }

    public List<BookingItem> GetIdenticalItems(BookingItem compareTo, List<BookingItem> items)
    {
        return items.Where(item => CompareBookingItems(compareTo, item)).ToList();
    }

    public bool CompareBookingItems(BookingItem item1, BookingItem item2)
    {
        return item1.RoomCategoryId == item2.RoomCategoryId
            && item1.TargetCapacity == item2.TargetCapacity
            && item1.Price == item2.Price;
    }

    public List<BookingItemRoomFeature> CreateBookingItemRoomFeatures(Room room)
    {
        if (room.Features == null)
        {
            throw new Exception("Room features not loaded!");
        }

        return room.Features.ConvertAll(feature => new BookingItemRoomFeature
        {
            RoomFeature = feature,
            RoomFeatureId = feature.Id
        });
    }

    public BookingStepContext GenerateBookingStepContext(BookingViewModel? viewModel, string? activeStep = null)
    {
        BookingStepContext context = new()
        {
            ActiveStep = activeStep ?? HOTEL_SELECTION_STEP_NAME
        };

        BookingStep hotel_selection = new()
        {
            Id = HOTEL_SELECTION_STEP_NAME,
            Content = "Hotel selection",
            Partial = "_ChooseHotelFieldset",
            ActionName = nameof(BookingController.BackToChooseHotel),
            Order = 0,
        };

        BookingStep room_reservation = new()
        {
            Id = ROOM_RESERVATION_STEP_NAME,
            Content = "Room reservation",
            Partial = "_ReserveFieldset",
            ActionName = nameof(BookingController.ReserveRooms),
            Order = 1,
            Disabled = viewModel == null || viewModel.HotelId == 0 || viewModel.CheckInDate == default || viewModel.CheckOutDate == default,
        };

        BookingStep contact = new()
        {
            Id = CONTACT_STEP_NAME,
            Content = "Contact",
            Partial = "_ContactFieldset",
            ActionName = nameof(BookingController.Contact),
            Order = 2,
            Disabled = room_reservation.Disabled || viewModel == null || viewModel.RoomsToReserve == null || viewModel.RoomsToReserve.Count == 0,
        };

        BookingStep summary = new()
        {
            Id = SUMMARY_STEP_NAME,
            Content = "Summary",
            Partial = "_SummaryFieldset",
            ActionName = nameof(BookingController.BookingSummary),
            Order = 3,
            Disabled = contact.Disabled || viewModel == null || viewModel.Contact == null || viewModel.Contact.FullName == null,
        };

        context.Steps = new()
        {
            hotel_selection,
            room_reservation,
            contact,
            summary,
        };

        return context;
    }
}
