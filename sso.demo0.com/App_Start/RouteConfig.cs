using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace sso.demo0.com
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "GetUser",
                url: "getuser",
                defaults: new { controller = "Home", action = "GetUser" }
            );
            routes.MapRoute(
               name: "GetToken",
               url: "gettoken",
               defaults: new { controller = "Home", action = "GetToken" }
           );
            routes.MapRoute(
                name: "Login",
                url: "login",
                defaults: new { controller = "Home", action = "Login" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}