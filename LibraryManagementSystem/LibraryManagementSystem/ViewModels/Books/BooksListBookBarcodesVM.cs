using System;
using System.Collections.Generic;
using LibraryManagementSystem.ViewModels.Pager;
using LibraryManagementSystem.DataAccess.Entities;

namespace LibraryManagementSystem.ViewModels.Books
{
    public class BooksListBookBarcodesVM
    {        
        public int BookID { get; set; }
        public string BookTitle { get; set; }
        public List<Barcode> Barcodes { get; set; }
        public GenericPagerVM BarcodesPager { get; set; }
    }
}