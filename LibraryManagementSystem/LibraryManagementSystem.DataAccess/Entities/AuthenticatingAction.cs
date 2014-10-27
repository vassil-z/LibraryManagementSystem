using System;
using System.Collections.Generic;
using DataAccess;

namespace LibraryManagementSystem.DataAccess.Entities
{
    public class AuthenticatingAction : BaseEntityWithID
    {
        public string Name { get; set; }
        public int AuthenticationControllerID { get; set; }        
        
        public virtual AuthenticationController AuthenticationController { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}
