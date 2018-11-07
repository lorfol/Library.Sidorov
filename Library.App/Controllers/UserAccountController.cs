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
        public ActionResult MyAccount()
        {

            return View();
        }

        public ActionResult AccountInfo()
        {
            return PartialView();
        }

        public ActionResult MyOrders()
        {
            return PartialView();
        }
    }
}