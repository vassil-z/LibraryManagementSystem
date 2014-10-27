using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Filters;
using LibraryManagementSystem.ViewModels.Customers;
using LibraryManagementSystem.ViewModels.Pager;
using LibraryManagementSystem.DataAccess.Entities;
using LibraryManagementSystem.DataAccess.Repositories;
using LibraryManagementSystem.DataAccess.DataAccessLayer;
using DataAccess;

namespace LibraryManagementSystem.Controllers
{
    [AuthenticationFilter]
    public class CustomersController : BaseController
    {
        public ActionResult Index(string sortOrder)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            CustomersRepository customersRepository = new CustomersRepository(context);
            CustomersIndexVM model = new CustomersIndexVM();
            this.TryUpdateModel(model);

            model.CustomersPager = model.CustomersPager ?? new GenericPagerVM();
            model.CustomersPager.CurrentPage = model.CustomersPager.CurrentPage == 0 ? 1 : model.CustomersPager.CurrentPage;
            model.CustomersPager.Action = "Index";
            model.CustomersPager.Controller = "Customers";
            model.CustomersPager.Prefix = "CustomersPager";
            model.CustomersPager.CurrentParameters = new Dictionary<string, object>()
            {
                { "PersonaNumber", model.PersonalNumber },
                { "CustomerName", model.CustomerName },
                { "Email", model.Email },
                { "Address", model.Address },
                { "BirthdayStartDate", model.BirthdayStartDate },
                { "BirthdayEndDate", model.BirthdayEndDate },
                { "DateInStartDate", model.DateInStartDate },
                { "DateInEndDate", model.DateInEndDate },
                { "DateOutStartDate", model.DateOutStartDate },
                { "DateOutEndDate", model.DateOutEndDate },
                { "CustomersPager.CurrentPage", model.CustomersPager.CurrentPage }
            };

            #region Sorting and Filtering
            Expression<Func<Customer, bool>> filter = c =>
                (model.PersonalNumber == default(int) || c.PersonalNumber == model.PersonalNumber) &&
                (string.IsNullOrEmpty(model.CustomerName) || (c.FirstName.Contains(model.CustomerName) || c.LastName.Contains(model.CustomerName))) &&
                (string.IsNullOrEmpty(model.Email) || c.Email.Contains(model.Email)) &&
                (string.IsNullOrEmpty(model.Address) || c.Address.Contains(model.Address)) &&
                (model.BirthdayStartDate == default(DateTime) || (c.Birthday == model.BirthdayStartDate || (c.Birthday >= model.BirthdayStartDate && c.Birthday <= model.BirthdayEndDate))) &&
                (model.DateInStartDate == default(DateTime) || (c.DateIn == model.DateInStartDate || (c.DateIn >= model.DateInStartDate && c.Birthday <= model.DateInEndDate))) &&
                (model.DateOutStartDate == default(DateTime) || (c.DateOut == model.DateOutStartDate || (c.DateOut >= model.DateOutStartDate && c.Birthday <= model.DateOutEndDate)));
            model.CustomersPager.PagesCount = GetPagesCount(filter);

            ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PersonalNumberSortParam = sortOrder == "PersonalNumber" ? "personalNumber_desc" : "PersonalNumber";
            ViewBag.EmailSortParam = sortOrder == "Email" ? "email_desc" : "Email";
            ViewBag.AddressSortParam = sortOrder == "Address" ? "address_desc" : "Address";
            ViewBag.BirthdaySortParam = sortOrder == "Birthday" ? "birthday_desc" : "Birthday";
            ViewBag.DateInSortParam = sortOrder == "DateIn" ? "dateIn_desc" : "DateIn";
            ViewBag.DateOutSortParam = sortOrder == "DateOut" ? "dateOut_desc" : "DateOut";
            switch (sortOrder)
            {
                case "name_desc":
                    model.CustomersList = customersRepository
                        .GetAll(model.CustomersPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderByDescending(c => c.FirstName));
                    break;
                case "PersonalNumber":
                    model.CustomersList = customersRepository
                        .GetAll(model.CustomersPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderBy(c => c.PersonalNumber));
                    break;
                case "personalNumber_desc":
                    model.CustomersList = customersRepository
                        .GetAll(model.CustomersPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderByDescending(c => c.PersonalNumber));
                    break;
                case "Email":
                    model.CustomersList = customersRepository
                        .GetAll(model.CustomersPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderBy(c => c.Email));
                    break;
                case "email_desc":
                    model.CustomersList = customersRepository
                        .GetAll(model.CustomersPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderByDescending(c => c.Email));
                    break;
                case "Address":
                    model.CustomersList = customersRepository
                        .GetAll(model.CustomersPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderBy(c => c.Address));
                    break;
                case "address_desc":
                    model.CustomersList = customersRepository
                        .GetAll(model.CustomersPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderByDescending(c => c.Address));
                    break;
                case "Birthday":
                    model.CustomersList = customersRepository
                        .GetAll(model.CustomersPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderBy(c => c.Birthday));
                    break;
                case "birthday_desc":
                    model.CustomersList = customersRepository
                        .GetAll(model.CustomersPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderByDescending(c => c.Birthday));
                    break;
                case "DateIn":
                    model.CustomersList = customersRepository
                        .GetAll(model.CustomersPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderBy(c => c.DateIn));
                    break;
                case "dateIn_desc":
                    model.CustomersList = customersRepository
                        .GetAll(model.CustomersPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderByDescending(c => c.DateIn));
                    break;
                case "DateOut":
                    model.CustomersList = customersRepository
                        .GetAll(model.CustomersPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderBy(c => c.DateOut));
                    break;
                case "dateOut_desc":
                    model.CustomersList = customersRepository
                        .GetAll(model.CustomersPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderByDescending(c => c.DateOut));
                    break;
                default:
                    model.CustomersList = customersRepository
                       .GetAll(model.CustomersPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderBy(c => c.FirstName));
                    break;
            }
            #endregion

            return View(model);
        }

        public ActionResult EditCustomer(int id)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            CustomersRepository customersRepository = new CustomersRepository(context);
            CustomersEditCustomerVM model = new CustomersEditCustomerVM();

            Customer customer = customersRepository.GetByID(id);
            if (id > 0)
            {
                model.ID = customer.ID;
                model.PersonalNumber = customer.PersonalNumber;
                model.FirstName = customer.FirstName;
                model.LastName = customer.LastName;
                model.Email = customer.Email;
                model.Address = customer.Address;
                model.PicturePath = customer.PicturePath;
                model.Birthday = customer.Birthday;
                model.DateIn = customer.DateIn;
                if (customer.DateOut != null)
                {
                    model.DateOut = customer.DateOut.Value;
                }
            }
            else
            {
                customer = new Customer();
                model.DateOut = null;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        public ActionResult EditCustomer(CustomersEditCustomerVM model)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            CustomersRepository customersRepository = new CustomersRepository(context);

            ModelState.Remove("DateOut");

            if (model.Email != null && customersRepository.GetAll().Any(c => c.Email == model.Email) &&
                model.ID != customersRepository.GetAll(filter: c => c.Email == model.Email).FirstOrDefault().ID)
            {
                ModelState.AddModelError("Email", "* email already exists");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                Customer customer = null;
                if (model.ID > 0)
                {
                    customer = customersRepository.GetByID(model.ID);
                    customer.PersonalNumber = model.PersonalNumber;
                }
                else
                {
                    customer = new Customer();
                    customer.PersonalNumber = customersRepository.GetAll().LastOrDefault().PersonalNumber + 1;
                }

                customer.ID = model.ID;
                customer.FirstName = model.FirstName;
                customer.LastName = model.LastName;
                customer.Email = model.Email;
                customer.Address = model.Address;
                customer.Birthday = model.Birthday;
                customer.DateIn = model.DateIn;
                customer.DateOut = model.DateOut != null ? model.DateOut : null;

                customersRepository.Save(customer);
            }

            return RedirectToAction("Index", "Customers");
        }

        public int GetPagesCount(Expression<Func<Customer, bool>> filter = null)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            CustomersRepository customersRepository = new CustomersRepository(context);

            int pagesCount = 0;
            int pageSize = ApplicationConfiguration.ItemsPerPage;
            int customersCount = 0;
            customersCount = customersRepository.Count(filter);
            pagesCount = customersCount / pageSize;
            if ((customersCount % pageSize) > 0)
            {
                pagesCount++;
            }

            return pagesCount;
        }

        public int GetPagesCount(Expression<Func<Book, bool>> filter = null)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            BooksRepository booksRepository = new BooksRepository(context);

            int pagesCount = 0;
            int pageSize = ApplicationConfiguration.ItemsPerPage;
            int booksCount = 0;
            booksCount = booksRepository.Count(filter);
            pagesCount = booksCount / pageSize;
            if ((booksCount % pageSize) > 0)
            {
                pagesCount++;
            }

            return pagesCount;
        }
    }
}