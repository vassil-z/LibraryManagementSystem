using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LibraryManagementSystem.DataAccess.Entities;

namespace LibraryManagementSystem.ViewModels.Books
{
    public class BooksEditBookVM
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "* title required")]
        public string Title { get; set; }       

        [DisplayName("Publisher")]
        [Required(ErrorMessage = "* publisher required")]
        public int PublisherID { get; set; }

        [DisplayName("Stock count")]
        [Required(ErrorMessage = "* stock count required")]
        public int StockCount { get; set; }
        
        [DisplayName("Delivery price")]
        [Required(ErrorMessage = "* delivery price required")]        
        public double DeliveryPrice { get; set; }
        
        [DisplayName("Date received")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "* date received required")]
        [DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateReceived { get; set; }
        
        [DisplayName("Date published")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "* date published required")]
        [DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DatePublished { get; set; }

        public List<Barcode> Barcodes { get; set; }
        public List<SelectListItem> Publishers { get; set; }
    }
}