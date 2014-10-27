using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess;

namespace LibraryManagementSystem.DataAccess.Entities
{
    public class Rent : BaseEntityWithID
    {
        [Column(TypeName = "datetime2")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RentDate { get; set; } 
              
        public int UserID { get; set; }        
        public int CustomerID { get; set; }
        public DateTime DateToReturn { get; set; }

        public virtual User User { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
