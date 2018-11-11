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
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Library.App.Controllers
{
    [Authorize]
    public class UserAccountController : Controller
    {
        private ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

        public async Task<ActionResult> UserAccount()
        {
            var user = await this.UserManager.FindByEmailAsync(this.User.Identity.Name);
            var mappedUser = Mapper.Map<User, UserViewModel>(user);

            return View(mappedUser);
        }

        public async Task<ActionResult> UserOrders(string userName)
        {
            var user = await this.UserManager.FindByEmailAsync(this.User.Identity.Name);
            var userOrdersFromDb = user.Orders.OrderByDescending(ord=>ord.TakenDate).ToList();
            var orders = Mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(userOrdersFromDb);

            return View(orders);
        }
    }
}