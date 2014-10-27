using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using LibraryManagementSystem.DataAccess.Entities;

namespace LibraryManagementSystem.ViewModels.Books
{
    public class BooksReturnBookVM
    {
        [DataType(DataType.Date)]
        [DisplayName("Return date")]
        [Required(ErrorMessage = "* return date required")]
        [DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateReturned { get; set; }
        
        [DisplayName("Barcode")]
        [Required(ErrorMessage = "* barcode required")]        
        public int BookBarcodeNumber { get; set; }

        public List<Book> Books { get; set; }
    }
}