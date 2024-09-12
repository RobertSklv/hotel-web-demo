using System.ComponentModel.DataAnnotations.Schema;

namespace HotelWebDemo.Models.Database;

[Table("CheckinInfos")]
public class CheckinInfo : BaseEntity
{
    public RoomReservation? RoomReservation { get; set; }

    public DateTime? CheckoutDate { get; set; }

    public List<CheckedInCustomer>? CheckedInCustomers { get; set; }

    public bool IsCheckedOut => CheckoutDate != null;
}
