using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.DataAccess.Entities
{
    public class RentParticipant : Person
    {        
        public int PersonalNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PicturePath { get; set; }

        [Column(TypeName = "datetime2")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }

        [Column(TypeName = "datetime2")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateIn { get; set; }

        [Column(TypeName = "datetime2")]
        [DataType(DataType.Date)]        
        [DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOut { get; set; }

        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<Rent> Rents { get; set; }
    }
}
