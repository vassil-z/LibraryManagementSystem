using System;
using System.Collections.Generic;
using LibraryManagementSystem.ViewModels.Pager;
using LibraryManagementSystem.DataAccess.Entities;

namespace LibraryManagementSystem.ViewModels.Roles
{
    public class RolesIndexVM
    {
        public List<Role> RolesList { get; set; }
        public GenericPagerVM RolesPager { get; set; }
    }
}