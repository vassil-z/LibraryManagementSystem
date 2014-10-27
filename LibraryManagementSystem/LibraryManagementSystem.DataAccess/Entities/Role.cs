using System;
using System.Collections.Generic;
using DataAccess;

namespace LibraryManagementSystem.DataAccess.Entities
{
    public class Role : BaseEntityWithID
    {
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<AuthenticatingAction> AuthenticatingActions { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
