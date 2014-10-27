using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using LibraryManagementSystem.DataAccess.Entities;
using LibraryManagementSystem.DataAccess.Repositories;
using LibraryManagementSystem.DataAccess.DataAccessLayer;

namespace LibraryManagementSystem.DataAccess.Services
{
    public class AuthenticationService
    {
        public User LoggedUser { get; private set; }

        public bool AuthenticateUser(string email, string password)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            UsersRepository usersRepository = new UsersRepository(context);

            LoggedUser = usersRepository.GetAll(filter: u => u.Email == email && u.Password == password).FirstOrDefault();
            return LoggedUser != null;
        }
    }
}