using System.Web.Mvc;
using System.Web.Routing;

namespace MobileShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            // BotDetect requests must not be routed
            routes.IgnoreRoute("{*botdetect}", new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            routes.MapRoute(
                name: "Search Page",
                url: "tim-kiem",
                defaults: new { controller = "Home", action = "Search" },
                 namespaces: new[] { "MobileShop.Controllers" }
            );

            routes.MapRoute(
                name: "Login Page",
                url: "dang-nhap",
                defaults: new { controller = "Login", action = "Index" },
                 namespaces: new[] { "MobileShop.Controllers" }
            );

            routes.MapRoute(
                name: "Register Page",
                url: "dang-ky",
                defaults: new { controller = "Register", action = "Index" },
                 namespaces: new[] { "MobileShop.Controllers" }
            );

            routes.MapRoute(
                name: "Contact Page",
                url: "lien-he",
                defaults: new { controller = "Contact", action = "Index" },
                 namespaces: new[] { "MobileShop.Controllers" }
            );

            routes.MapRoute(
                name: "About Page",
                url: "gioi-thieu",
                defaults: new { controller = "About", action = "Index" },
                namespaces: new []{"MobileShop.Controllers"}
            );

            routes.MapRoute(
                name: "Detail Page",
                url: "dien-thoai/{metatitle}-{id}",
                defaults: new { controller = "Detail", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Category Page",
                url: "dien-thoai-{categoryName}",
                defaults: new { controller = "Category", action = "Index", categoryName = UrlParameter.Optional },
                 namespaces: new[] { "MobileShop.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                 namespaces: new[] { "MobileShop.Controllers" }
            );
        }
    }
}
