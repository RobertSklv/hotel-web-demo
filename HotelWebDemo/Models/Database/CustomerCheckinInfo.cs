using System.ComponentModel.DataAnnotations.Schema;

namespace HotelWebDemo.Models.Database;

[Table("CustomerCheckinInfos")]
public class CustomerCheckinInfo : BaseEntity
{
    public Customer? Customer { get; set; }

    public int CustomerId { get; set; }

    public CheckinInfo? CheckinInfo { get; set; }

    public int CheckinInfoId { get; set; }
}
