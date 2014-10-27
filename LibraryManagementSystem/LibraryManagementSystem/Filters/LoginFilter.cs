using System;
using System.Web.Mvc;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.ViewModels.Home;

namespace LibraryManagementSystem.Filters
{
    public class LoginFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewData["HomeLoginVM"] = new HomeLoginVM();
            
            base.OnActionExecuting(filterContext);
        }
    }
}