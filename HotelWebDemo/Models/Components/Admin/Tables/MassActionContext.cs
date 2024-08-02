using HotelWebDemo.Models.ViewModels;

namespace HotelWebDemo.Models.Components.Admin.Tables;

public class MassActionContext
{
    public List<MassAction> Actions { get; set; } = new();

    public IListingModel ListingModel { get; set; }
}
