using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Filters;
using LibraryManagementSystem.ViewModels.Authors;
using LibraryManagementSystem.ViewModels.Books;
using LibraryManagementSystem.ViewModels.Pager;
using LibraryManagementSystem.DataAccess.Entities;
using LibraryManagementSystem.DataAccess.Repositories;
using LibraryManagementSystem.DataAccess.DataAccessLayer;
using DataAccess;

namespace LibraryManagementSystem.Controllers
{
    [AuthenticationFilter]
    public class AuthorsController : BaseController
    {
        public ActionResult Index(string sortOrder)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            AuthorsRepository authorsRepository = new AuthorsRepository(context);
            AuthorsIndexVM model = new AuthorsIndexVM();
            this.TryUpdateModel(model);

            model.AuthorsPager = model.AuthorsPager ?? new GenericPagerVM();
            model.AuthorsPager.CurrentPage = model.AuthorsPager.CurrentPage == 0 ? 1 : model.AuthorsPager.CurrentPage;          
            model.AuthorsPager.Action = "Index";
            model.AuthorsPager.Controller = "Authors";
            model.AuthorsPager.Prefix = "AuthorsPager";
            model.AuthorsPager.CurrentParameters = new Dictionary<string, object>()
            {
                { "AuthorName", model.AuthorName },
                { "AuthorsPager.CurrentPage", model.AuthorsPager.CurrentPage }                                        
            };

            #region Sorting and Filtering
            Expression<Func<Author, bool>> filter = a =>
                    string.IsNullOrEmpty(model.AuthorName) || (a.FirstName.Contains(model.AuthorName) || a.LastName.Contains(model.AuthorName));
            model.AuthorsPager.PagesCount = GetPagesCount(filter);

            ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            switch (sortOrder)
            {
                case "name_desc":
                    model.AuthorsList = authorsRepository
                        .GetAll(model.AuthorsPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter,
                        order: x => x.OrderByDescending(a => a.FirstName));
                    break;
                default:
                    model.AuthorsList = authorsRepository
                        .GetAll(model.AuthorsPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter,
                        order: x => x.OrderBy(a => a.FirstName));
                    break;
            } 
            #endregion

            return View(model);
        }

        public ActionResult EditAuthor(int id)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            AuthorsRepository authorsRepository = new AuthorsRepository(context);
            AuthorsEditAuthorVM model = new AuthorsEditAuthorVM();           

            Author author = authorsRepository.GetByID(id);
            if (id > 0)
            {
                if (author == null)
                {
                    return RedirectToAction("Index", "Authors");
                }

                model.ID = author.ID;
                model.FirstName = author.FirstName;
                model.LastName = author.LastName;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        public ActionResult EditAuthor(AuthorsEditAuthorVM model)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            AuthorsRepository authorsRepository = new AuthorsRepository(context);            

            Author author = null;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                if (model.ID > 0)
                {
                    author = authorsRepository.GetByID(model.ID);
                }
                else
                {
                    author = new Author();
                }

                author.ID = model.ID;
                author.FirstName = model.FirstName;
                author.LastName = model.LastName;

                authorsRepository.Save(author);
            }

            return RedirectToAction("Index", "Authors");
        }

        public ActionResult Details(int id, string sortOrder)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            AuthorsRepository authorsRepository = new AuthorsRepository(context);
            BooksRepository booksRepository = new BooksRepository(context);
            AuthorsDetailsVM model = new AuthorsDetailsVM();
            this.TryUpdateModel(model);

            Author author = authorsRepository.GetByID(id);
            if (author != null)
            {
                model.ID = author.ID;
                model.AuhtorName = author.ToString();
                model.BooksPager = model.BooksPager ?? new GenericPagerVM();
                model.BooksPager.CurrentPage = model.BooksPager.CurrentPage == 0 ? 1 : model.BooksPager.CurrentPage;
                model.BooksPager.Action = "Details";
                model.BooksPager.Controller = "Authors";
                model.BooksPager.Prefix = "BooksPager";
                model.BooksPager.CurrentParameters = new Dictionary<string, object>()
                {
                    { "BookTitle", model.BookTitle },
                    { "PublisherName", model.PublisherName },
                    { "BooksPager.CurrentPage", model.BooksPager.CurrentPage }
                };

                #region Sorting and Filtering
                Expression<Func<Book, bool>> filter = b =>
                           (string.IsNullOrEmpty(model.BookTitle) || b.Title.Contains(model.BookTitle)) &&
                           (string.IsNullOrEmpty(model.PublisherName) || b.Publisher.Name.Contains(model.PublisherName));
                model.BooksPager.PagesCount = GetPagesCount(filter);

                ViewBag.TitleSortParam = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
                ViewBag.PublisherSortParam = sortOrder == "Publisher" ? "publisher_desc" : "Publisher";
                ViewBag.StockCountSortParam = sortOrder == "StockCount" ? "stockCount_desc" : "StockCount";
                switch (sortOrder)
                {
                    case "title_desc":
                        model.Books = booksRepository
                            .GetAll(model.BooksPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderByDescending(b => b.Title))
                            .Where(b => b.Authors.Any(a => a.ID == id))
                            .ToList();
                        break;
                    case "Publisher":
                        model.Books = booksRepository
                            .GetAll(model.BooksPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderBy(b => b.Publisher.Name))
                            .Where(b => b.Authors.Any(a => a.ID == id))
                            .ToList();
                        break;
                    case "publisher_desc":
                        model.Books = booksRepository
                            .GetAll(model.BooksPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderByDescending(b => b.Publisher.Name))
                            .Where(b => b.Authors.Any(a => a.ID == id))
                            .ToList();
                        break;
                    case "StockCount":
                        model.Books = booksRepository
                            .GetAll(model.BooksPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderBy(b => b.StockCount))
                            .Where(b => b.Authors.Any(a => a.ID == id))
                            .ToList();
                        break;
                    case "stockCount_desc":
                        model.Books = booksRepository
                            .GetAll(model.BooksPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderByDescending(b => b.StockCount))
                            .Where(b => b.Authors.Any(a => a.ID == id))
                            .ToList();
                        break;
                    default:
                        model.Books = booksRepository
                            .GetAll(model.BooksPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderBy(b => b.Title))
                            .Where(b => b.Authors.Any(a => a.ID == id))
                            .ToList();
                        break;
                } 
                #endregion

                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Authors");
            }
        }

        public int GetPagesCount(Expression<Func<Author, bool>> filter = null)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            AuthorsRepository authorsRepository = new AuthorsRepository(context);

            int pagesCount = 0;
            int pageSize = ApplicationConfiguration.ItemsPerPage;
            int authorsCount = 0;
            authorsCount = authorsRepository.Count(filter);
            pagesCount = authorsCount / pageSize;
            if ((authorsCount % pageSize) > 0)
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