using System;
using System.Collections.Generic;
using LibraryManagementSystem.ViewModels.Pager;
using LibraryManagementSystem.DataAccess.Entities;

namespace LibraryManagementSystem.ViewModels.Publishers
{
    public class PublishersIndexVM
    {
        public string PublisherName { get; set; }
        public string PublisherAddress { get; set; }

        public IEnumerable<Publisher> PublishersList { get; set; }
        public GenericPagerVM PublishersPager { get; set; }
    }
}