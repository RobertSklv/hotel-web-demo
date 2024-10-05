using System.ComponentModel.DataAnnotations;
using HotelWebDemo.Models.Components.Common;
using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Models.ViewModels;

public class BookingViewModel : ListingModel<Room>, IBookingViewModel
{
    public new int Id { get; set; }

    public Hotel? Hotel { get; set; }

    public int HotelId { get; set; }

    public AdminUser? AdminUser { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Check-in date")]
    public DateTime CheckInDate { get; set; } = DateTime.UtcNow;

    [DataType(DataType.Date)]
    [Display(Name = "Check-out date")]
    public DateTime CheckOutDate { get; set; } = DateTime.UtcNow;

    public List<int>? RoomsToReserve { get; set; }

    public List<RoomViewModel>? ReservedRooms { get; set; }

    public List<RoomReservation>? RoomReservations { get; set; }

    public BookingContact? Contact { get; set; }

    public decimal RoomsPrice { get; set; }

    public decimal RoomFeaturesPrice { get; set; }

    [Display(Name = "Has custom grand total")]
    public bool HasCustomGrandTotal { get; set; }

    public decimal GrandTotal { get; set; }

    [Display(Name = "Custom grand total")]
    public decimal? CustomGrandTotal { get; set; }

    public BookingTotals? Totals { get; set; }

    public BookingStatus Status { get; set; }

    public bool CanBeCancelled { get; set; }

    public List<BookingEventLog>? Timeline { get; set; }

    public int Nights => (CheckOutDate - CheckInDate).Days;

    public string NightsLabel => Nights + (Nights > 1 ? " nights" : " night");

    public override Dictionary<string, string?> GenerateListingQuery()
    {
        var query = base.GenerateListingQuery();

        query.Add(nameof(HotelId), HotelId.ToString());
        query.Add(nameof(CheckInDate), CheckInDate.ToString());
        query.Add(nameof(CheckOutDate), CheckOutDate.ToString());

        for (int i = 0; i < RoomsToReserve?.Count; i++)
        {
            query.Add(nameof(RoomsToReserve) + $"[{i}]", RoomsToReserve[i].ToString());
        }

        if (Contact != null)
        {
            if (Contact.FullName != null)
            {
                query.Add("Contact.FullName", Contact.FullName.ToString());
            }

            if (Contact.Phone != null)
            {
                query.Add("Contact.Phone", Contact.Phone.ToString());
            }

            if (Contact.Email != null)
            {
                query.Add("Contact.Email", Contact.Email.ToString());
            }

            if (Contact.Note != null)
            {
                query.Add("Contact.Note", Contact.Note.ToString());
            }
        }

        query.Add(nameof(HasCustomGrandTotal), HasCustomGrandTotal.ToString());

        if (CustomGrandTotal != null)
        {
            query.Add(nameof(CustomGrandTotal), CustomGrandTotal.ToString());
        }

        return query;
    }

    public ColorClass GetStatusColor()
    {
        return Status switch
        {
            BookingStatus.New => Components.Common.ColorClass.Info,
            BookingStatus.PendingCheckin => Components.Common.ColorClass.Info,
            BookingStatus.CheckedIn => Components.Common.ColorClass.Primary,
            BookingStatus.Cancelled => Components.Common.ColorClass.Danger,
            BookingStatus.PendingCheckout => Components.Common.ColorClass.Info,
            BookingStatus.CheckedOut => Components.Common.ColorClass.Success,
            BookingStatus.NoShow => Components.Common.ColorClass.Danger,
            _ => Components.Common.ColorClass.Secondary,
        };
    }

    public string GetStatusLabel()
    {
        return Status switch
        {
            BookingStatus.New => "New",
            BookingStatus.PendingCheckin => "Pending check-in",
            BookingStatus.CheckedIn => "Checked in",
            BookingStatus.Cancelled => "Cancelled",
            BookingStatus.PendingCheckout => "Pending checkout",
            BookingStatus.CheckedOut => "Checked out",
            BookingStatus.NoShow => "No-show",
            _ => Status.ToString(),
        };
    }

    public string GetStatusColorAsString()
    {
        return GetStatusColor().ToString().ToLower();
    }

    public override void CopyFrom(IListingModel? listingModel)
    {
        base.CopyFrom(listingModel);

        if (listingModel is BookingViewModel bookingVM)
        {
            Hotel = bookingVM.Hotel;
            HotelId = bookingVM.HotelId;
            CheckInDate = bookingVM.CheckInDate;
            CheckOutDate = bookingVM.CheckOutDate;
            RoomsToReserve = bookingVM.RoomsToReserve;
        }
    }

    public override IListingModel Instantiate()
    {
        return new BookingViewModel();
    }

    public override void Clone(IListingModel listingModel)
    {
        base.Clone(listingModel);

        if (listingModel is BookingViewModel bookingVM)
        {
            bookingVM.Hotel = Hotel;
            bookingVM.HotelId = HotelId;
            bookingVM.CheckInDate = CheckInDate;
            bookingVM.CheckOutDate = CheckOutDate;
            bookingVM.RoomsToReserve = RoomsToReserve != null ? new(RoomsToReserve) : null;
        }
    }
}
