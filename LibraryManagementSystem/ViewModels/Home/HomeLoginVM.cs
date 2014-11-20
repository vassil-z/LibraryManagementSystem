using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.ViewModels.Home
{
    public class HomeLoginVM
    {             
        public string Email { get; set; }                
        public string Password { get; set; }
        public string RedirectUrl { get; set; }
    }
}