using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Filters;
using LibraryManagementSystem.ViewModels.Rents;
using LibraryManagementSystem.ViewModels.Pager;
using LibraryManagementSystem.DataAccess.Entities;
using LibraryManagementSystem.DataAccess.Repositories;
using LibraryManagementSystem.DataAccess.DataAccessLayer;
using DataAccess;
namespace LibraryManagementSystem.Controllers
{
    [AuthenticationFilter]
    public class RentsController : BaseController
    {
        public ActionResult Index(string sortOrder)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            RentsRepository rentsRepository = new RentsRepository(context);
            RentsIndexVM model = new RentsIndexVM();
            this.TryUpdateModel(model);

            model.RentsPager = model.RentsPager ?? new GenericPagerVM();
            model.RentsPager.CurrentPage = model.RentsPager.CurrentPage == 0 ? 1 : model.RentsPager.CurrentPage;
            model.RentsPager.Action = "Index";
            model.RentsPager.Controller = "Authors";
            model.RentsPager.Prefix = "RentsPager";
            model.RentsPager.CurrentParameters = new Dictionary<string, object>()
            {
                { "StartDate", model.StartDate },
                { "EndDate", model.EndDate },
                { "UserName", model.UserName },                
                { "CustomerFirstName", model.CustomerName },                
                { "RentsPager.CurrentPage", model.RentsPager.CurrentPage }
            };

            #region Sorting and Filtering
            Expression<Func<Rent, bool>> filter = r =>
                    (model.StartDate == default(DateTime) || (r.RentDate == model.StartDate || (r.RentDate >= model.StartDate && r.RentDate <= model.EndDate))) &&
                    (string.IsNullOrEmpty(model.BookTitle) || r.Books.Any(b => b.Title.Contains(model.BookTitle))) &&
                    (string.IsNullOrEmpty(model.UserName) || (r.User.FirstName.Contains(model.UserName) || r.User.LastName.Contains(model.UserName))) &&
                    (string.IsNullOrEmpty(model.CustomerName) || (r.Customer.FirstName.Contains(model.CustomerName) || r.Customer.LastName.Contains(model.CustomerName)));
            model.RentsPager.PagesCount = GetPagesCount(filter);

            ViewBag.RentDateSortParam = string.IsNullOrEmpty(sortOrder) ? "RentDate" : "";
            ViewBag.DateToReturnSortParam = sortOrder == "DateToReturn" ? "dateToReturn_desc" : "DateToReturn";
            ViewBag.BookSortParam = sortOrder == "Book" ? "book_desc" : "Book";
            ViewBag.UserSortParam = sortOrder == "User" ? "user_desc" : "User";
            ViewBag.CustomerSortParam = sortOrder == "Customer" ? "customer_desc" : "Customer";
            switch (sortOrder)
            {
                case "RentDate":
                    model.RentsList = rentsRepository
                        .GetAll(model.RentsPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, x => x.OrderBy(r => r.RentDate))
                        .ToList();
                    break;
                case "dateToReturn_desc":
                    model.RentsList = rentsRepository
                        .GetAll(model.RentsPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, x => x.OrderByDescending(r => r.DateToReturn))
                        .ToList();
                    break;
                case "DateToReturn":
                    model.RentsList = rentsRepository
                        .GetAll(model.RentsPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, x => x.OrderBy(r => r.DateToReturn))
                        .ToList();
                    break;
                case "book_desc":
                    model.RentsList = rentsRepository
                        .GetAll(model.RentsPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, x => x.OrderByDescending(r => r.Books.FirstOrDefault().Title))
                        .ToList();
                    break;
                case "Book":
                    model.RentsList = rentsRepository
                        .GetAll(model.RentsPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, x => x.OrderBy(r => r.Books.FirstOrDefault().Title))
                        .ToList();
                    break;
                case "user_desc":
                    model.RentsList = rentsRepository
                        .GetAll(model.RentsPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, x => x.OrderByDescending(r => r.User.FirstName))
                        .ToList();
                    break;
                case "User":
                    model.RentsList = rentsRepository
                        .GetAll(model.RentsPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, x => x.OrderBy(r => r.User.FirstName))
                        .ToList();
                    break;
                case "customer_desc":
                    model.RentsList = rentsRepository
                        .GetAll(model.RentsPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, x => x.OrderByDescending(r => r.Customer.FirstName))
                        .ToList();
                    break;
                case "Customer":
                    model.RentsList = rentsRepository
                        .GetAll(model.RentsPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, x => x.OrderBy(r => r.Customer.FirstName))
                        .ToList();
                    break;
                default:
                    model.RentsList = rentsRepository
                        .GetAll(model.RentsPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, x => x.OrderByDescending(r => r.RentDate))
                        .ToList();
                    break;
            } 
            #endregion

            return View(model);
        }

        public ActionResult EditRent()
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            BooksRepository booksRepository = new BooksRepository(context);
            CustomersRepository customersRepository = new CustomersRepository(context);
            RentsEditRentVM model = new RentsEditRentVM();

            model.Customers = customersRepository.GetAll();
            model.Books = booksRepository.GetAll();                       
            model.RentDate = DateTime.Now.Date;
            model.UserID = AuthenticationManager.LoggedUser.ID;

            return View(model);
        }

        [HttpPost]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        public ActionResult EditRent(string customerInfo, string bookInfo, RentsEditRentVM model)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            RentsRepository rentsRepository = new RentsRepository(context);
            BooksRepository booksRepository = new BooksRepository(context);
            CustomersRepository customersRepository = new CustomersRepository(context);

            // makes an customer info array in format {Personal No.}{Empty}{Empty}{First name}{Last name} 
            string[] customerInfoSplitted = customerInfo.Split(' ', '-');

            // makes an book info array in format {Barcode No.}{Empty}{Empty}{Book title}
            string[] bookInfoSplitted = bookInfo.Split(' ', '-');

            if (string.IsNullOrEmpty(customerInfo) || customerInfoSplitted[0] == "")
            {               
                ModelState.AddModelError("CustomerPersonalNumber", "* personal No. required");                
            }
            if (string.IsNullOrEmpty(bookInfo) || bookInfoSplitted[0] == "")
            {
                ModelState.AddModelError("BookBarcodeNumber", "* barcode required");
            }
            if (!ModelState.IsValid)
            {
                if (model.ID <= 0)
                {
                    model.RentDate = DateTime.Now;
                }

                model.Customers = customersRepository.GetAll();
                model.Books = booksRepository.GetAll();

                return View(model);
            }
           
            model.CustomerPersonalNumber = int.Parse(customerInfoSplitted[0]);
            model.Customer = customersRepository
                .GetAll(filter: c => c.PersonalNumber == model.CustomerPersonalNumber)
                .FirstOrDefault();
            
            model.BookBarcodeNumber = int.Parse(bookInfoSplitted[0]);
            model.Books = booksRepository
                .GetAll(filter: b => b.Barcodes.Any(bc => bc.BarcodeNumber == model.BookBarcodeNumber));                

            if (model.Books.Any(b => b.StockCount > 0))
            {
                Rent rent = new Rent();
                rent.Books = new List<Book>();
                rent.Books = model.Books;
                rent.Books.FirstOrDefault().StockCount--;
                rent.DateToReturn = DateTime.Now.AddMonths(1);
                rent.RentDate = model.RentDate;
                rent.UserID = model.UserID;
                rent.CustomerID = model.Customer.ID;

                rentsRepository.Save(rent);
            }
            else
            {
                ModelState.AddModelError("BookBarcodeNumber", "* book not in stock at the moment");

                if (model.ID <= 0)
                {
                    model.RentDate = DateTime.Now;
                }

                model.Customers = customersRepository.GetAll();
                model.Books = booksRepository.GetAll();

                return View(model);
            }

            return RedirectToAction("Index", "Rents");
        }       

        public int GetPagesCount(Expression<Func<Rent, bool>> filter)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            RentsRepository rentsRepository = new RentsRepository(context);

            int pagesCount = 0;
            int pageSize = ApplicationConfiguration.ItemsPerPage;
            int rentsCount = 0;
            rentsCount = rentsRepository.Count(filter);
            pagesCount = rentsCount / pageSize;
            if ((rentsCount % pageSize) > 0)
            {
                pagesCount++;
            }

            return pagesCount;
        }
    }
}