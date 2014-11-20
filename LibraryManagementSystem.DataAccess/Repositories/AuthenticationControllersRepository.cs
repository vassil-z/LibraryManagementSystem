using System;
using LibraryManagementSystem.DataAccess.Entities;
using LibraryManagementSystem.DataAccess.DataAccessLayer;
using DataAccess;

namespace LibraryManagementSystem.DataAccess.Repositories
{
    public class AuthenticationControllersRepository : BaseRepository<AuthenticationController>
    {
        public AuthenticationControllersRepository(LibraryManagementSystemContext context)
            : base(context)
        {
        }

        public AuthenticationControllersRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
