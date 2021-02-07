using System;
using System.Data.Entity.Migrations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using VRWeb.Models;

namespace VRWeb.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<VRWeb.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(VRWeb.Models.ApplicationDbContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            const string name = "admin@vrweb.com";
            const string password = "Admin123!";
            const string roleName = "Admin";
            const string fullName = "Admin";
            //Create Role Admin if it does not exist
            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role);
            }

            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = name,
                    Email = name,
                    FullName = fullName,
                    Datelindja = new DateTime(2015, 1, 1)
                };
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
            }

            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id,
                role.Name);
            }
             //Create users role
             const string userRoleName = "Users";
             role = roleManager.FindByName(userRoleName);
            if (role == null)
            {
                role = new IdentityRole(userRoleName);
                var roleresult = roleManager.Create(role);
            }

            }
    }
}
