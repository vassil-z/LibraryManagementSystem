using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LibraryManagementSystem.ViewModels.Pager;
using LibraryManagementSystem.DataAccess.Entities;

namespace LibraryManagementSystem.ViewModels.Customers
{
    public class CustomersIndexVM
    {
        public int PersonalNumber { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}", ApplyFormatInEditMode = true)]        
        public DateTime BirthdayStartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthdayEndDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateInStartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateInEndDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOutStartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOutEndDate { get; set; }

        public IEnumerable<Customer> CustomersList { get; set; }
        public GenericPagerVM CustomersPager { get; set; } 
    }
}