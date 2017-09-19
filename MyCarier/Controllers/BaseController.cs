using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyCarier.Controllers
{
    public class BaseController : Controller
    {
        protected bool IsUserAuthenticated()
        {
            return Session["CurrentUser"] != null;
        }
    }
}