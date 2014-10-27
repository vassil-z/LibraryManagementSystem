using System;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using LibraryManagementSystem.DataAccess.Entities;

namespace LibraryManagementSystem.ViewModels.Customers
{    
    public class CustomersEditCustomerVM
    {
        public int ID { get; set; }

        [DisplayName("Personal No.")]
        [Required(ErrorMessage = "* personal number required")]
        public int PersonalNumber { get; set; }       

        [DisplayName("First Name")]
        [Required(ErrorMessage = "* first name required")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "* last name required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "* email required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "* address required")]
        public string Address { get; set; }

        public string PicturePath { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "* birthday required")]
        [DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }

        [DisplayName("Date in")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "* date in required")]
        [DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateIn { get; set; }
        
        [DisplayName("Date out")]
        [DataType(DataType.Date)]
        [Required(AllowEmptyStrings = true)]
        [DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOut { get; set; }
    }
}