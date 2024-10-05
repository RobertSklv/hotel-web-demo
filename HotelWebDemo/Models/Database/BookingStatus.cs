namespace HotelWebDemo.Models.Database;

public enum BookingStatus
{
    New,
    PendingCheckin,
    CheckedIn,
    Cancelled,
    PendingCheckout,
    CheckedOut,
    NoShow
}
