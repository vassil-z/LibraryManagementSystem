using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Filters;
using LibraryManagementSystem.ViewModels.Publishers;
using LibraryManagementSystem.ViewModels.Pager;
using LibraryManagementSystem.DataAccess.Entities;
using LibraryManagementSystem.DataAccess.Repositories;
using LibraryManagementSystem.DataAccess.DataAccessLayer;
using DataAccess;

namespace LibraryManagementSystem.Controllers
{
    [AuthenticationFilter]
    public class PublishersController : BaseController
    {
        public ActionResult Index(string sortOrder)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            PublishersRepository publishersRepository = new PublishersRepository(context);
            PublishersIndexVM model = new PublishersIndexVM();
            this.TryUpdateModel(model);

            model.PublishersPager = model.PublishersPager ?? new GenericPagerVM();
            model.PublishersPager.CurrentPage = model.PublishersPager.CurrentPage == 0 ? 1 : model.PublishersPager.CurrentPage;            
            model.PublishersPager.Action = "Index";
            model.PublishersPager.Controller = "Publishers";
            model.PublishersPager.Prefix = "PublishersPager";
            model.PublishersPager.CurrentParameters = new Dictionary<string, object>()
            {
                { "PublisherName", model.PublisherName },
                { "PublisherAddress", model.PublisherAddress },
                { "PublishersPager.CurrentPage", model.PublishersPager.CurrentPage }
            };

            #region Sorting and Filtering
            Expression<Func<Publisher, bool>> filter = p =>
                    (string.IsNullOrEmpty(model.PublisherName) || p.Name.Contains(model.PublisherName)) &&
                    (string.IsNullOrEmpty(model.PublisherAddress) || p.Address.Contains(model.PublisherAddress));
            model.PublishersPager.PagesCount = GetPagesCount(filter);

            ViewBag.PublisherSortParam = string.IsNullOrEmpty(sortOrder) ? "publisher_desc" : "";
            ViewBag.AddressSortParam = sortOrder == "Address" ? "address_desc" : "Address";
            switch (sortOrder)
            {
                case "publisher_desc":
                    model.PublishersList = publishersRepository
                        .GetAll(model.PublishersPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, x => x.OrderByDescending(p => p.Name))
                        .ToList();
                    break;
                case "Address":
                    model.PublishersList = publishersRepository
                        .GetAll(model.PublishersPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, x => x.OrderBy(p => p.Address))
                        .ToList();
                    break;
                case "address_desc":
                    model.PublishersList = publishersRepository
                        .GetAll(model.PublishersPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, x => x.OrderByDescending(p => p.Address))
                        .ToList();
                    break;
                default:
                    model.PublishersList = publishersRepository
                       .GetAll(model.PublishersPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, x => x.OrderBy(p => p.Name))
                       .ToList();
                    break;
            } 
            #endregion

            return View(model);
        }

        public ActionResult EditPublisher(int id)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            PublishersRepository publishersRepository = new PublishersRepository(context);
            PublishersEditPublisherVM model = new PublishersEditPublisherVM();

            Publisher publisher = publishersRepository.GetByID(id);
            if (id > 0)
            {
                if (publisher == null)
                {
                    return RedirectToAction("Index", "Publishers");
                }

                model.ID = publisher.ID;
                model.Name = publisher.Name;
                model.Address = publisher.Address;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        public ActionResult EditPublisher(PublishersEditPublisherVM model)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            PublishersRepository publishersRepository = new PublishersRepository(context);

            Publisher publisher = null;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                if (model.ID > 0)
                {
                    publisher = publishersRepository.GetByID(model.ID);
                }
                else
                {
                    publisher = new Publisher();
                }

                publisher.ID = model.ID;
                publisher.Name = model.Name;
                publisher.Address = model.Address;

                publishersRepository.Save(publisher);
            }

            return RedirectToAction("Index", "Publishers");
        }

        public ActionResult Details(int id, string sortOrder)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            PublishersRepository publishersRepository = new PublishersRepository(context);
            BooksRepository booksRepository = new BooksRepository(context);
            PublishersDetailsVM model = new PublishersDetailsVM();
            this.TryUpdateModel(model);

            var publisher = publishersRepository.GetByID(id);
            if (publisher != null)
            {
                model.ID = publisher.ID;
                model.PublisherName = publisher.Name;
                model.Address = publisher.Address;
                model.BooksPager = model.BooksPager ?? new GenericPagerVM();
                model.BooksPager.CurrentPage = model.BooksPager.CurrentPage == 0 ? 1 : model.BooksPager.CurrentPage;                                
                model.BooksPager.Action = "Details";
                model.BooksPager.Controller = "Publishers";
                model.BooksPager.Prefix = "BooksPager";
                model.BooksPager.CurrentParameters = new Dictionary<string, object>()
                {                    
                    { "BookTitle", model.BookTitle },                    
                    { "BooksPager.CurrentPage", model.BooksPager.CurrentPage }
                };

                #region Sorting and Filtering               

                Expression<Func<Book, bool>> filter = b =>
                            (string.IsNullOrEmpty(model.BookTitle) || b.Title.Contains(model.BookTitle));
                model.BooksPager.PagesCount = GetPagesCount(filter);

                ViewBag.BookSortParam = string.IsNullOrEmpty(sortOrder) ? "book_desc" : "";
                switch (sortOrder)
                {
                    case "book_desc":
                        model.Books = booksRepository
                            .GetAll(model.BooksPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, x => x.OrderByDescending(b => b.Title))
                            .Where(b => b.PublisherID == id)
                            .ToList();
                        break;
                    default:
                        model.Books = booksRepository
                           .GetAll(model.BooksPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, x => x.OrderBy(b => b.Title))
                           .Where(b => b.PublisherID == id)
                           .ToList();
                        break;
                }                
                #endregion

                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Publishers");
            }
        }

        public int GetPagesCount(Expression<Func<Publisher, bool>> filter = null)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            PublishersRepository publishersRepository = new PublishersRepository(context);

            int pagesCount = 0;
            int pageSize = ApplicationConfiguration.ItemsPerPage;
            int publishersCount = 0;
            publishersCount = publishersRepository.Count(filter);
            pagesCount = publishersCount / pageSize;
            if ((publishersCount % pageSize) > 0)
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