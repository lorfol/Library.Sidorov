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
        public ActionResult NewOrders()
        {
            return View();
        }

        public ActionResult ConfirmOrder()
        {
            return View();
        }

        public ActionResult RejectOrder()
        {
            return View();
        }

        public ActionResult ConfirmedOrders()
        {
            return View();
        }
    }
}