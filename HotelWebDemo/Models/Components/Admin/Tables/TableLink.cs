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

    public Dictionary<string, TableFilter>? Filter { get; set; }

    public string? SearchPhrase { get; set; }

    public TableLink(string actionName, string content)
    {
        ActionName = actionName;
        Content = content;
    }

    public TableLink SetPage(int page)
    {
        Page = page;
        CheckDefaultPage();
        ClassList.Add(PAGINATION_LINK_CLASS);

        return this;
    }

    public TableLink SetPageOffset(int offset)
    {
        Page ??= ListingModel.DEFAULT_PAGE;
        Page += offset;
        CheckDefaultPage();
        ClassList.Add(PAGINATION_LINK_CLASS);

        return this;
    }

    public TableLink SetOrder(string? propertyName = null)
    {
        propertyName ??= Content;
        bool propertyIsDefaultOrder = propertyName == ListingModel.DEFAULT_ORDER_BY;

        OrderBy = propertyIsDefaultOrder ? null : propertyName;
        Direction = GetOppositeDirection();

        CheckDefaultPage();
        CheckDefaultOrderBy();
        CheckDefaultDirection();

        return this;
    }

    public Dictionary<string, string?> GetFilterQueryParameters()
    {
        Dictionary<string, string?> query = new();

        if (Filter != null)
        {
            foreach (var kvp in Filter)
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

    private string GetOppositeDirection()
    {
        if (Direction == null || Direction == "asc")
        {
            return "desc";
        }

        return "asc";
    }

    private void CheckDefaultPage()
    {
        if (Page == ListingModel.DEFAULT_PAGE)
        {
            Page = null;
        }
    }

    private void CheckDefaultOrderBy()
    {
        if (OrderBy == ListingModel.DEFAULT_ORDER_BY)
        {
            OrderBy = null;
        }
    }

    private void CheckDefaultDirection()
    {
        if (Direction == ListingModel.DEFAULT_DIRECTION)
        {
            Direction = null;
        }
    }
}
