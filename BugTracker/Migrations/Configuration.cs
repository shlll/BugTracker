namespace BugTracker.Migrations
{
    using BugTracker.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BugTracker.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "BugTracker.Models.ApplicationDbContext";
        }

        protected override void Seed(BugTracker.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!context.Roles.Any(r => r.Name == "Mike"))
            {
                roleManager.Create(new IdentityRole { Name = "Mike" });
            }
            if (!context.Roles.Any(r => r.Name == "Moderator"))
            {
                roleManager.Create(new IdentityRole { Name = "Monderator" });
            }
            if (!context.Roles.Any(r => r.Name == "Staff"))
            {
                roleManager.Create(new IdentityRole { Name = "Staff" });
            }
            if (!context.Roles.Any(r => r.Name == "TeamWork"))
            {
                roleManager.Create(new IdentityRole { Name = "TeamWork" });
            }
            if (!context.Roles.Any(r => r.Name == "Panel"))
            {
                roleManager.Create(new IdentityRole { Name = "Panel" });
            }

            ApplicationUser adminUser;
            if (!context.Users.Any(p => p.UserName == "mike.shenhanlin@gmail.com"))
            {
                adminUser = new ApplicationUser();
                adminUser.UserName = "mike.shenhanlin@gmail.com";
                adminUser.Email = "mike.shenhanlin@gmail.com";
                adminUser.Name = "Mike";
                adminUser.FinalName = "Mike";
                userManager.Create(adminUser, "Password-1");
            }
            else if (!context.Users.Any(p => p.UserName == "shll20120727@163.com"))
            {
                adminUser = new ApplicationUser();
                adminUser.UserName = "shll20120727@163.com";
                adminUser.Email = "shll20120727@163.com";
                adminUser.Name = "shlll";
                adminUser.FinalName = "shlll";
                userManager.Create(adminUser, "Password-2");
            }
            else
            {
                adminUser = context.Users.Where(p => p.UserName == "mike.shenhanlin@gmail.com").FirstOrDefault();
            }


            if (!userManager.IsInRole(adminUser.Id, "Mike"))
            {
                userManager.AddToRole(adminUser.Id, "Mike");
            }
        }
    }
}
