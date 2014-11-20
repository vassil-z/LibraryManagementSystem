using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryManagementSystem.DataAccess.Entities;
using LibraryManagementSystem.DataAccess.Services;

namespace LibraryManagementSystem.Models
{
    public class AuthenticationManager
    {
        private static AuthenticationService AuthenticationServiceInstance
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Session[typeof(AuthenticationService).Name] == null)
                {
                    HttpContext.Current.Session[typeof(AuthenticationService).Name] = new AuthenticationService();
                }

                return (AuthenticationService)HttpContext.Current.Session[typeof(AuthenticationService).Name];
            }
        }

        public static User LoggedUser
        {
            get { return AuthenticationManager.AuthenticationServiceInstance.LoggedUser; }
        }

        public static void Authenticate(string username, string password)
        {
            AuthenticationManager.AuthenticationServiceInstance.AuthenticateUser(username, password);
        }

        public static void Logout()
        {
            AuthenticationManager.AuthenticationServiceInstance.AuthenticateUser(null, null);
            HttpContext.Current.Session[typeof(AuthenticationManager).Name] = null;   
        }       
    }
}