using AutoMapper;
using Library.App.ViewModels;
using Library.Domain.Core.Models;
using Library.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.App.Controllers
{
    [Authorize(Roles = "librarian")]
    public class LibrarianAccountController : Controller
    {
        IUnitOfWork unitOfWork;

        public LibrarianAccountController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ActionResult NewOrders()
        {
            var newOrdersFromDb = this.unitOfWork.Orders.Find(ord => ord.Status == Domain.Core.Enums.OrderStatus.New).ToList();
            var orders = Mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(newOrdersFromDb);

            return View(orders);
        }

        public ActionResult ConfirmedOrders()
        {
            var newOrdersFromDb = this.unitOfWork.Orders.Find(ord => ord.Status == Domain.Core.Enums.OrderStatus.OnHands);
            var orders = Mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(newOrdersFromDb);

            return View(orders);
        }

        public ActionResult ConfirmOrder(int orderId)
        {
            var order = this.unitOfWork.Orders.Find(f => f.Id == orderId).FirstOrDefault();
            order.Status = Domain.Core.Enums.OrderStatus.OnHands;
            this.unitOfWork.Orders.Update(orderId, order);
            this.unitOfWork.Save();

            return new HttpStatusCodeResult(200);
        }


        public ActionResult RejectOrder(int orderId)
        {
            var order = this.unitOfWork.Orders.Find(f => f.Id == orderId).FirstOrDefault();
            order.Status = Domain.Core.Enums.OrderStatus.Rejected;
            this.unitOfWork.Orders.Update(orderId, order);
            this.unitOfWork.Save();

            return new HttpStatusCodeResult(200);
        }
    }

}