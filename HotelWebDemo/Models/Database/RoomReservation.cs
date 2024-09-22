using System.ComponentModel.DataAnnotations.Schema;
using HotelWebDemo.Models.Attributes;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Models.Database;

[Table("RoomReservations")]
public class RoomReservation : BaseEntity
{
    [TableColumn]
    public Booking? Booking { get; set; }

    public int BookingId { get; set; }

    [TableColumn]
    public Room? Room { get; set; }

    public int RoomId { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    public BookingItem? BookingItem { get; set; }

    public int BookingItemId { get; set; }

    [ForeignKey(nameof(CheckinInfoId))]
    public CheckinInfo? CheckinInfo { get; set; }

    public int? CheckinInfoId { get; set; }

    [TableColumn(Filterable = false, Orderable = false, Searchable = false)]
    public string? Period
    {
        get
        {
            if (Booking == null)
            {
                throw new Exception("Booking not loaded.");
            }

            if (Booking.Totals == null)
            {
                throw new Exception("Booking totals not loaded.");
            }

            string period = $"{Booking.Totals.Nights} night";

            if (Booking.Totals.Nights > 1)
            {
                period += "s";
            }

            return period;
        }
    }
}
