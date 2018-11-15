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
        private readonly IOrdersService ordersService;

        public LibrarianAccountController(IUnitOfWork unitOfWork, IOrdersService ordersService)
        {
            this.unitOfWork = unitOfWork;
            this.ordersService = ordersService;
        }

        // show new orders for librarian
        public ActionResult NewOrders()
        {
            var newOrdersFromDb = this.unitOfWork.Orders.Find(ord => ord.Status == Domain.Core.Enums.OrderStatus.New).ToList();
            var orders = Mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(newOrdersFromDb);

            return View(orders);
        }

        // show confirmed order for librarian
        public ActionResult ConfirmedOrders()
        {
            var newOrdersFromDb = this.unitOfWork.Orders.Find(ord => ord.Status == Domain.Core.Enums.OrderStatus.OnHands || ord.Status == Domain.Core.Enums.OrderStatus.Overdue).OrderByDescending(ord=>ord.TakenDate).ToList();
            var orders = Mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(newOrdersFromDb);

            return View(orders);
        }

        // close order (when book returned)
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

        // confirm user order
        public ActionResult ConfirmOrder(string orderId)
        {
            var order = this.unitOfWork.Orders.Find(f => f.Id == orderId).FirstOrDefault();
            order.Status = Domain.Core.Enums.OrderStatus.OnHands;
            order.TakenDate = DateTime.Now;
            order.ReturnDate = DateTime.Now.AddHours(10);
            this.unitOfWork.Orders.Update(orderId, order);
            this.unitOfWork.Save();

            var hubContext = GlobalHost.ConnectionManager.GetHubContext<UserOrdersHub>();
            hubContext.Clients.All.addMessage( new { Id = order.Id, Status = order.Status.ToString() });

            return new HttpStatusCodeResult(200);
        }

        // reject user order
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

        // call service to calculate the debt for orders
        public ActionResult ChargeFine()
        {
            ordersService.ChargeFine();

            return RedirectToAction("ConfirmedOrders");
        }
    }
}