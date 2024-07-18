using HotelWebDemo.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HotelWebDemo.Models.Components;

public class TableContext
{
    public string ActionName { get; set; }

    public string? OrderBy { get; set; }

    public string? Direction { get; set; }

    public int? Page { get; set; }

    public TableContext(string actionName, ViewDataDictionary viewData)
    {
        ActionName = actionName;
        OrderBy = (string?)viewData["OrderBy"];
        Direction = (string?)viewData["Direction"];
        Page = (int?)viewData["Page"];
    }

    public TableLink CreateLink(string content)
    {
        return new(ActionName, content)
        {
            OrderBy = OrderBy,
            Direction = Direction,
            Page = Page
        };
    }

    public Pagination CreatePagination<T>(PaginatedList<T> paginatedList)
    {
        return new(paginatedList.PageIndex, paginatedList.TotalPages, this);
    }
}
