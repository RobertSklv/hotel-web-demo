using HotelWebDemo.Areas.Admin.Controllers;
using HotelWebDemo.Data.Repositories;
using HotelWebDemo.Models.Components.Admin.Booking;
using HotelWebDemo.Models.Components.Admin.Tables;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;

namespace HotelWebDemo.Services;

public class BookingService : CrudService<Booking, BookingRoomSelectListingModel>, IBookingService
{
    public const string HOTEL_SELECTION_STEP_NAME = "hotel_selection";
    public const string ROOM_RESERVATION_STEP_NAME = "room_reservation";
    public const string SUMMARY_STEP_NAME = "summary";

    private readonly IRoomService roomService;

    public BookingService(IBookingRepository repository, IRoomService roomService)
        : base(repository)
    {
        this.roomService = roomService;
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
            .RemoveColumn("Id")
            .RemoveColumn("Enabled")
            .RemoveColumn("Hotel")
            .RemoveColumn("CreatedAt")
            .RemoveColumn("UpdatedAt")
            .AddRowAction("Reserve");

        return listingModel;
    }

    public override BookingRoomSelectListingModel EntityToViewModel(Booking entity)
    {
        return new BookingRoomSelectListingModel
        {
            StartDate = entity.StartDate,
            ExpirationDate = entity.ExpirationDate,
        };
    }

    public override Booking ViewModelToEntity(BookingRoomSelectListingModel model)
    {
        return new Booking
        {
            StartDate = model.StartDate,
            ExpirationDate = model.ExpirationDate,
        };
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
            QueryParameters = new()
        };

        if (viewModel != null)
        {
            if (viewModel.HotelId != default)
            {
                hotel_selection.QueryParameters.Add(nameof(BookingRoomSelectListingModel.HotelId), viewModel.HotelId.ToString());
            }

            if (viewModel.StartDate != default)
            {
                hotel_selection.QueryParameters.Add(nameof(BookingRoomSelectListingModel.StartDate), viewModel.StartDate.ToString());
            }

            if (viewModel.ExpirationDate != default)
            {
                hotel_selection.QueryParameters.Add(nameof(BookingRoomSelectListingModel.ExpirationDate), viewModel.ExpirationDate.ToString());
            }
        }

        BookingStep room_reservation = new()
        {
            Id = ROOM_RESERVATION_STEP_NAME,
            Content = "Room reservation",
            Partial = "_ReserveFieldset",
            Action = nameof(BookingController.ReserveRooms),
            Order = 1,
            Disabled = viewModel == null || viewModel.HotelId == 0 || viewModel.StartDate == default || viewModel.ExpirationDate == default,
            QueryParameters = hotel_selection.QueryParameters,
        };

        BookingStep summary = new()
        {
            Id = SUMMARY_STEP_NAME,
            Content = "Summary",
            Partial = "_SummaryFieldset",
            Action = nameof(BookingController.BookingSummary),
            Order = 2,
            Disabled = room_reservation.Disabled || viewModel == null || viewModel.RoomsToReserve == null || viewModel.RoomsToReserve.Count == 0,
            QueryParameters = new(viewModel?.GenerateListingQuery() ?? new())
        };

        context.Steps = new()
        {
            hotel_selection,
            room_reservation,
            summary,
        };

        return context;
    }
}
