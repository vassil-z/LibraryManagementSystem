using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using LibraryManagementSystem.DataAccess.Entities;

namespace LibraryManagementSystem.ViewModels.Authors
{
    public class AuthorsEditAuthorVM
    {
        public int ID { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "* first name required")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "* last name required")]
        public string LastName { get; set; }
    }
}