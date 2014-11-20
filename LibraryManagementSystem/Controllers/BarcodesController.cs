using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Filters;
using LibraryManagementSystem.ViewModels.Books;
using LibraryManagementSystem.ViewModels.Barcodes;
using LibraryManagementSystem.ViewModels.Pager;
using LibraryManagementSystem.DataAccess.Entities;
using LibraryManagementSystem.DataAccess.Repositories;
using LibraryManagementSystem.DataAccess.DataAccessLayer;
using DataAccess;

namespace LibraryManagementSystem.Controllers
{
    [AuthenticationFilter]
    public class BarcodesController : BaseController
    {        
        public ActionResult EditBarcode(int id, int bookID)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            BarcodesRepository barcodesRepository = new BarcodesRepository(context);
            BooksRepository booksRepository = new BooksRepository(context);
            BarcodesEditBarcodeVM model = new BarcodesEditBarcodeVM();

            Barcode barcode = barcodesRepository.GetByID(id);
            if (id > 0)
            {
                if (barcode == null)
                {
                    barcode.BookID = bookID;                    
                }

                model.ID = barcode.ID;
                model.BookID = barcode.BookID;
                model.BarcodeNumber = barcode.BarcodeNumber;                
            }     

            return View(model);
        }

        [HttpPost]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        public ActionResult EditBarcode(BarcodesEditBarcodeVM model)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            BarcodesRepository barcodesRepository = new BarcodesRepository(context);            

            Barcode barcode = null;
            if (barcodesRepository.GetAll().Any(b => b.BarcodeNumber == model.BarcodeNumber) &&
                model.ID != barcodesRepository.GetAll(filter: b => b.BarcodeNumber == model.BarcodeNumber).FirstOrDefault().ID)
            {
                ModelState.AddModelError("BarcodeNumber", "* barcode already exists");   
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                if (model.ID > 0)
                {
                    barcode = barcodesRepository.GetByID(model.ID);
                }
                else
                {
                    barcode = new Barcode();
                }

                barcode.ID = model.ID;
                barcode.BookID = model.BookID;
                barcode.BarcodeNumber = model.BarcodeNumber;

                barcodesRepository.Save(barcode);
            }

            return RedirectToAction("ListBookBarcodes/" + barcode.BookID, "Books");
        }

        public ActionResult DeleteBarcode(int id)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            BarcodesRepository barcodesRepository = new BarcodesRepository(context);
            BarcodesDeleteBarcodeVM model = new BarcodesDeleteBarcodeVM();

            Barcode barcode = barcodesRepository.GetByID(id);
            model.ID = barcode.ID;
            model.BarcodeNumber = barcode.BarcodeNumber;

            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteBarcode(BarcodesDeleteBarcodeVM model)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            BarcodesRepository barcodesRepository = new BarcodesRepository(context);

            Barcode barcode = barcodesRepository.GetByID(model.ID);
            if (barcode == null)
            {
                return HttpNotFound();
            }
            else
            {
                barcodesRepository.Delete(barcode);
            }

            return RedirectToAction("ListBookBarcodes/" + barcode.BookID, "Books");
        }
    }
}