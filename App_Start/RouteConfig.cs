using System.Web.Mvc;
using System.Web.Routing;

namespace do_an_web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "web_clothes.Controllers" }
            );
            routes.MapRoute(
                name: "admin",
                url: "admin/homeAdmin/{action}/{id}",
                defaults: new { controller = "homeAdmin", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
