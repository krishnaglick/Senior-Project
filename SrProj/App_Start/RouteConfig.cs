using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace SrProj
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("{file}.js");
            routes.IgnoreRoute("{file}.html");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapHttpRoute(
                name: "API Default",
                routeTemplate: "API/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
