using MyCarier.Filters;
using MyCarier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyCarier.Controllers
{
    [ActFilter, ExcFilter]
    public class AdminController : Controller
    {
        DatabaseContext db = new DatabaseContext();

        [AuthFilter]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Unauthorized()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult SignOut()
        {
            Session.Clear();
            return RedirectToAction("SignIn","Home");
        }
    }
}