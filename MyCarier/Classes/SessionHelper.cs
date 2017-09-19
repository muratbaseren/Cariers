using MyCarier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCarier.Classes
{
    public class SessionHelper
    {
        public static string GetCurrentEmail()
        {
            return HttpContext.Current.Session["CurrentUser"].ToString();
        }

        public static PersonInfo GetCurrentPersonInfo(DatabaseContext db = null)
        {
            if (db == null)
                db = new DatabaseContext();

            string email = GetCurrentEmail();
            return db.PersonInfos.FirstOrDefault(x => x.Email == email);
        }

        public static void SetCurrentEmail(string email)
        {
            HttpContext.Current.Session["CurrentUser"] = email;
        }
    }
}