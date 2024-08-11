using HotelWebDemo.Models.Components.Common;

namespace HotelWebDemo.Models.Components.Admin.Booking;

public class BookingStep : Link
{
    public string Partial { get; set; }

    public int Order { get; set; }

    public BookingStep()
    {
        Area = "Admin";
        Controller = "Booking";
    }
}
