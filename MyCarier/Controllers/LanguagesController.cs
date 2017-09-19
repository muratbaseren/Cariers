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

namespace MyCarier.Controllers
{
    [AuthFilter, ExcFilter, ActFilter]
    public class LanguagesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Skills
        public ActionResult Index()
        {
            //if(IsUserAuthenticated() == false) return RedirectToAction("SignIn", "Admin");

            //object o = 0;
            //int i = 5 / (int)o;

            PersonInfo pi = SessionHelper.GetCurrentPersonInfo(db);

            return View(db.Abilities.Where(x => x.PersonInfo.Id == pi.Id && x.Category == "lang").OrderByDescending(x => x.Scale).ToList());
        }

        // GET: Skills/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ability ability = db.Abilities.Find(id);
            if (ability == null)
            {
                return HttpNotFound();
            }
            return View(ability);
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
        public ActionResult Create(Ability ability)
        {
            ability.Category = "lang";

            ModelState.Remove("Category");

            if (ModelState.IsValid)
            {
                PersonInfo pi = SessionHelper.GetCurrentPersonInfo(db);
                ability.PersonInfo = pi;

                db.Abilities.Add(ability);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(ability);
        }

        // GET: Skills/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ability ability = db.Abilities.Find(id);
            if (ability == null)
            {
                return HttpNotFound();
            }
            return View(ability);
        }

        // POST: Skills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Ability ability)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ability).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(ability);
        }

        // GET: Skills/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ability ability = db.Abilities.Find(id);
            if (ability == null)
            {
                return HttpNotFound();
            }
            return View(ability);
        }

        // POST: Skills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ability ability = db.Abilities.Find(id);
            db.Abilities.Remove(ability);
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
