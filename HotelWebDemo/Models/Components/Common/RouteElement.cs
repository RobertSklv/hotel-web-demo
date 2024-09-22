using System.Text;

namespace HotelWebDemo.Models.Components.Common;

/// <summary>
/// Used for links/forms common data.
/// </summary>
public abstract class RouteElement : Element, IRouteElement
{
    public string? Route { get; set; }

    public string? Area { get; set; }

    public string? Controller { get; set; }

    public string? Action { get; set; }

    public Dictionary<string, object>? RequestParameters { get; set; }

    public virtual string GetRoute()
    {
        if (Route != null)
        {
            return Route;
        }

        StringBuilder sb = new();

        if (Area != null)
        {
            sb.Append('/');
            sb.Append(Area);
        }

        if (Controller != null)
        {
            sb.Append('/');
            sb.Append(Controller);
        }

        if (Action != null)
        {
            sb.Append('/');
            sb.Append(Action);
        }

        return sb.ToString();
    }

    public virtual string GetRoute(int id)
    {
        if (Route != null)
        {
            return Route;
        }

        return GetRoute() + '/' + id;
    }
}
