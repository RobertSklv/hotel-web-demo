namespace HotelWebDemo.Models.Components.Common;

public interface IRouteElement
{
    string? Route { get; set; }

    string? AreaName { get; set; }

    string? ControllerName { get; set; }

    string? ActionName { get; set; }

    Dictionary<string, object>? RequestParameters { get; set; }

    string GetRoute();

    string GetRoute(int id);
}
