using System;
using LibraryManagementSystem.DataAccess.Entities;
using LibraryManagementSystem.DataAccess.DataAccessLayer;
using DataAccess;

namespace LibraryManagementSystem.DataAccess.Repositories
{
    public class PublishersRepository : BaseRepository<Publisher>
    {
        public PublishersRepository(LibraryManagementSystemContext context)
            : base(context)
        {
        }
    }
}
