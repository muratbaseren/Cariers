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
    public class WorkExperiencesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: WorkExpriences
        public ActionResult Index()
        {
            PersonInfo pi = SessionHelper.GetCurrentPersonInfo(db);
            
            return View(db.WorkExpriences.Where(x => x.PersonInfo.Id == pi.Id).OrderByDescending(x => x.StartDate).ToList());
        }

        // GET: WorkExpriences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkExprience workExprience = db.WorkExpriences.Find(id);
            if (workExprience == null)
            {
                return HttpNotFound();
            }
            return View(workExprience);
        }

        // GET: WorkExpriences/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(WorkExprience workExprience)
        {
            if (ModelState.IsValid)
            {
                if (workExprience.IsCurrent && workExprience.EndDate != null)
                {
                    ModelState.AddModelError("EndDate", "If you have an out-of-date date, you can not be working.");
                    return View(workExprience);
                }

                if (workExprience.EndDate != null && (workExprience.EndDate <= workExprience.StartDate))
                {
                    ModelState.AddModelError("EndDate", "End Date can not be smaller then Start Date.");
                    return View(workExprience);
                }

                if (workExprience.IsCurrent == false && workExprience.EndDate == null)
                {
                    ModelState.AddModelError("EndDate", "End Date and Is Current can not be empty.");
                    return View(workExprience);
                }

                PersonInfo pi = SessionHelper.GetCurrentPersonInfo(db);
                workExprience.PersonInfo = pi;

                db.WorkExpriences.Add(workExprience);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(workExprience);
        }

        // GET: WorkExpriences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkExprience workExprience = db.WorkExpriences.Find(id);
            if (workExprience == null)
            {
                return HttpNotFound();
            }
            return View(workExprience);
        }


        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WorkExprience workExprience)
        {
            if (ModelState.IsValid)
            {
                if (workExprience.IsCurrent && workExprience.EndDate != null)
                {
                    ModelState.AddModelError("EndDate", "If you have an out-of-date date, you can not be working.");
                    return View(workExprience);
                }

                if (workExprience.EndDate != null && (workExprience.EndDate <= workExprience.StartDate))
                {
                    ModelState.AddModelError("EndDate", "End Date can not be smaller then Start Date.");
                    return View(workExprience);
                }

                if (workExprience.IsCurrent == false && workExprience.EndDate == null)
                {
                    ModelState.AddModelError("EndDate", "End Date and Is Current can not be empty.");
                    return View(workExprience);
                }

                db.Entry(workExprience).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(workExprience);
        }

        // GET: WorkExpriences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkExprience workExprience = db.WorkExpriences.Find(id);
            if (workExprience == null)
            {
                return HttpNotFound();
            }
            return View(workExprience);
        }

        // POST: WorkExpriences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkExprience workExprience = db.WorkExpriences.Find(id);
            db.WorkExpriences.Remove(workExprience);
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
