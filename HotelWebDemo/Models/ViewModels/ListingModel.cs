using HotelWebDemo.Models.Components.Admin.Tables;
using HotelWebDemo.Models.Components.Common;
using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Models.ViewModels;

public class ListingModel : RouteElement, IListingModel
{
    public const string DESCENDING = "desc";
    public const string ASCENDING = "asc";
    public const string DEFAULT_ORDER_BY = "Id";
    public const string DEFAULT_DIRECTION = DESCENDING;
    public const int DEFAULT_PAGE = 1;
    public const int DEFAULT_PAGE_SIZE = 10;

    public static int[] PageSizes = new int[] { 10, 20, 50, 100 };

    public string? OrderBy { get; set; }

    public string? Direction { get; set; }

    public int? Page { get; set; }

    public int? PageSize { get; set; } = DEFAULT_PAGE_SIZE;

    public Dictionary<string, TableFilter>? Filters { get; set; }

    public string? SearchPhrase { get; set; }

    public virtual Dictionary<string, string?> GenerateListingQuery()
    {
        Dictionary<string, string?> query = new();

        CheckDefaultPage();
        CheckDefaultPageSize();
        CheckDefaultOrderBy();
        CheckDefaultDirection();

        if (OrderBy != null)
        {
            query.Add(nameof(OrderBy), OrderBy);
        }

        if (Direction != null)
        {
            query.Add(nameof(Direction), Direction);
        }

        if (Page != null)
        {
            query.Add(nameof(Page), Page.ToString());
        }

        if (SearchPhrase != null)
        {
            query.Add(nameof(SearchPhrase), SearchPhrase);
        }

        if (Filters != null)
        {
            foreach (var kvp in Filters)
            {
                if (kvp.Value == null) continue;

                string operatorParamName = $"{nameof(Filters)}.{kvp.Key}.{nameof(TableFilter.Operator)}";
                string valueParamName = $"{nameof(Filters)}.{kvp.Key}.{nameof(TableFilter.Value)}";

                query.Add(operatorParamName, kvp.Value.Operator);
                query.Add(valueParamName, kvp.Value.Value);
            }
        }

        return query;
    }

    public virtual void CopyFrom(IListingModel? listingModel)
    {
        Route = listingModel?.Route;
        AreaName = listingModel?.AreaName;
        ControllerName = listingModel?.ControllerName;
        ActionName = listingModel?.ActionName ?? "Index";
        RequestParameters = listingModel?.RequestParameters;
        OrderBy = listingModel?.OrderBy ?? DEFAULT_ORDER_BY;
        Direction = listingModel?.Direction ?? DEFAULT_DIRECTION;
        Page = listingModel?.Page ?? DEFAULT_PAGE;
        PageSize = listingModel?.PageSize ?? DEFAULT_PAGE_SIZE;
        Filters = listingModel?.Filters;
        SearchPhrase = listingModel?.SearchPhrase;
    }

    public virtual IListingModel Instantiate()
    {
        return new ListingModel();
    }

    public virtual void Clone(IListingModel listingModel)
    {
        listingModel.Route = Route;
        listingModel.OrderBy = OrderBy;
        listingModel.Direction = Direction;
        listingModel.Page = Page;
        listingModel.PageSize = PageSize;
        listingModel.Filters = Filters != null ? new(Filters) : null;
        listingModel.SearchPhrase = SearchPhrase;
    }

    public IListingModel Clone()
    {
        IListingModel clone = Instantiate();
        Clone(clone);

        return clone;
    }

    private void CheckDefaultPage()
    {
        if (Page == DEFAULT_PAGE)
        {
            Page = null;
        }
    }

    private void CheckDefaultPageSize()
    {
        if (PageSize == DEFAULT_PAGE_SIZE)
        {
            PageSize = null;
        }
    }

    private void CheckDefaultOrderBy()
    {
        if (OrderBy == DEFAULT_ORDER_BY)
        {
            OrderBy = null;
        }
    }

    private void CheckDefaultDirection()
    {
        if (Direction == DEFAULT_DIRECTION)
        {
            Direction = null;
        }
    }
}

public class ListingModel<TEntity> : ListingModel
    where TEntity : IBaseEntity
{
    public Table<TEntity>? Table { get; set; }
}
