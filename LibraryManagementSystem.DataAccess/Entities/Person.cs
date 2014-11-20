using System;
using System.Collections.Generic;
using DataAccess;

namespace LibraryManagementSystem.DataAccess.Entities
{
    public class Person : BaseEntityWithID
    {        
        public string FirstName { get; set; }        
        public string LastName { get; set; }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
