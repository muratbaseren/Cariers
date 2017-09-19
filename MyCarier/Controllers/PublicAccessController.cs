using MyCarier.Classes;
using MyCarier.Filters;
using MyCarier.Models;
using MyCarier.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyCarier.Controllers
{
    [ExcFilter, ActFilter]
    public class PublicAccessController : Controller
    {
        DatabaseContext db = new DatabaseContext();

       
        public ActionResult Show(Guid? id)
        {
            if (id == null)
            {
                Exception ex = new Exception("Adres bir erişim ID 'si içermiyor.");
                Session["LastError"] = ex;
                return RedirectToAction("Error", "Home");
            }

            SharedUrl item = db.SharedUrls.Find(id);

            if (item == null)
            {
                Exception ex = new Exception("Belirtilen erişim ID 'sine ait kayıt bulunamadı.");
                Session["LastError"] = ex;
                return RedirectToAction("Error", "Home");
            }

            if (DateTime.Now.Date < item.StartDate)
            {
                Exception ex = new Exception("Belirtilen erişim ID 'si henüz başlamamıştır.");
                Session["LastError"] = ex;
                return RedirectToAction("Error", "Home");
            }

            if (DateTime.Now.Date >= item.StartDate && DateTime.Now.Date <= item.EndDate)
            {
                return RedirectToAction("SecretIndex", "Home", new { id = item.PersonInfo.Id });
            }
            else
            {
                Exception ex = new Exception("Belirtilen erişim ID 'sinin süresi dolmuştur.");
                Session["LastError"] = ex;
                return RedirectToAction("Error", "Home");
            }
        }

        [AuthFilter]
        public ActionResult Index()
        {
            PersonInfo pi = SessionHelper.GetCurrentPersonInfo();
            return View(db.SharedUrls.Where(x => x.PersonInfo.Id == pi.Id).ToList());
        }

        [AuthFilter]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, AuthFilter]
        public ActionResult Create(PublicAccessViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.EndDate <= model.StartDate)
                {
                    ModelState.AddModelError("EndDate", "End Date can not be smaller then Start Date.");
                    return View(model);
                }

                SharedUrl sharedUrl = new SharedUrl()
                {
                    Id = Guid.NewGuid(),
                    CreatedOn = DateTime.Now,
                    StartDate = model.StartDate.Date,
                    EndDate = model.EndDate.Date,
                    PersonInfo = SessionHelper.GetCurrentPersonInfo(db)
                };

                db.SharedUrls.Add(sharedUrl);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [AuthFilter]
        public ActionResult Delete(Guid id)
        {
            SharedUrl item = db.SharedUrls.Find(id);
            db.SharedUrls.Remove(item);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}