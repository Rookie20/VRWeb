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

        // GET: PaketaInfoes
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
        public ActionResult Italy()
        {
            return View();
        }

        // POST: PaketaInfoes/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Italy([Bind(Include = "paketaID,emriShtetit,emriQytetit,numriDiteve,vendqendrimiHotelYje,cmimi,regularPaketa,premiumPaketa")] PaketaInfo paketaInfo)
        {
            bool selectedlloji;
            bool selectedQyteti;
            if(paketaInfo.regularPaketa == false && paketaInfo.premiumPaketa == false) {
                selectedlloji = false;
                ViewBag.message = "Zgjidhni llojin e paketes";
            }
            else{
                selectedlloji = true;
            }
            if (paketaInfo.emriQytetit != "Milano" && paketaInfo.emriQytetit != "Venice" && paketaInfo.emriQytetit != "Rome") {
                selectedQyteti = false;
                ViewBag.qyteti = "Qytetet qe ofrojme jane Milano, Venice, Rome";
            }
            else{
                selectedQyteti = true;
            }
            var currentUserID = User.Identity.GetUserId();
            var paketaUser = db.paketaInfos.FirstOrDefault(d => d.userKey == currentUserID);
            if (selectedQyteti == true && selectedlloji == true && ModelState.IsValid)
            { 
                paketaInfo.userKey = currentUserID;
                paketaInfo.emriShtetit = "Italy";
                if(paketaInfo.regularPaketa == true) { 
                    paketaInfo.vendqendrimiHotelYje = 4;
                    paketaInfo.cmimi = 124.99m;
                }
                else {
                    paketaInfo.vendqendrimiHotelYje = 5;
                    paketaInfo.cmimi = 549.99m;
                }
                db.paketaInfos.Add(paketaInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paketaInfo);
        }

        [Authorize]
        public ActionResult Germany()
        {
            return View();
        }

        // POST: PaketaInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Germany([Bind(Include = "paketaID,emriShtetit,emriQytetit,numriDiteve,vendqendrimiHotelYje,cmimi,regularPaketa,premiumPaketa")] PaketaInfo paketaInfo)
        {
            bool selectedlloji;
            bool selectedQyteti;
            if (paketaInfo.regularPaketa == false && paketaInfo.premiumPaketa == false) {
                selectedlloji = false;
                ViewBag.message = "Zgjidhni llojin e paketes";
            }
            else {
                selectedlloji = true;
            }
            if (paketaInfo.emriQytetit != "Berlin" && paketaInfo.emriQytetit != "Munich" && paketaInfo.emriQytetit != "Frankfurt"){
                selectedQyteti = false;
                ViewBag.qyteti = "Qytetet qe ofrojme jane Berlin, Munich, Frankfurt";
            }
            else{
                selectedQyteti = true;
            }
            var currentUserID = User.Identity.GetUserId();
            var paketaUser = db.paketaInfos.FirstOrDefault(d => d.userKey == currentUserID);
            if (selectedQyteti == true && selectedlloji == true && ModelState.IsValid)
            {
                paketaInfo.userKey = currentUserID;
                paketaInfo.emriShtetit = "Germany";
                if (paketaInfo.regularPaketa == true){ 
                    paketaInfo.vendqendrimiHotelYje = 3;
                    paketaInfo.cmimi = 99.99m;
                }
                else {
                    paketaInfo.vendqendrimiHotelYje = 4;
                    paketaInfo.cmimi = 264.99m;
                }
                db.paketaInfos.Add(paketaInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paketaInfo);
        }

        [Authorize]
        public ActionResult France()
        {
            return View();
        }

        // POST: PaketaInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult France([Bind(Include = "paketaID,emriShtetit,emriQytetit,numriDiteve,vendqendrimiHotelYje,cmimi,regularPaketa,premiumPaketa")] PaketaInfo paketaInfo)
        {
            bool selectedlloji;
            bool selectedQyteti;
            if (paketaInfo.regularPaketa == false && paketaInfo.premiumPaketa == false) {
                selectedlloji = false;
                ViewBag.message = "Zgjidhni llojin e paketes";
            }
            else {
                selectedlloji = true;
            }
            if (paketaInfo.emriQytetit != "Paris" && paketaInfo.emriQytetit != "Nice" && paketaInfo.emriQytetit != "Lyon"){
                selectedQyteti = false;
                ViewBag.qyteti = "Qytetet qe ofrojme jane Paris, Nice, Lyon";
            }
            else{
                selectedQyteti = true;
            }
            var currentUserID = User.Identity.GetUserId();
            var paketaUser = db.paketaInfos.FirstOrDefault(d => d.userKey == currentUserID);
            if (selectedQyteti == true && selectedlloji == true && ModelState.IsValid)
            {
                paketaInfo.userKey = currentUserID;
                paketaInfo.emriShtetit = "France";
                if(paketaInfo.regularPaketa == true) {
                    paketaInfo.vendqendrimiHotelYje = 4;
                    paketaInfo.cmimi = 279.99m;
                }
                else{
                    paketaInfo.vendqendrimiHotelYje = 5;
                    paketaInfo.cmimi = 484.99m;
                }
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
