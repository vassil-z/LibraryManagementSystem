using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LibraryManagementSystem.ViewModels.Pager;
using LibraryManagementSystem.DataAccess.Entities;

namespace LibraryManagementSystem.ViewModels.Users
{
    public class UsersIndexVM
    {
        public int PersonalNumber { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string RoleName { get; set; }

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

        public IEnumerable<User> UsersList { get; set; }
        public GenericPagerVM UsersPager { get; set; }        
    }
}