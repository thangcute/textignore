using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Humax.Ess.Api.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "api/v1/{controller}/{action}/{id}",
                defaults: new { controller = "Authentication", action = "Test", id = UrlParameter.Optional }
            );
        }
    }
}