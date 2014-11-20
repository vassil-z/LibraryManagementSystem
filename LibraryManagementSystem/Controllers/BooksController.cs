using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Filters;
using LibraryManagementSystem.ViewModels.Books;
using LibraryManagementSystem.ViewModels.Pager;
using LibraryManagementSystem.DataAccess.Entities;
using LibraryManagementSystem.DataAccess.Repositories;
using LibraryManagementSystem.DataAccess.DataAccessLayer;

namespace LibraryManagementSystem.Controllers
{
    [AuthenticationFilter]
    public class BooksController : BaseController
    {
        public ActionResult Index(string sortOrder)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            BooksRepository booksRepository = new BooksRepository(context);
            BooksIndexVM model = new BooksIndexVM();
            this.TryUpdateModel(model);

            model.BooksPager = model.BooksPager ?? new GenericPagerVM();
            model.BooksPager.CurrentPage = model.BooksPager.CurrentPage == 0 ? 1 : model.BooksPager.CurrentPage;
            model.BooksPager.Prefix = "BooksPager";
            model.BooksPager.Action = "Index";
            model.BooksPager.Controller = "Books";
            model.BooksPager.CurrentParameters = new Dictionary<string, object>()
            {
                { "Title", model.Title },
                { "Publisher", model.Publisher },
                { "BooksPager.CurrentPage", model.BooksPager.CurrentPage }
            };

            #region Sorting and Filtering
            Expression<Func<Book, bool>> filter = b =>
                   (string.IsNullOrEmpty(model.Title) || b.Title.Contains(model.Title)) &&
                   (string.IsNullOrEmpty(model.Publisher) || b.Publisher.Name.Contains(model.Publisher));
            model.BooksPager.PagesCount = GetPagesCount(filter);

            ViewBag.TitleSortParam = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.PublisherSortParam = sortOrder == "Publisher" ? "publisher_desc" : "Publisher";
            ViewBag.StockCountSortParam = sortOrder == "StockCount" ? "stockCount_desc" : "StockCount";
            switch (sortOrder)
            {
                case "title_desc":
                    model.BooksList = booksRepository
                        .GetAll(model.BooksPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderByDescending(b => b.Title))
                        .ToList();
                    break;
                case "Publisher":
                    model.BooksList = booksRepository
                        .GetAll(model.BooksPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderBy(b => b.Publisher.Name))
                        .ToList();
                    break;
                case "publisher_desc":
                    model.BooksList = booksRepository
                        .GetAll(model.BooksPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderByDescending(b => b.Publisher.Name))
                        .ToList();
                    break;
                case "StockCount":
                    model.BooksList = booksRepository
                        .GetAll(model.BooksPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderBy(b => b.StockCount))
                        .ToList();
                    break;
                case "stockCount_desc":
                    model.BooksList = booksRepository
                        .GetAll(model.BooksPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderByDescending(b => b.StockCount))
                        .ToList();
                    break;
                default:
                    model.BooksList = booksRepository
                        .GetAll(model.BooksPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, filter, order: x => x.OrderBy(b => b.Title))
                        .ToList();
                    break;
            }
            #endregion

            return View(model);
        }

        public ActionResult EditBook(int id)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            BooksRepository booksRepository = new BooksRepository(context);
            PublishersRepository publishersRepository = new PublishersRepository(context);
            BooksEditBookVM model = new BooksEditBookVM();

            Book book = booksRepository.GetByID(id);
            if (id > 0)
            {
                if (book == null)
                {
                    return RedirectToAction("Index", "Books");
                }

                model.ID = book.ID;
                model.Title = book.Title;
                model.PublisherID = book.Publisher.ID;
                model.StockCount = book.StockCount;
                model.DeliveryPrice = book.DeliveryPrice;
                model.DateReceived = book.DateReceived;
                model.DatePublished = book.DatePublished;
            }
            else
            {
                book = new Book();
            }

            model.Publishers = model.Publishers ?? new List<SelectListItem>();
            model.Publishers = SelectListHandler.Create<Publisher>(
                publishersRepository.GetAll(), p => p.Name, p => p.ID.ToString(), model.PublisherID.ToString());

            return View(model);
        }

        [HttpPost]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        public ActionResult EditBook(BooksEditBookVM model)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            BooksRepository booksRepository = new BooksRepository(context);
            PublishersRepository publishersRepository = new PublishersRepository(context);

            Book book = null;
            if (!ModelState.IsValid)
            {
                model.Publishers = model.Publishers ?? new List<SelectListItem>();
                model.Publishers = SelectListHandler.Create<Publisher>(
                    publishersRepository.GetAll(), p => p.Name, p => p.ID.ToString(), model.PublisherID.ToString());
                return View(model);
            }
            else
            {
                if (model.ID > 0)
                {
                    book = booksRepository.GetByID(model.ID);
                }
                else
                {
                    book = new Book();
                }

                book.ID = model.ID;
                book.Title = model.Title;
                book.PublisherID = model.PublisherID;
                book.StockCount = model.StockCount;
                book.DeliveryPrice = model.DeliveryPrice;
                book.DateReceived = model.DateReceived;
                book.DatePublished = model.DatePublished;

                booksRepository.Save(book);
            }

            return RedirectToAction("Index", "Books");
        }

        public ActionResult AddBookAuthor(int id)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            BooksRepository booksRepository = new BooksRepository(context);
            AuthorsRepository authorsRepository = new AuthorsRepository(context);
            BooksAddBookAuthorVM model = new BooksAddBookAuthorVM();

            Book book = booksRepository.GetByID(id);
            model.ID = book.ID;
            model.Title = book.Title;
            model.Authors = model.Authors ?? new List<SelectListItem>();
            model.Authors = SelectListHandler.Create<Author>(
                authorsRepository.GetAll(), a => (a.FirstName + " " + a.LastName), a => a.ID.ToString(), model.AuthorID.ToString());

            return View(model);
        }

        [HttpPost]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        public ActionResult AddBookAuthor(BooksAddBookAuthorVM model)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            BooksRepository booksRepository = new BooksRepository(context);
            AuthorsRepository authorsRepository = new AuthorsRepository(context);

            Book book = null;
            Author author = null;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                book = booksRepository.GetByID(model.ID);
                author = authorsRepository.GetByID(model.AuthorID);

                book.Authors.Add(author);
                booksRepository.Save(book);
            }

            return RedirectToAction("Details/" + model.ID, "Books");
        }

        public ActionResult DeleteBook(int id)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            BooksRepository booksRepository = new BooksRepository(context);
            BooksDeleteBookVM model = new BooksDeleteBookVM();

            Book book = booksRepository.GetByID(id);

            model.ID = book.ID;
            model.Title = book.Title;

            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteBook(BooksDeleteBookVM model)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            BooksRepository booksRepository = new BooksRepository(context);

            Book book = booksRepository.GetByID(model.ID);
            if (book == null)
            {
                return HttpNotFound();
            }
            else
            {
                booksRepository.Delete(book);
            }

            return RedirectToAction("Index");
        }

        public ActionResult ListBookBarcodes(int id)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            BooksRepository booksRepository = new BooksRepository(context);
            BarcodesRepository barcodesRepository = new BarcodesRepository(context);
            BooksListBookBarcodesVM model = new BooksListBookBarcodesVM();
            this.TryUpdateModel(model);

            var book = booksRepository.GetByID(id);
            if (book != null)
            {
                model.BookID = book.ID;
                model.BookTitle = book.Title;
                model.BarcodesPager = model.BarcodesPager ?? new GenericPagerVM();
                model.BarcodesPager.PagesCount = GetPagesCount();
                model.BarcodesPager.CurrentPage =
                    model.BarcodesPager.CurrentPage == 0 ? 1 : model.BarcodesPager.CurrentPage;
                model.Barcodes = barcodesRepository
                    .GetAll(model.BarcodesPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, b => b.BookID == id)                    
                    .ToList();
                model.BarcodesPager.Action = "Index";
                model.BarcodesPager.Controller = "Books";
                model.BarcodesPager.Prefix = "BarcodesPager";
                model.BarcodesPager.CurrentParameters = new Dictionary<string, object>()
                {
                    { "BarcodesPager.CurrentPage", model.BarcodesPager.CurrentPage }
                };

                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Books");
            }
        }

        public ActionResult Details(int id)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            BooksRepository booksRepository = new BooksRepository(context);
            AuthorsRepository authorsRepository = new AuthorsRepository(context);
            BooksDetailsVM model = new BooksDetailsVM();
            this.TryUpdateModel(model);

            Book book = booksRepository.GetByID(id);
            Author author = new Author();
            if (book != null)
            {
                model.ID = book.ID;
                model.Title = book.Title;
                model.Publisher = book.Publisher;
                model.StockCount = book.StockCount;
                model.DeliveryPrice = book.DeliveryPrice;
                model.DateReceived = book.DateReceived;
                model.DatePublished = book.DatePublished;
                model.AuthorsPager = model.AuthorsPager ?? new GenericPagerVM();
                model.AuthorsPager.PagesCount = GetPagesCount();
                model.AuthorsPager.CurrentPage =
                    model.AuthorsPager.CurrentPage == 0 ? 1 : model.AuthorsPager.CurrentPage;
                model.Authors = authorsRepository
                    .GetAll(model.AuthorsPager.CurrentPage, ApplicationConfiguration.ItemsPerPage, a => a.Books.Any(b => b.ID == id), order: x => x.OrderBy(a => a.FirstName))                    
                    .ToList();
                model.AuthorsPager.Action = "Details";
                model.AuthorsPager.Controller = "Books";
                model.AuthorsPager.Prefix = "AuthorsPager";
                model.AuthorsPager.CurrentParameters = new Dictionary<string, object>()
                {
                    { "AuthorsPager.CurrentPage", model.AuthorsPager.CurrentPage }
                };

                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Authors");
            }
        }

        public ActionResult ReturnBook()
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            BooksRepository booksRepository = new BooksRepository(context);
            BooksReturnBookVM model = new BooksReturnBookVM();

            model.DateReturned = DateTime.Now.Date;
            model.Books = booksRepository.GetAll();

            return View(model);
        }

        [HttpPost]
        [ValidateInput(true)]
        public ActionResult ReturnBook(string bookInfo, BooksReturnBookVM model)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            BooksRepository booksRepository = new BooksRepository(context);

            // makes an book info array in format {Barcode No.}{Empty}{Empty}{Book title}
            string[] bookInfoSplitted = bookInfo.Split(' ', '-');

            if (string.IsNullOrEmpty(bookInfo) || bookInfoSplitted[0] == "")
            {
                ModelState.AddModelError("BookBarcodeNumber", "* barcode required");
            }
            if (!ModelState.IsValid)
            {
                model.DateReturned = DateTime.Now;
                model.Books = booksRepository.GetAll();                

                return View(model);
            }
            
            model.BookBarcodeNumber = int.Parse(bookInfoSplitted[0]);
            Book book = booksRepository
                .GetAll(filter: b => b.Barcodes.FirstOrDefault().BarcodeNumber == model.BookBarcodeNumber)
                .FirstOrDefault();
            book.StockCount++;
            booksRepository.Save(book);

            return RedirectToAction("Index", "Books");
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