namespace HotelWebDemo.Models.Components.Admin.Tables;

public class Pagination
{
    public const int INNER_PAGINATION_LINKS_COUNT = 9;
    public const string ELLIPSIS_LINK_CONTENT = "...";

    public int CurrentPage { get; set; }

    public int TotalPages { get; set; }

    public Table Table { get; }

    public Pagination(int currentPage, int totalPages, Table table)
    {
        CurrentPage = currentPage;
        TotalPages = totalPages;
        Table = table;
    }

    public List<TableLink> GeneratePaginationLinks()
    {
        List<TableLink> pageLinks = new();
        TableLink previous = Table.CreateLink("Previous").SetPageOffset(-1);
        pageLinks.Add(previous);

        if (CurrentPage - 1 <= 0)
        {
            previous.Disabled = true;
            previous.ClassList.Add("disabled");
        }

        int totalLinks = TotalPages > INNER_PAGINATION_LINKS_COUNT
            ? INNER_PAGINATION_LINKS_COUNT
            : TotalPages;

        for (int i = 1; i <= totalLinks; i++)
        {
            TableLink pageLink = GenerateLink(i, totalLinks);

            if (pageLink.Page == CurrentPage || CurrentPage == 1 && pageLink.Page == null)
            {
                pageLink.Disabled = true;
                pageLink.ClassList.Add("disabled");
            }

            pageLinks.Add(pageLink);
        }

        TableLink next = Table.CreateLink("Next").SetPageOffset(1);
        pageLinks.Add(next);

        if (CurrentPage + 1 > TotalPages)
        {
            next.Disabled = true;
            next.ClassList.Add("disabled");
        }

        return pageLinks;
    }

    private TableLink GenerateLink(int index, int totalLinks)
    {
        if (index == 1)
        {
            return CreateLink(index);
        }

        if (index == totalLinks)
        {
            return CreateLink(TotalPages);
        }

        int offsetStart = CurrentPage - INNER_PAGINATION_LINKS_COUNT / 2;

        if (offsetStart < 1)
        {
            offsetStart = 1;
        }

        int page = offsetStart + (index - 1);

        string? content = null;

        if (index == 2 && page > 2)
        {
            content = ELLIPSIS_LINK_CONTENT;
        }

        if (index == INNER_PAGINATION_LINKS_COUNT - 1 && page < TotalPages - 1)
        {
            content = ELLIPSIS_LINK_CONTENT;
        }

        return CreateLink(page, content);
    }

    private TableLink CreateLink(int page, string? content = null)
    {
        return Table.CreateLink(content ?? page.ToString()).SetPage(page);
    }
}
