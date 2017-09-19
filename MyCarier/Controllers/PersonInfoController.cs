using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyCarier.Models;
using MyCarier.Filters;

namespace MyCarier.Controllers
{
    [AuthFilter, ExcFilter, ActFilter]
    public class PersonInfoController : Controller
    {
        private DatabaseContext db = new DatabaseContext();


        // GET: PersonInfo/Edit
        public ActionResult Edit()
        {

            if (Session["CurrentUser"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string email = Session["CurrentUser"].ToString();
            
            PersonInfo personInfo = db.PersonInfos.FirstOrDefault(x => x.Email == email);
            if (personInfo == null)
            {
                return HttpNotFound();
            }
            return View(personInfo);
        }

        // POST: PersonInfo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(PersonInfo personInfo, HttpPostedFileBase uploaded)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personInfo).State = EntityState.Modified;
                
                if (uploaded != null)
                {
                    string filepath =
                        Server.MapPath("~/images") + "/" + personInfo.Id.ToString() + ".png";

                    uploaded.SaveAs(filepath);

                    personInfo.PhotoImageName = personInfo.Id.ToString() + ".png";
                }

                if(db.SaveChanges() > 0)
                {
                    Session["CurrentUser"] = personInfo.Email;
                }
                
                return RedirectToAction("Edit");
            }
            return View(personInfo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
