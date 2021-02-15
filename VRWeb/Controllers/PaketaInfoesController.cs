using Microsoft.AspNet.Identity;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using VRWeb.Models;

namespace VRWeb.Controllers
{
    public class PaketaInfoesController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: PaketaInfoes Buy
        [Authorize]
        public ActionResult Index(string kerkimi)
        {
            var user = User.Identity.GetUserName();
            if (User.IsInRole("Admin"))
            {

                if (!string.IsNullOrEmpty(kerkimi))
                {
                    var kontaktiAll = db.paketaInfos.Where(k => k.emriQytetit.Contains(kerkimi)
                    || k.emriShtetit.Contains(kerkimi) || k.ApplicationUser.Email.Contains(kerkimi)).ToList();
                    return View(kontaktiAll);
                }
                else
                {
                    var kontaktiAll = db.paketaInfos.ToList();
                    return View(kontaktiAll);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(kerkimi))
                {
                    var kontaktiAll = db.paketaInfos.Where(k => (k.emriQytetit.Contains(kerkimi)
                    || k.emriShtetit.Contains(kerkimi)) && k.ApplicationUser.Email == user).ToList();
                    return View(kontaktiAll);
                }
                else
                { 
                    var kontakti = db.paketaInfos.Where(e => e.ApplicationUser.Email.Contains(user)).ToList();
                    return View(kontakti);
                }
            }
        }

        // GET: PaketaInfoes/Details/5
        [Authorize]
        [PreventFromUrl]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaketaInfo paketaInfo = db.paketaInfos.Find(id);
            if (paketaInfo == null)
            {
                return HttpNotFound();
            }
            return View(paketaInfo);
        }

        // GET: PaketaInfoes/Create
        [Authorize]
        public ActionResult OculusQuest()
        {
            return View();
        }

        // POST: PaketaInfoes/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult OculusQuest([Bind(Include = "paketaID,emriShtetit,emriQytetit,zipCode,cmimi,regularPaketa,premiumPaketa")] PaketaInfo paketaInfo)
        {

            var currentUserID = User.Identity.GetUserId();
            var paketaUser = db.paketaInfos.FirstOrDefault(d => d.userKey == currentUserID);
            if (ModelState.IsValid)
            { 
                if(paketaInfo.regularPaketa == true)
                {
                    paketaInfo.cmimi = 299.00m;
                }
                else
                {
                    paketaInfo.cmimi = 399.00m;
                }
                paketaInfo.userKey = currentUserID;

                db.paketaInfos.Add(paketaInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paketaInfo);
        }

        [Authorize]
        public ActionResult ValveIndex()
        {
            return View();
        }

        // POST: PaketaInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult ValveIndex([Bind(Include = "paketaID,emriShtetit,emriQytetit,zipCode,cmimi,regularPaketa,premiumPaketa")] PaketaInfo paketaInfo)
        {
            
            var currentUserID = User.Identity.GetUserId();
            var paketaUser = db.paketaInfos.FirstOrDefault(d => d.userKey == currentUserID);
            if (ModelState.IsValid)
            {
                if (paketaInfo.regularPaketa == true)
                {
                    paketaInfo.cmimi = 999.00m;
                }
                else
                {
                    paketaInfo.cmimi = 1399.00m;
                }
                paketaInfo.userKey = currentUserID;
                db.paketaInfos.Add(paketaInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paketaInfo);
        }

        [Authorize]
        public ActionResult PlaystationVr()
        {
            return View();
        }

        // POST: PaketaInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult PlaystationVr([Bind(Include = "paketaID,emriShtetit,emriQytetit,zipCode,cmimi,regularPaketa,premiumPaketa")] PaketaInfo paketaInfo)
        {
            var currentUserID = User.Identity.GetUserId();
            var paketaUser = db.paketaInfos.FirstOrDefault(d => d.userKey == currentUserID);
            if (ModelState.IsValid)
            {
                paketaInfo.userKey = currentUserID;
                db.paketaInfos.Add(paketaInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paketaInfo);
        }

        // GET: PaketaInfoes/Delete/5
        //[Authorize]
        //[PreventFromUrl]
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    PaketaInfo paketaInfo = db.paketaInfos.Find(id);
        //    if (paketaInfo == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(paketaInfo);
        //}

        // POST: PaketaInfoes/Delete/5
        [Authorize]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult DeleteConfirmed(int id)
        {
            PaketaInfo paketaInfo = db.paketaInfos.Find(id);
            db.paketaInfos.Remove(paketaInfo);
            var rezultati  = db.SaveChanges();
            return Json(rezultati, JsonRequestBehavior.AllowGet);
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
