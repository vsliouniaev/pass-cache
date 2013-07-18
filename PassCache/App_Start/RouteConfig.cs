using System.Web.Mvc;
using System.Web.Routing;

namespace PassCache.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Get",
                url: "Get",
                defaults: new { controller = "Main", action = "Get" }
            );

            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "Main", action = "Set", id = UrlParameter.Optional }
            );
        }
    }
}