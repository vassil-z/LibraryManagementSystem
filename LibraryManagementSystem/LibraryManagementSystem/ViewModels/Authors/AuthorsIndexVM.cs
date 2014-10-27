using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using LibraryManagementSystem.ViewModels.Pager;
using LibraryManagementSystem.DataAccess.Entities;

namespace LibraryManagementSystem.ViewModels.Authors
{
    public class AuthorsIndexVM
    {
        [DisplayName("Name")]
        public string AuthorName { get; set; }

        public IEnumerable<Author> AuthorsList { get; set; }
        public GenericPagerVM AuthorsPager { get; set; }        
    }
}