using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.DataAccess.Repositories;
using LibraryManagementSystem.DataAccess.DataAccessLayer;

namespace LibraryManagementSystem.Filters
{
    public class AuthenticationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            RolesRepository rolesRepository = new RolesRepository(context);
            if (AuthenticationManager.LoggedUser == null)
            {
                filterContext.HttpContext.Response.Redirect("~/?RedirectUrl=" + filterContext.HttpContext.Request.Url);
                filterContext.Result = new EmptyResult();
                return;
            }
            else
            {
                foreach (var role in AuthenticationManager.LoggedUser.Roles)
                {
                    if (!rolesRepository.Exists(role.ID, filterContext.RouteData.Values["Controller"].ToString(), filterContext.RouteData.Values["Action"].ToString()))
                    {
                        filterContext.HttpContext.Response.Redirect("~/");
                    }
                    else
                    {
                        base.OnActionExecuting(filterContext);
                    }
                }
            }
        }
    }
}