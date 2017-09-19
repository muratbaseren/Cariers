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
using MyCarier.Classes;

namespace MyCarier.Controllers
{
    [AuthFilter, ExcFilter, ActFilter]
    public class SocialsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Socials
        public ActionResult Index()
        {
            PersonInfo pi = SessionHelper.GetCurrentPersonInfo(db);

            return View(db.Socials.Where(x => x.PersonInfo.Id == pi.Id).ToList());
        }

        // GET: Socials/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Social social = db.Socials.Find(id);
            if (social == null)
            {
                return HttpNotFound();
            }
            return View(social);
        }

        // GET: Socials/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Socials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Social social)
        {
            if (ModelState.IsValid)
            {
                Social dbSocial = db.Socials.FirstOrDefault(x => x.Url == social.Url);

                if(dbSocial != null)
                {
                    ModelState.AddModelError(nameof(social.Url), "Url is already exists.");
                    return View(social);
                }

                social.Id = Guid.NewGuid();

                PersonInfo pi = SessionHelper.GetCurrentPersonInfo(db);
                social.PersonInfo = pi;

                db.Socials.Add(social);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(social);
        }

        // GET: Socials/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Social social = db.Socials.Find(id);
            if (social == null)
            {
                return HttpNotFound();
            }
            return View(social);
        }

        // POST: Socials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Social social)
        {
            if (ModelState.IsValid)
            {
                Social dbSocial = db.Socials.FirstOrDefault(x => x.Url == social.Url);

                if (dbSocial != null)
                {
                    ModelState.AddModelError(nameof(social.Url), "Url is already exists.");
                    return View(social);
                }

                db.Entry(social).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(social);
        }

        // GET: Socials/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Social social = db.Socials.Find(id);
            if (social == null)
            {
                return HttpNotFound();
            }
            return View(social);
        }

        // POST: Socials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Social social = db.Socials.Find(id);
            db.Socials.Remove(social);
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
