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

    public override async Task<Table<Hotel>> CreateListingTable(ListingModel<Hotel> listingModel, PaginatedList<Hotel> items)
    {
        return (await base.CreateListingTable(listingModel, items))
            .SetSearchable(false);
    }

    public override RowAction CustomizeDeleteRowAction(RowAction action)
    {
        return base.CustomizeDeleteRowAction(action)
            .SetConfirmationMessage<Hotel>(hotel => $"Are you sure you want to remove hotel {hotel.Name}? This action cannot be undone.");
    }
}
