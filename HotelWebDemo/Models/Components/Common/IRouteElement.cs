namespace HotelWebDemo.Models.Components.Common;

public interface IRouteElement
{
    string? Route { get; set; }

    string? Area { get; set; }

    string? Controller { get; set; }

    string? Action { get; set; }

    Dictionary<string, object>? RequestParameters { get; set; }

    string GetRoute();

    string GetRoute(int id);
}
