using HotelWebDemo.Data.Repositories;
using HotelWebDemo.Models.Components;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.Database.Indexing;
using HotelWebDemo.Models.ViewModels;

namespace HotelWebDemo.Services;

public class HotelService : CrudService<Hotel, HotelIndex>, IHotelService
{
    public HotelService(IHotelRepository repository)
        : base(repository)
    {
    }

    public override Table<HotelIndex> CreateListingTable(ListingModel<HotelIndex> listingModel, PaginatedList<HotelIndex> items)
    {
        return base.CreateListingTable(listingModel, items)
            .AddRowActions(null, options => options.SetDeleteConfirmationMessage<HotelIndex>(
                hotel => $"Are you sure you want to remove hotel {hotel.Name}? This action cannot be undone."));
    }
}
