using System.Web.Mvc;
using System.Web.Routing;

namespace SupplyManagement_NET48
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Role",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Role", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Vendor",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Vendor", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Employee",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Employee", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "AccountVendor",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "AccountVendor", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Account",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Project",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Project", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "AccountRole",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "AccountRole", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
