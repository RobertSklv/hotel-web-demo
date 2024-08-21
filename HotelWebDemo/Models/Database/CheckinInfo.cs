using System.ComponentModel.DataAnnotations.Schema;

namespace HotelWebDemo.Models.Database;

[Table("CheckinInfos")]
public class CheckinInfo : BaseEntity
{
    public RoomReservation? RoomReservation { get; set; }

    public int RoomReservationId { get; set; }

    public DateTime? CheckoutDate { get; set; }

    public List<CustomerCheckinInfo>? CustomerCheckinInfos { get; set; }

    public bool IsCheckedOut => CheckoutDate != null;
}
