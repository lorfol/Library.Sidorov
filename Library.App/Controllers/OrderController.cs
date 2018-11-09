using AutoMapper;
using Library.App.ViewModels;
using Library.Domain.Core.Models;
using Library.Domain.Interfaces;
using Library.Infrastructure.Data;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.App.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        IUnitOfWork unitOfWork;

        private ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

        public OrderController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ActionResult CreateBookOrder(int bookId, string userName)
        {
            var bookFromDb = this.unitOfWork.Books.GetById(bookId);
            Order order = new Order()
            {
                Book = bookFromDb,
                TakenDate = DateTime.Now,
                ReturnDate = DateTime.Now.AddMinutes(3),
                LateFine = 0,
                Status = Domain.Core.Enums.OrderStatus.New,
                User = UserManager.Users.FirstOrDefault(u => u.UserName == userName),
                

            };


            return RedirectToAction("UserOrders", "UserAccount");
        }

        [HttpGet]
        public ActionResult GetCountOfOrders()
        {
            var user = this.HttpContext.User;
            this.unitOfWork.Orders.Find(f => f.User.Name == user.Identity.Name);

            return this.Json(2, JsonRequestBehavior.AllowGet);
        }
    }
}