using System.Web.Mvc;
using System.Web.Routing;
using Web.Client.Helpers.Extensions;

namespace Web.Client
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.html/{*pathInfo}");
            routes.IgnoreRoute("Scripts/{*pathinfo}");
            routes.IgnoreRoute("Content/{*pathinfo}");

            routes.MapRouteEx("TemperatureIndex", "", "Temperature", "Index");
            routes.MapRouteEx("TemperatureGetTemperature", "ajax/GetTemperature", "Temperature", "GetTemperature");

            routes.MapRouteEx("CatchAll", "{*url}", new { controller = "Temperature", action = "PageNotFound" });

        }
    }
}
