using Library.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using PagedList;
using Library.Domain.Core.Models;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Library.Infrastructure.Data;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;

namespace Library.App.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdministratorAccountController : Controller
    {
        private ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

        IUnitOfWork unitOfWork;

        public AdministratorAccountController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ActionResult CreateBook()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateBookPost()
        {
            return View();
        }

        public ActionResult UpdateBook()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdateBookPost()
        {
            return View();
        }

        public ActionResult DeleteBook()
        {
            return View();
        }

        public async Task<ActionResult> ManageUsers()
        {
            var userRole = ApplicationUserManager.RoleManager.Roles.FirstOrDefault(f => f.Name == "user").Id;
            var users = await UserManager.Users.Where(t => t.Roles.Any(p=>p.RoleId == userRole)).ToListAsync();

            return View(users);
        }

        public async Task<ActionResult> ManageLibrarians()
        {
            var role = ApplicationUserManager.RoleManager.Roles.FirstOrDefault(f => f.Name == "librarian").Id;
            var librarians = await UserManager.Users.Where(t => t.Roles.Any(p => p.RoleId == role)).ToListAsync();

            return View(librarians);
        }

        public async Task<RedirectToRouteResult> CreateLibrarian(string userId)
        {
            var user = UserManager.Users.FirstOrDefault(u => u.Id == userId);
            await UserManager.AddToRoleAsync(userId, "librarian");
            await UserManager.RemoveFromRoleAsync(userId, "user");

            return RedirectToAction("ManageUsers");
        }

        public async Task<RedirectToRouteResult> DeleteLibrarian(string userId)
        {
            var user = UserManager.Users.FirstOrDefault(u => u.Id == userId);
            await UserManager.AddToRoleAsync(userId, "user");
            await UserManager.RemoveFromRoleAsync(userId, "librarian");

            return RedirectToAction("ManageLibrarians");
        }

        public async Task<RedirectToRouteResult> BanUser(string userId)
        {
            var user = UserManager.Users.FirstOrDefault(u => u.Id == userId);

            if (!UserManager.IsInRole(userId, "banned"))
            {
                await UserManager.AddToRoleAsync(userId, "banned");
                user.IsBanned = true;
            }
            else if (UserManager.IsInRole(userId, "banned"))
            {
                user.IsBanned = true;
            }

            return RedirectToAction("ManageUsers");
        }

        public async Task<RedirectToRouteResult> UnbanUser(string userId)
        {
            var user = UserManager.Users.FirstOrDefault(u => u.Id == userId);

            if (UserManager.IsInRole(userId, "banned"))
            {
                await UserManager.RemoveFromRoleAsync(userId, "banned");
                user.IsBanned = false;
            }
            else if (!UserManager.IsInRole(userId, "banned"))
            {
                user.IsBanned = false;
            }

            return RedirectToAction("ManageUsers");
        }
    }
}