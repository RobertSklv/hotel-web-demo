using System.Text;

namespace HotelWebDemo.Models.Components.Common;

/// <summary>
/// Used for links/forms common data.
/// </summary>
public abstract class RouteElement : Element, IRouteElement
{
    public string? Route { get; set; }

    public string? AreaName { get; set; }

    public string? ControllerName { get; set; }

    public string? ActionName { get; set; }

    public Dictionary<string, object>? RequestParameters { get; set; }

    public virtual string GetRoute()
    {
        if (Route != null)
        {
            return Route;
        }

        StringBuilder sb = new();

        if (AreaName != null)
        {
            sb.Append('/');
            sb.Append(AreaName);
        }

        if (ControllerName != null)
        {
            sb.Append('/');
            sb.Append(ControllerName);
        }

        if (ActionName != null)
        {
            sb.Append('/');
            sb.Append(ActionName);
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
