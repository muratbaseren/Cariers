using MyCarier.Filters;
using MyCarier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyCarier.Controllers
{
    [AuthFilter, AdminFilter, ExcFilter, ActFilter]
    public class LogsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index()
        {
            return View(db.Logs.OrderByDescending(x => x.Date).ToList());
        }
    }
}