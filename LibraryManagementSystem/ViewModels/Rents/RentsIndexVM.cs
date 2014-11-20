using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LibraryManagementSystem.ViewModels.Pager;
using LibraryManagementSystem.DataAccess.Entities;

namespace LibraryManagementSystem.ViewModels.Rents
{
    public class RentsIndexVM
    {
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public string BookTitle { get; set; }

        public string UserName { get; set; }                
        public string CustomerName { get; set; }

        public List<Rent> RentsList { get; set; }
        public GenericPagerVM RentsPager { get; set; }
    }
}