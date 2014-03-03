// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RouteConfig.cs" company="N/A">
//   Public domain
// </copyright>
// <summary>
//   Defines the RouteConfig type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PassCache
{
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// The route config.
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// The routes register.
        /// </summary>
        /// <param name="routes">
        /// The routes.
        /// </param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Get",
                url: "Get",
                defaults: new { controller = "Main", action = "Get" });

            routes.MapRoute(
                name: "Default",
                url: string.Empty,
                defaults: new { controller = "Main", action = "Set", id = UrlParameter.Optional });
        }
    }
}