using Library.Domain.Core.Models;
using Library.Infrastructure.Data;
using System;
using System.Collections.Generic;
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
            //Book book = new Book()
            //{
            //    Count = 3,
            //    Description = "Desc",
            //    Name = "C#",
            //    PublicationDate = DateTime.UtcNow
            //};

            //User user = new User()
            //{
            //    Email = "esfseg",
            //    FullName = "Vova",
            //    Password = "1"
            //};

            //this.context.Books.Add(book);
            //this.context.Users.Add(user);
            //this.context.SaveChanges();

            //var userFromDb = this.context.Users.Find(1);
            //var order = new Order
            //{
            //    BookId = 1,
            //    LateFine = 0,
            //    TakenDate = DateTime.UtcNow,
            //    ReturnDate = DateTime.UtcNow.AddDays(15)
            //};
            //userFromDb.Orders.Add(order);

            ////var bookFromDb = this.context.Books.Find(1);
            ////bookFromDb.Count--;
            //this.context.SaveChanges();
            return View();
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