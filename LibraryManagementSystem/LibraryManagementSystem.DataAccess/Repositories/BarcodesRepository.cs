using System;
using LibraryManagementSystem.DataAccess.Entities;
using LibraryManagementSystem.DataAccess.DataAccessLayer;
using DataAccess;
namespace LibraryManagementSystem.DataAccess.Repositories
{
    public class BarcodesRepository : BaseRepository<Barcode>
    {
        public BarcodesRepository(LibraryManagementSystemContext context)
            : base(context)
        {
        }
    }
}
