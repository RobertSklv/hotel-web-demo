using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Models.ViewModels;

public class BookingRoomSelectListingModel : ListingModel<Room>, IBookingViewModel
{
    public int Id { get; set; }

    public Hotel? Hotel { get; set; }

    public int HotelId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime ExpirationDate { get; set; }

    public List<int>? RoomsToReserve { get; set; }

    public List<Room>? ReservedRooms { get; set; }

    public override Dictionary<string, string?> GenerateListingQuery()
    {
        var query = base.GenerateListingQuery();

        query.Add(nameof(HotelId), HotelId.ToString());
        query.Add(nameof(StartDate), StartDate.ToString());
        query.Add(nameof(ExpirationDate), ExpirationDate.ToString());

        for (int i = 0; i < RoomsToReserve?.Count; i++)
        {
            query.Add(nameof(RoomsToReserve) + $"[{i}]", RoomsToReserve[i].ToString());
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
            StartDate = bookingVM.StartDate;
            ExpirationDate = bookingVM.ExpirationDate;
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
            bookingVM.StartDate = StartDate;
            bookingVM.ExpirationDate = ExpirationDate;
            bookingVM.RoomsToReserve = RoomsToReserve != null ? new(RoomsToReserve) : null;
        }
    }
}
