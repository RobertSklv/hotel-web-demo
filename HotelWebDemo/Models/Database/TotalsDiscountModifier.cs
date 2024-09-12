using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelWebDemo.Models.Database;

[Table("TotalsDiscountModifiers")]
public class TotalsDiscountModifier : TotalsModifier
{
    //TODO: add discount ID foreign key
}
