using AutoMapper;
using Library.App.Hubs;
using Library.App.ViewModels;
using Library.Domain.Core.Models;
using Library.Domain.Interfaces;
using Library.Services.Interfaces;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Library.App.Controllers
{
    [System.Web.Mvc.Authorize(Roles = "librarian")]
    public class LibrarianAccountController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IOrdersServise ordersServise;

        public LibrarianAccountController(IUnitOfWork unitOfWork, IOrdersServise ordersServise)
        {
            this.unitOfWork = unitOfWork;
            this.ordersServise = ordersServise;
        }

        public ActionResult NewOrders()
        {
            var newOrdersFromDb = this.unitOfWork.Orders.Find(ord => ord.Status == Domain.Core.Enums.OrderStatus.New).ToList();
            var orders = Mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(newOrdersFromDb);

            return View(orders);
        }

        public ActionResult ConfirmedOrders()
        {
            var newOrdersFromDb = this.unitOfWork.Orders.Find(ord => ord.Status == Domain.Core.Enums.OrderStatus.OnHands || ord.Status == Domain.Core.Enums.OrderStatus.Overdue).OrderByDescending(ord=>ord.TakenDate).ToList();
            var orders = Mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(newOrdersFromDb);

            return View(orders);
        }

        public ActionResult CloseOrder(string orderId)
        {
            var order = this.unitOfWork.Orders.Find(f => f.Id == orderId).FirstOrDefault();
            order.Status = Domain.Core.Enums.OrderStatus.Closed;
            this.unitOfWork.Orders.Update(orderId, order);
            var book = this.unitOfWork.Books.GetById(order.BookId);
            book.Count++;
            this.unitOfWork.Books.Update(book.Id, book);
            this.unitOfWork.Save();

            var hubContext = GlobalHost.ConnectionManager.GetHubContext<UserOrdersHub>();
            hubContext.Clients.All.addMessage(new { Id = order.Id, Status = order.Status.ToString() });

            return new HttpStatusCodeResult(200);
        }

        public ActionResult ConfirmOrder(string orderId)
        {
            var order = this.unitOfWork.Orders.Find(f => f.Id == orderId).FirstOrDefault();
            order.Status = Domain.Core.Enums.OrderStatus.OnHands;
            order.TakenDate = DateTime.Now;
            order.ReturnDate = DateTime.Now.AddHours(10);
            this.unitOfWork.Orders.Update(orderId, order);
            this.unitOfWork.Save();

            var hubContext = GlobalHost.ConnectionManager.GetHubContext<UserOrdersHub>();
            hubContext.Clients.All.addMessage(new { Id = order.Id, Status = order.Status.ToString() });

            return new HttpStatusCodeResult(200);
        }

        public ActionResult RejectOrder(string orderId)
        {
            var order = this.unitOfWork.Orders.Find(f => f.Id == orderId).FirstOrDefault();
            order.Status = Domain.Core.Enums.OrderStatus.Rejected;
            this.unitOfWork.Orders.Update(orderId, order);
            var book = this.unitOfWork.Books.GetById(order.BookId);
            book.Count++;
            this.unitOfWork.Books.Update(book.Id, book);
            this.unitOfWork.Save();

            var hubContext = GlobalHost.ConnectionManager.GetHubContext<UserOrdersHub>();
            hubContext.Clients.All.addMessage(new { Id = order.Id, Status = order.Status.ToString() });

            return new HttpStatusCodeResult(200);
        }

        public ActionResult ChargeFine() // govno. to servise!
        {
            ordersServise.ChargeFine();

            //DateTime dateTime = DateTime.Now;
            //TimeSpan ts = new TimeSpan(23, 59, 59);
            //dateTime = dateTime.Date + ts;

            //var overdueOrders = this.unitOfWork.Orders
            //    .Find(t => t.Status == Domain.Core.Enums.OrderStatus.OnHands)
            //    .Where(ord => ord.ReturnDate.Value < dateTime).ToList();

            //foreach (var order in overdueOrders)
            //{
            //    var penalty = dateTime.Subtract(order.ReturnDate.Value).Days;
            //    if (order.LateFine == 0)
            //    {
            //        order.LateFine += 10 * penalty;

            //    }
            //    else
            //    {
            //        order.LateFine += 10;
            //    }
            //    order.Status = Domain.Core.Enums.OrderStatus.Overdue;
            //    this.unitOfWork.Orders.Update(order.Id, order);
            //    this.unitOfWork.Save();
            //}

            return RedirectToAction("ConfirmedOrders");
        }
    }

}