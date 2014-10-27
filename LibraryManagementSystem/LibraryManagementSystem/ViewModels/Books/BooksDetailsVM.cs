using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LibraryManagementSystem.ViewModels.Pager;
using LibraryManagementSystem.DataAccess.Entities;

namespace LibraryManagementSystem.ViewModels.Books
{
    public class BooksDetailsVM
    {
        public int ID { get; set; }
        public int PublisherID { get; set; }
        public string Title { get; set; }        
        public int StockCount { get; set; }
        public double DeliveryPrice { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}", ApplyFormatInEditMode = true)]             
        public DateTime DateReceived { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DatePublished { get; set; }

        public virtual Publisher Publisher { get; set; }

        public List<Author> Authors { get; set; }
        public GenericPagerVM AuthorsPager { get; set; }
    }
}