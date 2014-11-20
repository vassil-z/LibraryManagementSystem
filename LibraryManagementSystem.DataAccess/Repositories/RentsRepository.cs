using System;
using LibraryManagementSystem.DataAccess.Entities;
using LibraryManagementSystem.DataAccess.DataAccessLayer;
using DataAccess;

namespace LibraryManagementSystem.DataAccess.Repositories
{
    public class RentsRepository : BaseRepository<Rent>
    {
        public RentsRepository(LibraryManagementSystemContext context)
            : base(context)
        {
        }
    }
}
