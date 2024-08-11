using System.ComponentModel.DataAnnotations;
using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Models.ViewModels;

public class BookingRoomSelectListingModel : ListingModel<Room>, IBookingViewModel
{
    public int Id { get; set; }

    public Hotel? Hotel { get; set; }

    public int HotelId { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Check-in date")]
    public DateTime CheckInDate { get; set; } = DateTime.UtcNow;

    [DataType(DataType.Date)]
    [Display(Name = "Check-out date")]
    public DateTime CheckOutDate { get; set; } = DateTime.UtcNow;

    public List<int>? RoomsToReserve { get; set; }

    public List<Room>? ReservedRooms { get; set; }

    public BookingContact? Contact { get; set; }
    
    public decimal RoomsPrice => ReservedRooms?.Sum(r => r.GetPrice(Nights)) ?? 0;

    public decimal FeaturesPrice => ReservedRooms?.Sum(r => r.GetFeaturesPrice(Nights)) ?? 0;

    public decimal TotalPrice => ReservedRooms?.Sum(r => r.GetTotalPriceForPeriod(Nights)) ?? 0;

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

        return query;
    }

    public override void Copy(IListingModel? listingModel)
    {
        base.Copy(listingModel);

        if (listingModel is BookingRoomSelectListingModel bookingVM)
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
        return new BookingRoomSelectListingModel();
    }

    public override void Clone(IListingModel listingModel)
    {
        base.Clone(listingModel);

        if (listingModel is BookingRoomSelectListingModel bookingVM)
        {
            bookingVM.Hotel = Hotel;
            bookingVM.HotelId = HotelId;
            bookingVM.CheckInDate = CheckInDate;
            bookingVM.CheckOutDate = CheckOutDate;
            bookingVM.RoomsToReserve = RoomsToReserve != null ? new(RoomsToReserve) : null;
        }
    }
}
