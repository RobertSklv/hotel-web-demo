namespace HotelWebDemo.Models.Components;

public class TableLink : Link
{
    public const string DEFAULT_ORDER_BY = "Id";
    public const string DEFAULT_DIRECTION = "desc";
    public const int DEFAULT_PAGE = 1;

    public const string PAGINATION_LINK_CLASS = "page-link";

    public string ActionName { get; set; }

    public string Content { get; set; }

    public string? OrderBy { get; set; }

    public string? Direction { get; set; }

    public int? Page { get; set; }

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
        Page ??= DEFAULT_PAGE;
        Page += offset;
        CheckDefaultPage();
        ClassList.Add(PAGINATION_LINK_CLASS);

        return this;
    }

    public TableLink SetOrder(string? propertyName = null)
    {
        propertyName ??= Content;
        bool propertyIsDefaultOrder = propertyName == "Id";

        OrderBy = propertyIsDefaultOrder ? null : propertyName;
        Direction = GetOppositeDirection();

        CheckDefaultOrderBy();
        CheckDefaultDirection();

        return this;
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
        if (Page == DEFAULT_PAGE)
        {
            Page = null;
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
