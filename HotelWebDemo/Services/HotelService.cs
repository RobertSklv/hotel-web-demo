using HotelWebDemo.Data.Repositories;
using HotelWebDemo.Models.Components;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HotelWebDemo.Services;

public class HotelService : CrudService<Hotel>, IHotelService
{
    public HotelService(IHotelRepository repository)
        : base(repository)
    {
    }

    public async Task<ListingModel<Hotel>> CreateHotelListingModel(ViewDataDictionary viewData)
    {
        ListingModel<Hotel> model = new()
        {
            ActionName = "Index",
            OrderBy = (string?)viewData["OrderBy"],
            Direction = (string?)viewData["Direction"],
            Page = (int?)viewData["Page"],
            Filter = (Dictionary<string, TableFilter>?)viewData["Filter"],
        };

        PaginatedList<Hotel> items = await List(model);

        Table<Hotel> table = new Table<Hotel>(model, items)
            .SetOrderable(true)
            .SetFilterable(true)
            .AddRowActions(null, options => options.SetDeleteConfirmationMessage<Hotel>(
                hotel => $"Are you sure you want to remove hotel {hotel.Name}? This action cannot be undone"))
            .AddPagination(items);

        model.Table = table;

        return model;
    }
}
