using HotelWebDemo.Data.Repositories;
using HotelWebDemo.Models.Components.Admin.Tables;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;

namespace HotelWebDemo.Services;

public class HotelService : CrudService<Hotel>, IHotelService
{
    public HotelService(IHotelRepository repository)
        : base(repository)
    {
    }

    public override Table<Hotel> CreateListingTable(ListingModel<Hotel> listingModel, PaginatedList<Hotel> items)
    {
        return base.CreateListingTable(listingModel, items)
            .SetSearchable(false)
            .AddRowActions(null, options => options.SetDeleteConfirmationMessage<Hotel>(
                hotel => $"Are you sure you want to remove hotel {hotel.Name}? This action cannot be undone."));
    }
}
