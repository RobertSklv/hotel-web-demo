using HotelWebDemo.Models.Components;
using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Models.ViewModels;

public abstract class ListingModel
{
    public const string DEFAULT_ORDER_BY = "Id";
    public const string DEFAULT_DIRECTION = "desc";
    public const int DEFAULT_PAGE = 1;
    public const int DEFAULT_PAGE_SIZE = 10;
}

public class ListingModel<T> : ListingModel, IListingModel
    where T : IBaseEntity
{
    public string ActionName { get; set; }

    public string? OrderBy { get; set; }

    public string? Direction { get; set; }

    public int? Page { get; set; }

    public int PageSize { get; set; } = DEFAULT_PAGE_SIZE;

    public Dictionary<string, TableFilter>? Filter { get; set; }

    public Table<T> Table { get; set; }
}
