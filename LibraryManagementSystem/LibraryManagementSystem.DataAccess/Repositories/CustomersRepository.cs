using System;
using LibraryManagementSystem.DataAccess.Entities;
using LibraryManagementSystem.DataAccess.DataAccessLayer;
using DataAccess;

namespace LibraryManagementSystem.DataAccess.Repositories
{
    public class CustomersRepository : BaseRepository<Customer>
    {
        public CustomersRepository(LibraryManagementSystemContext context)
            :base(context)
        {
        }
    }
}
