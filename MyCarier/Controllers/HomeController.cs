using MyCarier.Classes;
using MyCarier.Filters;
using MyCarier.Models;
using MyCarier.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MyCarier.Controllers
{
    [ActFilter, ExcFilter]
    public class HomeController : Controller
    {
        DatabaseContext db = new DatabaseContext();


        public ActionResult HomePage()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                PersonInfo pi = new PersonInfo()
                {
                    Email = model.Email,
                    Name = model.Name,
                    Surname = model.Surname,
                    PhotoImageName = "profile.png"
                };

                db.PersonInfos.Add(pi);
                db.SaveChanges();

                Login login = new Login()
                {
                    Id = Guid.NewGuid(),
                    Email = model.Email,
                    Password = Crypto.SHA256(model.Password),
                    PersonInfo = pi
                };

                db.Logins.Add(login);
                db.SaveChanges();

                return RedirectToAction("SignIn");
            }

            return View(model);
        }

        [AuthFilter]
        public ActionResult Index(int? id)
        {
            if (id != null)
            {
                ViewBag.Person = db.PersonInfos.Find(id);
            }
            else
            {
                ViewBag.Person = SessionHelper.GetCurrentPersonInfo(db);
            }

            return View();
        }


        public ActionResult SecretIndex(int? id)
        {
            if (id != null)
            {
                ViewBag.Person = db.PersonInfos.Find(id);
            }
            else
            {
                ViewBag.Person = SessionHelper.GetCurrentPersonInfo(db);
            }

            return View("Index");
        }

        public ActionResult CreatePdf(int id, string param)
        {
            PersonInfo pi = db.PersonInfos.Find(id);

            PdfViewModel model = new PdfViewModel()
            {
                PersonInfo = pi,
                Param = param,
                Educations = db.Educations.Where(x => x.PersonInfo.Id == pi.Id).ToList(),
                WorkExperiences = db.WorkExpriences.Where(x => x.PersonInfo.Id == pi.Id).ToList(),
                Skills = db.Abilities.Where(x => x.PersonInfo.Id == pi.Id && x.Category == "skill").ToList(),
                Languages = db.Abilities.Where(x => x.PersonInfo.Id == pi.Id && x.Category == "lang").ToList(),
            };

            return View(model);
        }

        public ActionResult Template()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(Login model)
        {
            if (ModelState.IsValid)
            {
                string encoded = Crypto.SHA256(model.Password);

                Login login = db.Logins.FirstOrDefault(x =>
                    x.Email == model.Email && x.Password == encoded);

                if (login == null)
                {
                    ModelState.AddModelError("", "Username or password is invalid.");
                    return View(model);
                }

                Session["CurrentUser"] = login.Email;
                return RedirectToAction("Index", "Admin");
            }

            return View(model);
        }
    }
}