using System;
using LibraryManagementSystem.DataAccess.Entities;
using LibraryManagementSystem.DataAccess.DataAccessLayer;
using DataAccess;

namespace LibraryManagementSystem.DataAccess.Repositories
{
    public class RolesRepository : BaseRepository<Role>
    {
        public RolesRepository(LibraryManagementSystemContext context)
            : base(context)
        {
        }

        public RolesRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public bool Exists(int roleID, string controllerName, string actionName)
        {
            controllerName += "Controller";
            bool result = false;

            var roles = this.GetAll(filter: r => r.ID == roleID, includeProperties: "AuthenticatingActions");

            foreach (var role in roles)
            {
                var rolesList = role.AuthenticatingActions;
                foreach (var item in rolesList)
                {
                    if (item.AuthenticationController.Name == controllerName && item.Name == actionName)
                    {
                        result = true;
                    }
                }
            }

            return result;
        }
    }
}
