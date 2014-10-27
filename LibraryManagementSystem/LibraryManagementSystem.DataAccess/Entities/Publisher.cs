using System;
using System.Collections.Generic;
using DataAccess;

namespace LibraryManagementSystem.DataAccess.Entities
{
    public class Publisher : BaseEntityWithID
    {
        public string Name { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
