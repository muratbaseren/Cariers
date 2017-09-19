using MyCarier.Classes;
using MyCarier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyCarier.Filters
{
    public class AuthFilter : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (HttpContext.Current.Session["CurrentUser"] == null)
            {
                filterContext.Result = new RedirectResult("/Home/SignIn");
            }
        }
    }

    public class AdminFilter : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            PersonInfo pi = SessionHelper.GetCurrentPersonInfo();
            if (pi == null)
            {
                filterContext.Result = new RedirectResult("/Admin/Unauthorized");
            }
        }
    }
}