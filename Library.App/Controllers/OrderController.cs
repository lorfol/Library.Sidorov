using AutoMapper;
using Library.App.Hubs;
using Library.App.ViewModels;
using Library.Domain.Core.Models;
using Library.Domain.Interfaces;
using Library.Infrastructure.Data;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.App.Controllers
{
    [System.Web.Mvc.Authorize]
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
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<OrderHub>();
            var userID = UserManager.Users.FirstOrDefault(u => u.UserName == userName).Id;
            Order order = new Order()
            {
                BookId = bookId,
                TakenDate = DateTime.Now,
                ReturnDate = DateTime.Now.AddMinutes(3),
                LateFine = 0,
                Status = Domain.Core.Enums.OrderStatus.New,
                UserId = userID
            };
            var orderId = this.unitOfWork.Orders.Create(order).Id;

            this.unitOfWork.Save();

            var orderFromDb = this.unitOfWork.Orders.Find(ord => ord.Id == orderId).FirstOrDefault();
            var orderView = Mapper.Map<Order, OrderViewModel>(orderFromDb);
            var htmlParetial = this.RenderViewToString(ControllerContext, "OrderPartialView", orderView);
            hubContext.Clients.All.addMessage(htmlParetial);

            return new HttpStatusCodeResult(200);
        }

        [HttpGet]
        public ActionResult GetCountOfOrders()
        {
            var user = this.HttpContext.User;
            this.unitOfWork.Orders.Find(f => f.User.Name == user.Identity.Name);

            return this.Json(2, JsonRequestBehavior.AllowGet);
        }

        public string RenderViewToString(ControllerContext context, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = context.RouteData.GetRequiredString("action");

            var viewData = new ViewDataDictionary(model);

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(context, viewName);
                var viewContext = new ViewContext(context, viewResult.View, viewData, new TempDataDictionary(), sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
    }
}