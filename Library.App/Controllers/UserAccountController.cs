using AutoMapper;
using Library.App.ViewModels;
using Library.Domain.Core.Models;
using Library.Domain.Interfaces;
using Library.Infrastructure.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.App.Controllers
{
    [Authorize]
    public class UserAccountController : Controller
    {
        private ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

        public ActionResult UserAccount(string userName)
        {
            var userFromDb = UserManager.Users.FirstOrDefault(u => u.UserName == userName);
            var user = Mapper.Map<User, UserViewModel>(userFromDb);

            return View(user);
        }

        public ActionResult UserOrders(string userName)
        {
            var user = UserManager.Users.FirstOrDefault(u => u.UserName == userName);
            var userOrdersFromDb = user.Orders;
            var orders = Mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(userOrdersFromDb);

            return View(orders);
        }
    }
}