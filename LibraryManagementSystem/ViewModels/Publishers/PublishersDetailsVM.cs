using System;
using System.Collections.Generic;
using LibraryManagementSystem.ViewModels.Pager;
using LibraryManagementSystem.DataAccess.Entities;

namespace LibraryManagementSystem.ViewModels.Publishers
{
    public class PublishersDetailsVM
    {
        public int ID { get; set; }
        public string PublisherName { get; set; }
        public string Address { get; set; }
        public string BookTitle { get; set; }        

        public List<Book> Books { get; set; }
        public GenericPagerVM BooksPager { get; set; }
    }
}