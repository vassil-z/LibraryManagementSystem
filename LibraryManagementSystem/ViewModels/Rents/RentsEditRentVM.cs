using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using LibraryManagementSystem.DataAccess.Entities;

namespace LibraryManagementSystem.ViewModels.Rents
{
    public class RentsEditRentVM
    {
        public int ID { get; set; }

        [DisplayName("Rent date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "* rent date required")]
        [DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RentDate { get; set; }
        
        [Required(ErrorMessage = "* barcode required")]       
        public int BookBarcodeNumber { get; set; }
 
        public int UserID { get; set; }
        
        [Required(ErrorMessage = "* personal No. required")]               
        public int CustomerPersonalNumber { get; set; }
        
        public Customer Customer { get; set; }
                      
        public List<Customer> Customers { get; set; }
        public List<Book> Books { get; set; }        
    }
}