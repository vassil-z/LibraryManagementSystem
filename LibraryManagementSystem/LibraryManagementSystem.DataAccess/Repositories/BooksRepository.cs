using System;
using LibraryManagementSystem.DataAccess.Entities;
using LibraryManagementSystem.DataAccess.DataAccessLayer;
using DataAccess;


namespace LibraryManagementSystem.DataAccess.Repositories
{
    public class BooksRepository : BaseRepository<Book>
    {
        public BooksRepository(LibraryManagementSystemContext context)
            : base(context)
        {
        }
    }
}
