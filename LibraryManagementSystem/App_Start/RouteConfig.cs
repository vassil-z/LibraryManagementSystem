using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LibraryManagementSystem
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "AddBookAuthor",
                url: "Books/AddBookAuthor/{id}/{authorID}",
                defaults: new
                {
                    controller = "Books",
                    action = "AddBookAuthor",
                    id = UrlParameter.Optional,
                    authorID = UrlParameter.Optional
                });

            routes.MapRoute(
                name: "Barcodes",
                url: "Barcodes/EditBarcode/{id}/{bookID}",
                defaults: new
                {
                    controller = "Barcodes",
                    action = "EditBarcode",
                    id = UrlParameter.Optional,
                    bookID = UrlParameter.Optional
                });

            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );
        }
    }
}
