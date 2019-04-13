using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Knjizara
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Front",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Front",
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );
            /* ########## Front End ########## */
            /* ########## Back End ########## */
            routes.MapRoute(
                name: "Back",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Back",
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );

            /* ########## Back End ########## */
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Front",
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );
        }
    }
}
