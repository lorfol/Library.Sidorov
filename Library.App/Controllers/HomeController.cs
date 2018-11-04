using AutoMapper;
using Library.App.ViewModels;
using Library.Domain.Core.Models;
using Library.Domain.Interfaces;
using Library.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using System.Linq.Dynamic;

namespace Library.App.Controllers
{
    public class HomeController : Controller
    {
        IUnitOfWork unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ActionResult Index(string search, int? page, string orderBy = "Name", string orderDirection = "asc")
        {
            //var books = Mapper.Map<IEnumerable<Book>, IEnumerable<BookViewModel>>(listOfBooks);

            IPagedList<Book> listOfBooks;

            if (!string.IsNullOrEmpty(search))
            {
                listOfBooks = this.unitOfWork.Books.Find(f => f.Name.ToLower().Contains(search) ||
                f.Authors.Select(z => z).Any(g => g.Name.ToLower().Contains(search))).OrderBy(string.Join(" ", orderBy, orderDirection)).ToPagedList(page ?? 1, 10);
            }
            else
            {
                listOfBooks = unitOfWork.Books.GetAll().OrderBy(string.Join(" ", orderBy, orderDirection)).ToPagedList(page ?? 1, 10);
            }
            //OrderBy(string.Join(" ", orderBy, "desc")

            ViewBag.PreviosOrderBy = orderBy;
            ViewBag.PreviosOrderDirection = orderDirection;

            return View(listOfBooks);
        }


        [AllowAnonymous]
        public ActionResult Details(int bookId)
        {
            var book = unitOfWork.Books.GetById(bookId);
            if (book != null)
                return View("Details", book);
            return HttpNotFound();
        }

        [Authorize]
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

        public ActionResult AccountInfo(int userId)
        {
            
            return PartialView("AccountInfoPartial");
        }

        public ActionResult MyOrders(int userId)
        {

            return PartialView("MyOrdersPartial");
        }
    }
}