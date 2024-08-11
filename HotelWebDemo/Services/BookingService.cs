using HotelWebDemo.Areas.Admin.Controllers;
using HotelWebDemo.Data.Repositories;
using HotelWebDemo.Models.Components.Admin.Booking;
using HotelWebDemo.Models.Components.Admin.Tables;
using HotelWebDemo.Models.Components.Common;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;

namespace HotelWebDemo.Services;

public class BookingService : CrudService<Booking, BookingRoomSelectListingModel>, IBookingService
{
    public const string HOTEL_SELECTION_STEP_NAME = "hotel_selection";
    public const string ROOM_RESERVATION_STEP_NAME = "room_reservation";
    public const string CONTACT_STEP_NAME = "contact";
    public const string SUMMARY_STEP_NAME = "summary";

    private readonly IRoomService roomService;
    private readonly IRoomCategoryService categoryService;

    public BookingService(IBookingRepository repository, IRoomService roomService, IRoomCategoryService categoryService)
        : base(repository)
    {
        this.roomService = roomService;
        this.categoryService = categoryService;
    }

    public async Task<ListingModel<Room>> CreateRoomListing(BookingRoomSelectListingModel? viewModel)
    {
        BookingRoomSelectListingModel listingModel = new();
        listingModel.Copy(viewModel);
        listingModel.ActionName = "ReserveRooms";

        PaginatedList<Room> items = await roomService.GetBookableRooms(listingModel);

        listingModel.Table = new Table<Room>(listingModel, items)
            .SetOrderable(true)
            .SetFilterable(true)
            .AddPagination(true)
            .RemoveColumn(nameof(Room.Id))
            .RemoveColumn(nameof(Room.Enabled))
            .RemoveColumn(nameof(Room.Hotel))
            .RemoveColumn(nameof(Room.CreatedAt))
            .RemoveColumn(nameof(Room.UpdatedAt))
            .AddRowAction("Reserve")
            .SetSelectableOptionsSource(nameof(Room.Category), categoryService.GetAll());

        return listingModel;
    }

    public override Table<Booking> CreateListingTable(ListingModel<Booking> listingModel, PaginatedList<Booking> items)
    {
        return base.CreateListingTable(listingModel, items)
            .RemoveColumn(nameof(BaseEntity.UpdatedAt))
            .AddRowAction("View", RequestMethod.Get, customizationCallback: a => a.SetSortOrder(0));
    }

    public override Table<Booking> CreateEditRowAction(Table<Booking> table)
    {
        return table;
    }

    public override BookingRoomSelectListingModel EntityToViewModel(Booking entity)
    {
        BookingRoomSelectListingModel viewModel = new()
        {
            CheckInDate = entity.CheckInDate,
            CheckOutDate = entity.CheckOutDate,
            Contact = entity.Contact,
            RoomsToReserve = new(),
            ReservedRooms = new(),
        };

        if (entity.ReservedRooms == null || entity.ReservedRooms.Count == 0)
        {
            throw new Exception($"ReservedRooms is null or empty");
        }

        viewModel.Hotel = entity.ReservedRooms[0].Room.Category.Hotel;
        viewModel.HotelId = viewModel.Hotel.Id;

        foreach (RoomReservation rr in entity.ReservedRooms)
        {
            viewModel.ReservedRooms.Add(rr.Room);
            viewModel.RoomsToReserve.Add(rr.RoomId);
        }

        return viewModel;
    }

    public override Booking ViewModelToEntity(BookingRoomSelectListingModel viewModel)
    {
        Booking booking = new()
        {
            CheckInDate = viewModel.CheckInDate,
            CheckOutDate = viewModel.CheckOutDate,
            Contact = viewModel.Contact ?? throw new Exception("Contact is null."),
            ReservedRooms = new()
        };

        if (viewModel.RoomsToReserve == null || viewModel.RoomsToReserve.Count == 0)
        {
            throw new Exception($"RoomsToReserve is null or empty");
        }

        foreach (int roomId in viewModel.RoomsToReserve)
        {
            booking.ReservedRooms.Add(CreateRoomReservation(roomId));
        }

        return booking;
    }

    public async Task ConvertReservedRoomIdsIfAny(BookingRoomSelectListingModel viewModel)
    {
        if (viewModel.RoomsToReserve == null || viewModel.RoomsToReserve.Count == 0)
        {
            return;
        }

        viewModel.ReservedRooms = await roomService.GetByIds(viewModel.RoomsToReserve);
    }

    public RoomReservation CreateRoomReservation(int roomId)
    {
        Room room = roomService.Get(roomId) ?? throw new Exception($"No such room with ID {roomId} was found in the database.");

        return new RoomReservation
        {
            RoomId = roomId,
            Room = room,
        };
    }

    public BookingStepContext GenerateBookingStepContext(BookingRoomSelectListingModel? viewModel, string? activeStep = null)
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
            Action = nameof(BookingController.BackToChooseHotel),
            Order = 0,
        };

        BookingStep room_reservation = new()
        {
            Id = ROOM_RESERVATION_STEP_NAME,
            Content = "Room reservation",
            Partial = "_ReserveFieldset",
            Action = nameof(BookingController.ReserveRooms),
            Order = 1,
            Disabled = viewModel == null || viewModel.HotelId == 0 || viewModel.CheckInDate == default || viewModel.CheckOutDate == default,
        };

        BookingStep contact = new()
        {
            Id = CONTACT_STEP_NAME,
            Content = "Contact",
            Partial = "_ContactFieldset",
            Action = nameof(BookingController.Contact),
            Order = 2,
            Disabled = room_reservation.Disabled || viewModel == null || viewModel.RoomsToReserve == null || viewModel.RoomsToReserve.Count == 0,
        };

        BookingStep summary = new()
        {
            Id = SUMMARY_STEP_NAME,
            Content = "Summary",
            Partial = "_SummaryFieldset",
            Action = nameof(BookingController.BookingSummary),
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
