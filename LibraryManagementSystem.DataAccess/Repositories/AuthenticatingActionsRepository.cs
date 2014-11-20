using System;
using LibraryManagementSystem.DataAccess.Entities;
using LibraryManagementSystem.DataAccess.DataAccessLayer;
using DataAccess;

namespace LibraryManagementSystem.DataAccess.Repositories
{
    public class AuthenticatingActionsRepository : BaseRepository<AuthenticatingAction>
    {
         public AuthenticatingActionsRepository(LibraryManagementSystemContext context)
            : base(context)
        {
        }

         public AuthenticatingActionsRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
