using System;
using LibraryManagementSystem.DataAccess.Entities;
using LibraryManagementSystem.DataAccess.DataAccessLayer;
using DataAccess;

namespace LibraryManagementSystem.DataAccess.Repositories
{
    public class UsersRepository : BaseRepository<User>
    {
        public UsersRepository(LibraryManagementSystemContext context)
            : base(context)
        {
        }       

        public UsersRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
