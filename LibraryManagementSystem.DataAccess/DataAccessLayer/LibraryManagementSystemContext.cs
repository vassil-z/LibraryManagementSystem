using System;
using System.Data.Entity;
using LibraryManagementSystem.DataAccess.Entities;

namespace LibraryManagementSystem.DataAccess.DataAccessLayer
{
    public class LibraryManagementSystemContext : DbContext
    {
        public LibraryManagementSystemContext()
            : base("LibraryManagementSystem")
        {
        }

        DbSet<AuthenticatingAction> AuthenticatingActions { get; set; }

        DbSet<AuthenticationController> AuthenticationControllers { get; set; }

        DbSet<Author> Authors { get; set; }

        DbSet<Barcode> Barcodes { get; set; }

        DbSet<Book> Books { get; set; }

        DbSet<Publisher> Publishers { get; set; }

        DbSet<Rent> Rents { get; set; }

        DbSet<Role> Roles { get; set; }

        DbSet<User> Users { get; set; }

        DbSet<Customer> Customers { get; set; }        
    }
}
