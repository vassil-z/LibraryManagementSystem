using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess;

namespace LibraryManagementSystem.DataAccess.Entities
{
    public class Book : BaseEntityWithID
    {
        public string Title { get; set; }                     
        public int PublisherID { get; set; }

        public int StockCount { get; set; }
        public double DeliveryPrice { get; set; }        
        public string PicturePath { get; set; }

        [Column(TypeName = "datetime2")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateReceived { get; set; }

        [Column(TypeName = "datetime2")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DatePublished { get; set; }
                
        public virtual Publisher Publisher { get; set; }

        public virtual ICollection<Rent> Rents { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Author> Authors { get; set; }                
        public virtual ICollection<Barcode> Barcodes { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
