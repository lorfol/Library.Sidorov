using Library.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.App.Controllers
{
    public class OrdersController : Controller
    {
        IUnitOfWork unitOfWork;

        public OrdersController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        public ActionResult OrderBook(int bookId, bool isAtReadingRoom)
        {
            var user = this.HttpContext.User;
            return new HttpStatusCodeResult(200);
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