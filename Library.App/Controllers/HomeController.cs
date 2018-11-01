using Library.Domain.Core.Models;
using Library.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.App.Controllers
{
    public class HomeController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        public ActionResult Index()
        {
            var listOfBooks = unitOfWork.Books.GetAll();

            return View(listOfBooks);
        }

        public ActionResult Details(int bookId)
        {
            var book = unitOfWork.Books.GetById(bookId);
            if (book != null)
                return View("Details", book);
            return HttpNotFound();
        }

        public ActionResult Orders()
        {
            var orders = unitOfWork.Orders.GetAll(); // TODO: not all orders. only current user orders
            return View(orders);
        }

        [Authorize]
        public ActionResult CreateOrder(int bookId)
        {
            
            return View();
        }
    }
}