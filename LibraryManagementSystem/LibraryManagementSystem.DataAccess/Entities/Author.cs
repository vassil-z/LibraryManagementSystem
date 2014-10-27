using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.DataAccess.Entities
{
    public class Author : Person
    {
        public virtual ICollection<Book> Books { get; set; }        
    }
}
