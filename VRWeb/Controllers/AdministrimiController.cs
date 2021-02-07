using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using VRWeb.Models;

namespace VRWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdministrimiController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public AdministrimiController() { }
        public AdministrimiController
       (ApplicationUserManager userManager,
       ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ??
             HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            { _userManager = value; }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ??
               HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }


        // GET: Administrimi
        
        public async Task<ActionResult> Index()
        {
            return View(await UserManager.Users.ToListAsync());
        }



        [PreventFromUrl]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                ApplicationUser user = context.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            //get User Data from Userid
            var user = await UserManager.FindByIdAsync(id);

            //List Logins associated with user
            var logins = user.Logins;

            //Gets list of Roles associated with current user
            var rolesForUser = await UserManager.GetRolesAsync(id);

            using (var transaction = context.Database.BeginTransaction())
            {

                //foreach(var paketa in context.paketaInfos.Where(k => k.userKey == id))
                //{
                //    context.paketaInfos.Remove(paketa);
                //    //context.SaveChanges();
                //}

                foreach (var login in logins.ToList())
                {
                    await UserManager.RemoveLoginAsync(login.UserId, new UserLoginInfo(login.LoginProvider, login.ProviderKey));
                }

                if (rolesForUser.Count() > 0)
                {
                    foreach (var item in rolesForUser.ToList())
                    {
                        // item should be the name of the role
                        var result = await UserManager.RemoveFromRoleAsync(user.Id, item);
                    }
                }

                //Delete User
                await UserManager.DeleteAsync(user);

                TempData["Message"] = "User Deleted Successfully. ";
                TempData["MessageValue"] = "1";
                //transaction.commit();
            }

            return RedirectToAction("Index", "Administrimi", new { area = "", });
        }






        [Authorize]
        [PreventFromUrl]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = context.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FullName,Email,Datelindja")] ApplicationUser user)
        {
            //bool isTrue;
            //if(user.UserName != user.Email)
            //{
            //    ViewBag.message = "Username and Email must be the same";
            //    isTrue = false;
            //}
            //else
            //{
            //    isTrue = true;
            //}
            if (/*isTrue == true &&*/ ModelState.IsValid)
            {
                user.UserName = user.Email;

                var passwordHash = (from k in context.Users
                                  where (k.Id == user.Id)
                                  select k.PasswordHash).FirstOrDefault();
                user.PasswordHash = passwordHash;

                var phoneNumber = (from k in context.Users
                                    where (k.Id == user.Id)
                                    select k.PhoneNumber).FirstOrDefault();
                user.PhoneNumber = phoneNumber;

                var security = (from k in context.Users
                                    where (k.Id == user.Id)
                                    select k.SecurityStamp).FirstOrDefault();
                user.SecurityStamp = security;

                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }
    }
}
