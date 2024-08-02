using HotelWebDemo.Models.Components.Admin.Tables;
using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Models.ViewModels;

public class ListingModel : IListingModel
{
    public const string DEFAULT_ORDER_BY = "Id";
    public const string DEFAULT_DIRECTION = "desc";
    public const int DEFAULT_PAGE = 1;
    public const int DEFAULT_PAGE_SIZE = 10;

    public static int[] PageSizes = new int[] { 10, 20, 50, 100 };

    public string ActionName { get; set; }

    public string? OrderBy { get; set; }

    public string? Direction { get; set; }

    public int? Page { get; set; }

    public int? PageSize { get; set; } = DEFAULT_PAGE_SIZE;

    public Dictionary<string, TableFilter>? Filters { get; set; }

    public string? SearchPhrase { get; set; }

    public Dictionary<string, string?> GenerateListingQuery()
    {
        Dictionary<string, string?> query = new();

        CheckDefaultPage();
        CheckDefaultPageSize();
        CheckDefaultOrderBy();
        CheckDefaultDirection();

        if (OrderBy != null)
        {
            query.Add("OrderBy", OrderBy);
        }

        if (Direction != null)
        {
            query.Add("Direction", Direction);
        }

        if (Page != null)
        {
            query.Add("Page", Page.ToString());
        }

        if (SearchPhrase != null)
        {
            query.Add("SearchPhrase", SearchPhrase);
        }

        if (Filters != null)
        {
            foreach (var kvp in Filters)
            {
                if (kvp.Value == null) continue;

                string operatorParamName = $"Filters.{kvp.Key}.Operator";
                string valueParamName = $"Filters.{kvp.Key}.Value";

                query.Add(operatorParamName, kvp.Value.Operator);
                query.Add(valueParamName, kvp.Value.Value);
            }
        }

        return query;
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

public class ListingModel<T> : ListingModel
    where T : IBaseEntity
{
    public Table<T> Table { get; set; }
}
