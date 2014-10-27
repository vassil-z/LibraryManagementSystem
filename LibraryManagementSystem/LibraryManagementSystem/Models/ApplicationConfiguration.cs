using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace LibraryManagementSystem.Models
{
    public class ApplicationConfiguration
    {
        #region Paging
        public static int ItemsPerPage { get; private set; }        
        #endregion

        static ApplicationConfiguration()
        {
            #region Paging
            ItemsPerPage = ConfigurationManager.AppSettings["itemsPerPage"] != null ? Convert.ToInt32(ConfigurationManager.AppSettings["itemsPerPage"]) : 10;            
            #endregion
        }
    }
}