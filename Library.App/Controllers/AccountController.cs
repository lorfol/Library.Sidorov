using Library.App.Models;
using Library.Domain.Core.Models;
using Library.Infrastructure.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Library.App.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        // return form for register user
        public ActionResult Register()
        {
            return View("Register");
        }

        // register new user in sistem
        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Name = model.Name, UserName = model.Email, Email = model.Email };
                IdentityResult result = await this.UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    ClaimsIdentity claim = await this.UserManager.CreateIdentityAsync(user,
                                    DefaultAuthenticationTypes.ApplicationCookie);

                    this.AuthenticationManager.SignOut();
                    this.AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);

                    this.UserManager.AddToRole(user.Id, "user");

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }

        // return form for login
        public ActionResult Login()
        {
            return View("Login");
        }

        // ligin user in sistem
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = UserManager.Find(model.Email, model.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "Wrong email or password.");
                }
                else
                {
                    ClaimsIdentity claim = await UserManager
                        .CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);

                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        [Authorize]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login");
        }
    }
}