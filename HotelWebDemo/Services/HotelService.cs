using HotelWebDemo.Data.Repositories;
using HotelWebDemo.Models.Components;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.Database.Indexing;
using HotelWebDemo.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HotelWebDemo.Services;

public class HotelService : CrudService<Hotel, HotelIndex>, IHotelService
{
    public HotelService(IHotelRepository repository)
        : base(repository)
    {
    }

    public async Task<ListingModel<HotelIndex>> CreateHotelListingModel(ViewDataDictionary viewData)
    {
        ListingModel<HotelIndex> model = new()
        {
            ActionName = "Index",
            OrderBy = (string?)viewData["OrderBy"],
            Direction = (string?)viewData["Direction"],
            Page = (int?)viewData["Page"],
            Filter = (Dictionary<string, TableFilter>?)viewData["Filter"],
        };

        PaginatedList<HotelIndex> items = await List(model);

        Table<HotelIndex> table = new Table<HotelIndex>(model, items)
            .SetOrderable(true)
            .SetFilterable(true)
            .AddRowActions(null, options => options.SetDeleteConfirmationMessage<HotelIndex>(
                hotel => $"Are you sure you want to remove hotel {hotel.Name}? This action cannot be undone"))
            .AddPagination(items);

        model.Table = table;

        return model;
    }
}
