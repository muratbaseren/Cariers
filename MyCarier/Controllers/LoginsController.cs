using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyCarier.Models;
using System.Threading;
using MyCarier.Filters;
using MyCarier.Classes;
using System.Web.Helpers;

namespace MyCarier.Controllers
{
    [AuthFilter,AdminFilter, ExcFilter, ActFilter]
    public class LoginsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Skills
        public ActionResult Index()
        {
            //if(IsUserAuthenticated() == false) return RedirectToAction("SignIn", "Admin");

            //object o = 0;
            //int i = 5 / (int)o;

            PersonInfo pi = SessionHelper.GetCurrentPersonInfo(db);

            return View(db.Logins.Where(x => x.PersonInfo.Id == pi.Id).ToList());
        }

        // GET: Skills/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = db.Logins.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        // GET: Skills/Create
        public ActionResult Create()
        {
            //if (IsUserAuthenticated() == false) return RedirectToAction("SignIn", "Admin");

            return View();
        }

        // POST: Skills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Login login)
        {
            if (ModelState.IsValid)
            {
                login.PersonInfo = SessionHelper.GetCurrentPersonInfo(db);

                login.Password = Crypto.SHA256(login.Password);
                login.Id = Guid.NewGuid();

                db.Logins.Add(login);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(login);
        }

        // GET: Skills/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = db.Logins.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        // POST: Skills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Login login)
        {
            ModelState.Remove("Password");

            if (ModelState.IsValid)
            {
                PersonInfo me = SessionHelper.GetCurrentPersonInfo(db);
                Login loginMe = db.Logins.FirstOrDefault(x => x.Email == me.Email);

                PersonInfo dbPerson = db.PersonInfos.FirstOrDefault(x => x.Email == login.Email);

                if (dbPerson != null && 
                    (dbPerson.Id != me.Id || (dbPerson.Id == me.Id && login.Id != loginMe.Id)))
                {
                    ModelState.AddModelError(nameof(login.Email), "This e-mail is already exists.");
                    return View(login);
                }

                db.Entry(login).State = EntityState.Modified;

                if (loginMe.Id == login.Id)
                    me.Email = login.Email;

                if (db.SaveChanges() > 0)
                {
                    if (loginMe.Id == login.Id)
                        SessionHelper.SetCurrentEmail(login.Email);
                }

                return RedirectToAction("Index");
            }
            return View(login);
        }

        // GET: Skills/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = db.Logins.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        // POST: Skills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Login login = db.Logins.Find(id);
            db.Logins.Remove(login);
            db.SaveChanges();
            return RedirectToAction("Index");
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
