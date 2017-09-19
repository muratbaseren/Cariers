using MyCarier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyCarier.Filters
{
    public class ActFilter : FilterAttribute, IActionFilter
    {
        DatabaseContext db = new DatabaseContext();

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string uname = string.Empty;

            if (HttpContext.Current.Session["CurrentUser"] != null)
                uname = HttpContext.Current.Session["CurrentUser"].ToString();

            Log log = new Log()
            {
                Date = DateTime.Now,
                Username = uname,
                ActionName = filterContext.ActionDescriptor.ActionName,
                ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                Description = "OnActionExecuted"
            };

            db.Logs.Add(log);
            db.SaveChanges();
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string uname = string.Empty;

            if (HttpContext.Current.Session["CurrentUser"] != null)
                uname = HttpContext.Current.Session["CurrentUser"].ToString();

            Log log = new Log()
            {
                Date = DateTime.Now,
                Username = uname,
                ActionName = filterContext.ActionDescriptor.ActionName,
                ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                Description = "OnActionExecuting"
            };

            db.Logs.Add(log);
            db.SaveChanges();
        }
    }
}