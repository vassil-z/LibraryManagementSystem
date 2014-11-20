using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.ViewModels.Books
{
    public class BooksAddBookAuthorVM
    {
        public int ID { get; set; }

        [DisplayName("Select author")]
        public int AuthorID { get; set; }

        [DisplayName("Title")]
        public string Title { get; set; }

        public List<SelectListItem> Authors { get; set; }
    }
}