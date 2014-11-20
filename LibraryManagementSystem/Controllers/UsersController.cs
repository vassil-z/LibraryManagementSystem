using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Filters;
using LibraryManagementSystem.ViewModels.Users;
using LibraryManagementSystem.ViewModels.Pager;
using LibraryManagementSystem.ViewModels.AssignedRoles;
using LibraryManagementSystem.DataAccess.Entities;
using LibraryManagementSystem.DataAccess.Repositories;
using LibraryManagementSystem.DataAccess.DataAccessLayer;
using DataAccess;

namespace LibraryManagementSystem.Controllers
{
    [AuthenticationFilter]
    public class UsersController : BaseController
    {
        public ActionResult Index(string sortOrder)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext(); 
            UsersRepository usersRepository = new UsersRepository(context);
            UsersIndexVM model = new UsersIndexVM();
            this.TryUpdateModel(model);

            model.UsersPager = model.UsersPager ?? new GenericPagerVM();
            model.UsersPager.CurrentPage = model.UsersPager.CurrentPage == 0 ? 1 : model.UsersPager.CurrentPage;                           
            model.UsersPager.Action = "Index";
            model.UsersPager.Controller = "Users";
            model.UsersPager.Prefix = "UsersPager";
            model.UsersPager.CurrentParameters = new Dictionary<string, object>()
            {
                { "PersonaNumber", model.PersonalNumber },
                { "CustomerName", model.UserName },
                { "Email", model.Email },
                { "Address", model.Address },
                { "RoleName", model.RoleName },
                { "BirthdayStartDate", model.BirthdayStartDate },
                { "BirthdayEndDate", model.BirthdayEndDate },
                { "DateInStartDate", model.DateInStartDate },
                { "DateInEndDate", model.DateInEndDate },
                { "DateOutStartDate", model.DateOutStartDate },
                { "DateOutEndDate", model.DateOutEndDate },
                { "UsersPager.CurrentPage", model.UsersPager.CurrentPage }
            };

            #region Soring and Filtering
            Expression<Func<User, bool>> filter = u =>
                   (model.PersonalNumber == default(int) || u.PersonalNumber == model.PersonalNumber) &&
                   (string.IsNullOrEmpty(model.UserName) || (u.FirstName.Contains(model.UserName) || u.LastName.Contains(model.UserName))) &&
                   (string.IsNullOrEmpty(model.Email) || u.Email.Contains(model.Email)) &&
                   (string.IsNullOrEmpty(model.Address) || u.Address.Contains(model.Address)) &&
                   (string.IsNullOrEmpty(model.RoleName) || u.Roles.Any(r => r.Name.Contains(model.RoleName))) &&
                   (model.BirthdayStartDate == default(DateTime) || (u.Birthday == model.BirthdayStartDate || (u.Birthday >= model.BirthdayStartDate && u.Birthday <= model.BirthdayEndDate))) &&
                   (model.DateInStartDate == default(DateTime) || (u.DateIn == model.DateInStartDate || (u.DateIn >= model.DateInStartDate && u.Birthday <= model.DateInEndDate))) &&
                   (model.DateOutStartDate == default(DateTime) || (u.DateOut == model.DateOutStartDate || (u.DateOut >= model.DateOutStartDate && u.Birthday <= model.DateOutEndDate)));
            model.UsersPager.PagesCount = GetPagesCount(filter);

            ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.RolesSortParam = sortOrder == "Roles" ? "roles_desc" : "Roles";
            ViewBag.PersonalNumberSortParam = sortOrder == "PersonalNumber" ? "personalNumber_desc" : "PersonalNumber";
            ViewBag.EmailSortParam = sortOrder == "Email" ? "email_desc" : "Email";
            ViewBag.AddressSortParam = sortOrder == "Address" ? "address_desc" : "Address";
            ViewBag.BirthdaySortParam = sortOrder == "Birthday" ? "birthday_desc" : "Birthday";
            ViewBag.DateInSortParam = sortOrder == "DateIn" ? "dateIn_desc" : "DateIn";
            ViewBag.DateOutSortParam = sortOrder == "DateOut" ? "dateOut_desc" : "DateOut";
            switch (sortOrder)
            {
                case "name_desc":
                    model.UsersList = usersRepository
                        .GetAll(model.UsersPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderByDescending(c => c.FirstName));
                    break;
                case "Roles":
                    model.UsersList = usersRepository
                        .GetAll(model.UsersPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderBy(c => c.Roles.FirstOrDefault().Name));
                    break;
                case "roles_desc":
                    model.UsersList = usersRepository
                        .GetAll(model.UsersPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderByDescending(c => c.Roles.FirstOrDefault().Name));
                    break;
                case "PersonalNumber":
                    model.UsersList = usersRepository
                        .GetAll(model.UsersPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderBy(c => c.PersonalNumber));
                    break;
                case "personalNumber_desc":
                    model.UsersList = usersRepository
                        .GetAll(model.UsersPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderByDescending(c => c.PersonalNumber));
                    break;
                case "Email":
                    model.UsersList = usersRepository
                        .GetAll(model.UsersPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderBy(c => c.Email));
                    break;
                case "email_desc":
                    model.UsersList = usersRepository
                        .GetAll(model.UsersPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderByDescending(c => c.Email));
                    break;
                case "Address":
                    model.UsersList = usersRepository
                        .GetAll(model.UsersPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderBy(c => c.Address));
                    break;
                case "address_desc":
                    model.UsersList = usersRepository
                        .GetAll(model.UsersPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderByDescending(c => c.Address));
                    break;
                case "Birthday":
                    model.UsersList = usersRepository
                        .GetAll(model.UsersPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderBy(c => c.Birthday));
                    break;
                case "birthday_desc":
                    model.UsersList = usersRepository
                        .GetAll(model.UsersPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderByDescending(c => c.Birthday));
                    break;
                case "DateIn":
                    model.UsersList = usersRepository
                        .GetAll(model.UsersPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderBy(c => c.DateIn));
                    break;
                case "dateIn_desc":
                    model.UsersList = usersRepository
                        .GetAll(model.UsersPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderByDescending(c => c.DateIn));
                    break;
                case "DateOut":
                    model.UsersList = usersRepository
                        .GetAll(model.UsersPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderBy(c => c.DateOut));
                    break;
                case "dateOut_desc":
                    model.UsersList = usersRepository
                        .GetAll(model.UsersPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderByDescending(c => c.DateOut));
                    break;
                default:
                    model.UsersList = usersRepository
                       .GetAll(model.UsersPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderBy(c => c.FirstName));
                    break;
            } 
            #endregion

            return View(model);
        }

        public ActionResult EditUser(int id)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            UsersRepository usersRepository = new UsersRepository(context);
            RolesRepository rolesRepository = new RolesRepository(context);
            UsersEditUserVM model = new UsersEditUserVM();

            if (id > 0)
            {
                User user = usersRepository.GetAll(filter: u => u.ID == id,
                    includeProperties: "Roles").FirstOrDefault();                

                PopulateAssignedRoles(user, rolesRepository);
                
                model.ID = user.ID;
                model.PersonalNumber = user.PersonalNumber;                
                model.Password = user.Password;
                model.FirstName = user.FirstName;
                model.Email = user.Email;
                model.Address = user.Address;
                model.LastName = user.LastName;
                model.Birthday = user.Birthday;
                model.DateIn = user.DateIn;
                if (user.DateOut != null)
                {
                    model.DateOut = user.DateOut.Value;
                }
            }
            else
            {
                model.Email = " ";                

                User user = new User();
                user.Roles = new List<Role>();
                PopulateAssignedRoles(user, rolesRepository);
            }
            

            return View(model);        
        }

        [HttpPost]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(UsersEditUserVM model, string[] assignedRoles)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();

            ModelState.Remove("DateOut");

            UsersRepository usersRepositoryContext = new UsersRepository(context);
            if (model.Email != null && usersRepositoryContext.GetAll().Any(u => u.Email == model.Email) &&
                model.ID != usersRepositoryContext.GetAll(filter: u => u.Email == model.Email).FirstOrDefault().ID)
            {                              
                ModelState.AddModelError("Email", "* email already exists");                                
            }
            if (model.ID <= 0 && string.IsNullOrEmpty(model.Password))
            {
                this.ModelState.AddModelError("Password", "* password required");
            }
            if (!this.ModelState.IsValid)
            {
                RolesRepository rolesRepository = new RolesRepository(context);
                var allRoles = rolesRepository.GetAll();

                List<AssignedRolesVM> assignedRolesViewModel = new List<AssignedRolesVM>();
                foreach (var role in allRoles)
                {
                    assignedRolesViewModel.Add(new AssignedRolesVM
                        {
                            ID = role.ID,
                            Name = role.Name,
                            IsAssigned = false
                        });
                }

                ViewBag.Roles = assignedRolesViewModel;
                return View(model);
            }

            TryUpdateModel(model);

            User user = null;            
            using (UnitOfWork unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    UsersRepository usersRepositoryUnitOfWork = new UsersRepository(unitOfWork);
                    RolesRepository rolesRepository = new RolesRepository(unitOfWork);

                    if (model.ID > 0)
                    {
                        user = usersRepositoryUnitOfWork.GetAll(filter: u => u.ID == model.ID, includeProperties: "Roles").FirstOrDefault();
                        user.PersonalNumber = model.PersonalNumber;
                    }
                    else
                    {
                        user = new User();
                        user.Roles = new List<Role>();
                        user.PersonalNumber = usersRepositoryUnitOfWork.GetAll().LastOrDefault().PersonalNumber + 1;
                    }
                                        
                    user.Password =
                        (model.Password != null) && (model.Password.Trim() != String.Empty) ? model.Password.Trim() : user.Password;
                    user.FirstName = model.FirstName;
                    user.Email = model.Email;
                    user.Address = model.Address;
                    user.LastName = model.LastName;
                    user.Birthday = model.Birthday;
                    user.DateIn = model.DateIn;
                    user.DateOut = model.DateOut != null ? model.DateOut : null;

                    UpdateUserRoles(assignedRoles, user, rolesRepository);
                    usersRepositoryUnitOfWork.Save(user);

                    PopulateAssignedRoles(user, rolesRepository);
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.RollBack();
                    throw ex;
                }
            }

            return RedirectToAction("Index", "Users");
        }

        public ActionResult DeleteUser(int id)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            UsersRepository usersRepository = new UsersRepository(context);
            UsersDeleteUserVM model = new UsersDeleteUserVM();

            User user = usersRepository.GetByID(id);

            model.ID = user.ID;
            model.FullName = user.ToString();

            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteUser(UsersDeleteUserVM model)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            UsersRepository usersRepository = new UsersRepository(context);                        

            User user = usersRepository.GetAll(filter: u => u.ID == model.ID, includeProperties: "Roles").FirstOrDefault();
            if (user == null)
            {
                return HttpNotFound();
            }

            user.Roles = null;
            usersRepository.Delete(user);

            return RedirectToAction("Index", "Users");
        }

        private void PopulateAssignedRoles(User user, RolesRepository rolesRepository)
        {
            var allRoles = rolesRepository.GetAll();
            HashSet<int> userRoles = new HashSet<int>(user.Roles.Select(r => r.ID));
            List<AssignedRolesVM> model = new List<AssignedRolesVM>();
            foreach (var role in allRoles)
            {
                model.Add(new AssignedRolesVM
                    {
                        ID = role.ID,
                        Name = role.Name,
                        IsAssigned = userRoles.Contains(role.ID)
                    });
            }

            ViewBag.Roles = model;
        }

        private void UpdateUserRoles(string[] assignedRoles, User user, RolesRepository rolesRepository)
        {
            if (assignedRoles == null)
            {
                return;
            }

            var assignedRolesHS = new HashSet<string>(assignedRoles);
            var userRoles = new HashSet<int>(user.Roles.Select(u => u.ID));

            foreach (var role in rolesRepository.GetAll())
            {
                if (assignedRolesHS.Contains(role.ID.ToString()))
                {
                    if (!userRoles.Contains(role.ID))
                    {
                        user.Roles.Add(role);
                    }
                }
                else if (userRoles.Contains(role.ID))
                {
                    user.Roles.Remove(role);
                }               
            }
        }

        public int GetPagesCount(Expression<Func<User, bool>> filter = null)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            UsersRepository usersRepository = new UsersRepository(context);

            int pagesCount = 0;
            int pageSize = ApplicationConfiguration.ItemsPerPage;            
            int usersCount = 0;
            usersCount = usersRepository.Count(filter);
            pagesCount = usersCount / pageSize;
            if ((usersCount % pageSize) > 0)
            {
                pagesCount++;
            }

            return pagesCount;
        }
    }
}