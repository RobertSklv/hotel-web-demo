using System.ComponentModel.DataAnnotations;
using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Models.ViewModels;

public class BookingViewModel : ListingModel<Room>, IBookingViewModel
{
    public int Id { get; set; }

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

    public List<Room>? ReservedRooms { get; set; }

    public BookingContact? Contact { get; set; }

    public bool HasCustomGrandTotal { get; set; }

    public decimal? CustomGrandTotal { get; set; }

    public BookingTotals? Totals { get; set; }

    public BookingStatus Status { get; set; }

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

    public override void Copy(IListingModel? listingModel)
    {
        base.Copy(listingModel);

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
