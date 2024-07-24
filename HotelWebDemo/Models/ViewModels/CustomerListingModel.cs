using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Models.ViewModels;

public class CustomerListingModel : ListingModel<Customer>
{
    public List<Country> Countries { get; set; }
}
