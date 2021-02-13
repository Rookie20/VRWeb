using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VRWeb.Models;

namespace VRWeb.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            //var kontakti = new ContactUsPage();
            //ViewBag.contactEmail = new SelectList(db.ContactUs, "contactEmail", "emri", kontakti.contactEmail);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact([Bind(Include = "contactName, contactEmail, contactPhone, contactMessage")] ContactUsPage contactUs)
        {
            if (User.Identity.IsAuthenticated)
            {
                ModelState["contactEmail"].Errors.Clear();
                var user = User.Identity.GetUserName();
                contactUs.contactEmail = user;

                ModelState["contactName"].Errors.Clear();
                string fullName = (from fn in db.Users
                                   where (fn.Email == user)
                                   select fn.FullName).Single();
                contactUs.contactName = fullName;
            }


            if (ModelState.IsValid)
            {
                db.ContactUs.Add(contactUs);
                db.SaveChanges();
                return RedirectToAction("Contact");
            }
            return View(contactUs);
        }

        public ActionResult OculusQuestService()
        {
            return View();
        }
        public ActionResult PlaystationVrService()
        {
            return View();
        }
        public ActionResult ValveIndexService()
        {
            return View();
        }

        public ActionResult VRGameDemo()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult PolygonShredder()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}