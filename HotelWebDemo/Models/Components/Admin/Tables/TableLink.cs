using HotelWebDemo.Models.Components.Common;
using HotelWebDemo.Models.ViewModels;

namespace HotelWebDemo.Models.Components.Admin.Tables;

public class TableLink : Link
{
    public const string PAGINATION_LINK_CLASS = "page-link";

    public string ActionName { get; set; }

    public string? OrderBy { get; set; }

    public string? Direction { get; set; }

    public int? Page { get; set; }

    public int PageSize { get; set; }

    public Dictionary<string, TableFilter>? Filters { get; set; }

    public string? SearchPhrase { get; set; }

    public IListingModel Listing { get; set; }

    public TableLink(IListingModel listing, string content)
    {
        ActionName = listing.ActionName;
        Content = content;
        Listing = listing;
    }

    public TableLink SetPage(int page)
    {
        Page = page;
        ClassList.Add(PAGINATION_LINK_CLASS);

        return this;
    }

    public TableLink SetPageOffset(int offset)
    {
        Page ??= ListingModel.DEFAULT_PAGE;
        Page += offset;
        ClassList.Add(PAGINATION_LINK_CLASS);

        return this;
    }

    public TableLink SetOrder(string? propertyName = null)
    {
        propertyName ??= Content;
        bool propertyIsDefaultOrder = propertyName == ListingModel.DEFAULT_ORDER_BY;

        OrderBy = propertyIsDefaultOrder ? null : propertyName;
        Direction = GetOppositeDirection();

        return this;
    }

    public IListingModel GetListingModel()
    {
        IListingModel clone = Listing.Clone();
        clone.ActionName = ActionName;
        clone.OrderBy = OrderBy;
        clone.Direction = Direction;
        clone.Page = Page;
        clone.PageSize = PageSize;
        clone.Filters = Filters;
        clone.SearchPhrase = SearchPhrase;

        return clone;
    }

    private string GetOppositeDirection()
    {
        if (Direction != null && Direction == "asc")
        {
            return "desc";
        }

        return "asc";
    }
}
