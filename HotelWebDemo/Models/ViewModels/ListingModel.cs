using HotelWebDemo.Models.Components.Admin.Tables;
using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Models.ViewModels;

public class ListingModel : IListingModel
{
    public const string DEFAULT_ORDER_BY = "Id";
    public const string DEFAULT_DIRECTION = "desc";
    public const int DEFAULT_PAGE = 1;
    public const int DEFAULT_PAGE_SIZE = 10;

    public string ActionName { get; set; }

    public string? OrderBy { get; set; }

    public string? Direction { get; set; }

    public int? Page { get; set; }

    public int PageSize { get; set; } = DEFAULT_PAGE_SIZE;

    public Dictionary<string, TableFilter>? Filters { get; set; }

    public string? SearchPhrase { get; set; }
}

public class ListingModel<T> : ListingModel
    where T : IBaseEntity
{
    public Table<T> Table { get; set; }
}
