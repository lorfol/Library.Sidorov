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
            ViewBag.IsAuth = User.Identity.IsAuthenticated;
            ViewBag.IsAdmin = User.IsInRole("Administrator");
            ViewBag.IsLibrarian = User.IsInRole("Librarian");

            return View(listOfBooks);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult BooksList()
        {
            return View();
        }
    }
}