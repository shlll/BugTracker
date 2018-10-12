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

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }
            if (!context.Roles.Any(r => r.Name == "Submitters"))
            {
                roleManager.Create(new IdentityRole { Name = "Submitters" });
            }
            if (!context.Roles.Any(r => r.Name == "Developers"))
            {
                roleManager.Create(new IdentityRole { Name = "Developers" });
            }
            if (!context.Roles.Any(r => r.Name == "Project Manager"))
            {
                roleManager.Create(new IdentityRole { Name = "Project Manager" });
            }


            ApplicationUser adminUser;
            if (!context.Users.Any(p => p.UserName == "admin@gmail.com"))
            {
                adminUser = new ApplicationUser();
                adminUser.UserName = "admin@gmail.com";
                adminUser.Email = "admin@gmail.com";
                adminUser.Name = "Admin";
                adminUser.FinalName = "Admin";
                userManager.Create(adminUser, "Password-1");
            }
            else
            {
                adminUser = context.Users.Where(p => p.UserName == "admin@gmail.com").FirstOrDefault();
            }
             if (!context.Users.Any(p => p.UserName == "shll20120727@163.com"))
            {
                adminUser = new ApplicationUser();
                adminUser.UserName = "shll20120727@163.com";
                adminUser.Email = "shll20120727@163.com";
                adminUser.Name = "Submitters";
                adminUser.FinalName = "Submitters";
                userManager.Create(adminUser, "Password-3");
            }
            else
            {
                adminUser = context.Users.Where(p => p.UserName == "shll20120727@163.com").FirstOrDefault();
            }
            if (!context.Users.Any(p => p.UserName == "shll20120727@163.com"))
            {
                adminUser = new ApplicationUser();
                adminUser.UserName = "shll20120727@163.com";
                adminUser.Email = "shll20120727@163.com";
                adminUser.Name = "Developers";
                adminUser.FinalName = "Developers";
                userManager.Create(adminUser, "Password-4");
            }
            else
            {
                adminUser = context.Users.Where(p => p.UserName == "shll20120727@163.com").FirstOrDefault();
            }
            if (!context.Users.Any(p => p.UserName == "shll20120727@163.com"))
            {
                adminUser = new ApplicationUser();
                adminUser.UserName = "shll20120727@163.com";
                adminUser.Email = "shll20120727@163.com";
                adminUser.Name = "Project Manager";
                adminUser.FinalName = "Project Manager";
                userManager.Create(adminUser, "Password-5");
            }
            else
            {
                adminUser = context.Users.Where(p => p.UserName == "shll20120727@163.com").FirstOrDefault();
            }


            if (!userManager.IsInRole(adminUser.Id, "Admin"))
            {
                userManager.AddToRole(adminUser.Id, "Admin");
            }
            if (!userManager.IsInRole(adminUser.Id, "Submitters"))
            {
                userManager.AddToRole(adminUser.Id, "Submitters");
            }
            if (!userManager.IsInRole(adminUser.Id, "Developers"))
            {
                userManager.AddToRole(adminUser.Id, "Developers");
            }
            if (!userManager.IsInRole(adminUser.Id, "Project Manager"))
            {
                userManager.AddToRole(adminUser.Id, "Project Manager");
            }


            if (!context.PriorityOfTickets.Any(p => p.Name == "shll20120727@163.com"))
            {
                adminUser = new ApplicationUser();
                adminUser.Id = "shll20120727@163.com";
                adminUser.Name = "shll20120727@163.com";
                userManager.Create(adminUser, "Password-5");
            }
            if (!context.StatusOfTickets.Any(p => p.Name == "shll20120727@163.com"))
            {
                adminUser = new ApplicationUser();
                adminUser.Id = "shll20120727@163.com";
                adminUser.Name = "shll20120727@163.com";
                userManager.Create(adminUser, "Password-5");
            }
            if (!context.TypeOfTickets.Any(p => p.Name == "shll20120727@163.com"))
            {
                adminUser = new ApplicationUser();
                adminUser.Id = "shll20120727@163.com";
                adminUser.Name = "shll20120727@163.com";
                userManager.Create(adminUser, "Password-5");
            }



            context.PriorityOfTickets.AddOrUpdate(
                new PriorityOfTicket() { Id = 2, Name = "Low" },
                new PriorityOfTicket() { Id = 3, Name = "Medium" },
                new PriorityOfTicket() { Id = 4, Name = "Urgent" }
                
            );
            context.SaveChanges();


            context.TypeOfTickets.AddOrUpdate(
               new TypeOfTicket() { Id = 1, Name = "Bug Fixes" },
               new TypeOfTicket() { Id = 2, Name = "Software Update" }
            );
            context.SaveChanges();

            context.StatusOfTickets.AddOrUpdate(
             new StatusOfTicket() { Id = 1, Name = "Not Started" },
             new StatusOfTicket() { Id = 2, Name = "Finished" },
             new StatusOfTicket() { Id = 3, Name = "On Hold" },
             new StatusOfTicket() { Id = 4, Name = "In Progress" }
          );
            context.SaveChanges();








        }
    }
}
