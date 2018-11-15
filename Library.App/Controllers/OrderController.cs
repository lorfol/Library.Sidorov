using AutoMapper;
using Library.App.Hubs;
using Library.App.ViewModels;
using Library.Domain.Core.Models;
using Library.Domain.Interfaces;
using Library.Infrastructure.Data;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.SignalR;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Library.App.Controllers
{
    [System.Web.Mvc.Authorize(Roles = "user")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        private ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

        public OrderController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // Creating new order
        public async Task<ActionResult> CreateBookOrder(int bookId, bool isAtReadingRoom)
        {
            if (User.IsInRole("banned"))
            {
                return View("BanMessage");
            }
            
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<OrderHub>();
            var user = await this.UserManager.FindByEmailAsync(this.User.Identity.Name);
            var orderId = Guid.NewGuid().ToString();

            Order order = new Order()
            {
                Id = orderId,
                BookId = bookId,
                TakenDate = DateTime.Now,
                LateFine = 0,
                Status = Domain.Core.Enums.OrderStatus.New,
                UserId = user.Id,
                IsAtReadingRoom = isAtReadingRoom
            };

            this.unitOfWork.Orders.Create(order);
            var book = this.unitOfWork.Books.GetById(bookId);
            book.Count--;
            this.unitOfWork.Books.Update(bookId, book);
            this.unitOfWork.Save();

            var orderFromDb = this.unitOfWork.Orders.GetById(orderId);
            var orderView = Mapper.Map<Order, OrderViewModel>(orderFromDb);
            orderView.UserName = user.UserName;

            var htmlPartial = this.RenderViewToString(ControllerContext, "NewOrderPartialView", orderView);
            hubContext.Clients.All.addMessage(htmlPartial); // Calling a method on all clients

            return RedirectToAction("UserOrders", "UserAccount");
        }

        // convert partial view to string
        private string RenderViewToString(ControllerContext context, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = context.RouteData.GetRequiredString("action");
            }

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