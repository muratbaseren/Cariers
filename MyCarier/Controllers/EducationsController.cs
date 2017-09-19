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
    public class EducationsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Education
        public ActionResult Index()
        {
            PersonInfo pi = SessionHelper.GetCurrentPersonInfo(db);
            return View(db.Educations.Where(x => x.PersonInfo.Id == pi.Id).OrderByDescending(x => x.StartYear).ToList());
        }

        // GET: Education/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Education edu = db.Educations.Find(id);
            if (edu == null)
            {
                return HttpNotFound();
            }
            return View(edu);
        }

        // GET: Education/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Education edu)
        {
            if (ModelState.IsValid)
            {
                if (edu.IsCurrent && edu.EndYear != null)
                {
                    ModelState.AddModelError("EndYear", "If you have an out-of-date date, you can not be working.");
                    return View(edu);
                }

                if (edu.EndYear != null && (edu.EndYear <= edu.StartYear))
                {
                    ModelState.AddModelError("EndYear", "End Year can not be smaller then Start Year.");
                    return View(edu);
                }

                if (edu.IsCurrent == false && edu.EndYear == null)
                {
                    ModelState.AddModelError("EndYear", "End Year and Is Current can not be empty.");
                    return View(edu);
                }

                PersonInfo pi = SessionHelper.GetCurrentPersonInfo(db);
                edu.PersonInfo = pi;
                
                db.Educations.Add(edu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(edu);
        }

        // GET: Education/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Education edu = db.Educations.Find(id);
            if (edu == null)
            {
                return HttpNotFound();
            }
            return View(edu);
        }


        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Education edu)
        {
            if (ModelState.IsValid)
            {
                if (edu.IsCurrent && edu.EndYear != null)
                {
                    ModelState.AddModelError("EndYear", "If you have an out-of-date date, you can not be working.");
                    return View(edu);
                }

                if (edu.EndYear != null && (edu.EndYear <= edu.StartYear))
                {
                    ModelState.AddModelError("EndYear", "End Date can not be smaller then Start Date.");
                    return View(edu);
                }

                if (edu.IsCurrent == false && edu.EndYear == null)
                {
                    ModelState.AddModelError("EndYear", "End Date and Is Current can not be empty.");
                    return View(edu);
                }

                db.Entry(edu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(edu);
        }

        // GET: Education/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Education edu = db.Educations.Find(id);
            if (edu == null)
            {
                return HttpNotFound();
            }
            return View(edu);
        }

        // POST: Education/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Education edu = db.Educations.Find(id);
            db.Educations.Remove(edu);
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