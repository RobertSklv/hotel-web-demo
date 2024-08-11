namespace HotelWebDemo.Models.Components.Admin.Booking;

public class BookingStepContext
{
    public string ActiveStep { get; set; }

    public List<BookingStep> Steps { get; set; }

    public BookingStep GetStep(string id)
    {
        return Steps.First(s => s.Id == id);
    }
}
