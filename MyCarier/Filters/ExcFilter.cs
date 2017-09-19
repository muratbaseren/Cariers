using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyCarier.Filters
{
    public class ExcFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            HttpContext.Current.Session["LastError"] = filterContext.Exception;
            filterContext.Result = new RedirectResult("/Home/Error");
        }
    }
}