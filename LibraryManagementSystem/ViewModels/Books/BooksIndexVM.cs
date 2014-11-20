using System;
using System.Collections.Generic;
using LibraryManagementSystem.ViewModels.Pager;
using LibraryManagementSystem.DataAccess.Entities;

namespace LibraryManagementSystem.ViewModels.Books
{
    public class BooksIndexVM
    {
        public string Title { get; set; }
        public string Publisher { get; set; }

        public List<Book> BooksList { get; set; }
        public GenericPagerVM BooksPager { get; set; }
    }
}