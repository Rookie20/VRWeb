using System.Linq;
using System.Web.Mvc;
using VRWeb.Models;
using Microsoft.AspNet.Identity;
using System.Net;
using System.Data.Entity;

namespace VRWeb.Controllers
{
    public class FaqeKontaktController : Controller
    {
        private ApplicationDbContext _dbContext;

            public FaqeKontaktController()
        {
            _dbContext = new ApplicationDbContext();
        }
        // GET: FaqeKontakt

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
        }

        //Advances search
        [Authorize]
        public ActionResult Index(string kerkimi)
        {
            var user = User.Identity.GetUserName();
            if (User.IsInRole("Admin"))
            {
                if (!string.IsNullOrEmpty(kerkimi)) 
                {
                    var kontaktiAll = _dbContext.ContactUs.Where(k => k.contactEmail.Contains(kerkimi) || k.contactName.Contains(kerkimi) || k.contactMessage.Contains(kerkimi));
                    return View(kontaktiAll);
                }
                else 
                { 
                    var kontaktiAll = _dbContext.ContactUs.ToList();
                    return View(kontaktiAll);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(kerkimi))
                {
                    var kontaktiAll = _dbContext.ContactUs.Where(k => k.contactMessage.Contains(kerkimi) && k.contactEmail == user);
                    return View(kontaktiAll);
                }
                else
                {
                    var kontakti = _dbContext.ContactUs.Where(e => e.contactEmail.Contains(user)).ToList();
                    return View(kontakti);
                }
            }

        }

        [Authorize]
        [PreventFromUrl]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactUsPage kontakte = _dbContext.ContactUs.Find(id);
            if (kontakte == null)
            {
                return HttpNotFound();
            }
            return View(kontakte);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "contactID,contactName,contactEmail,contactPhone,contactMessage")] ContactUsPage kontakte)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(kontakte).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kontakte);
        }




        //[Authorize]
        //[PreventFromUrl]
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    else { 
        //        ContactUsPage kontakt = _dbContext.ContactUs.Find(id);
        //        if (kontakt == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        return View(kontakt);
        //    }
        //}

        [Authorize]
        [HttpPost]
        [Route("FaqeKontakt/DeleteConfirmed/{id}")]
        //[ValidateAntiForgeryToken]
        public JsonResult DeleteConfirmed(int id)
        {
            ContactUsPage kontakt = _dbContext.ContactUs.Find(id); ;
            _dbContext.ContactUs.Remove(kontakt);
            var rezultati = _dbContext.SaveChanges();
            return Json(rezultati, JsonRequestBehavior.AllowGet);
        }

    }
}