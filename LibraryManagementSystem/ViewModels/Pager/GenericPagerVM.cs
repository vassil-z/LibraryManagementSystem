using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.ViewModels.Pager
{
    public class GenericPagerVM
    {
        public string Prefix { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }

        public int CurrentPage { get; set; }
        public int PagesCount { get; set; }

        public Dictionary<string, object> CurrentParameters { get; set; }
    }
}