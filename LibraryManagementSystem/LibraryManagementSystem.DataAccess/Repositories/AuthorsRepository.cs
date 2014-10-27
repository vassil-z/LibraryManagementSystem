using System;
using LibraryManagementSystem.DataAccess.Entities;
using LibraryManagementSystem.DataAccess.DataAccessLayer;
using DataAccess;

namespace LibraryManagementSystem.DataAccess.Repositories
{
    public class AuthorsRepository : BaseRepository<Author>
    {
        public AuthorsRepository(LibraryManagementSystemContext context)
            : base(context)
        {
        }
    }
}
