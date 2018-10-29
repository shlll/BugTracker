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
            if (!context.Roles.Any(r => r.Name == "Submitter"))
            {
                roleManager.Create(new IdentityRole { Name = "Submitter" });
            }
            if (!context.Roles.Any(r => r.Name == "Developer"))
            {
                roleManager.Create(new IdentityRole { Name = "Developer" });
            }
            if (!context.Roles.Any(r => r.Name == "Project Manager"))
            {
                roleManager.Create(new IdentityRole { Name = "Project Manager" });
            }
            ApplicationUser adminUser;
            if (!context.Users.Any(p => p.UserName == "adminator@bugtracker.com"))
            {
                adminUser = new ApplicationUser();
                adminUser.UserName = "adminator@bugtracker.com";
                adminUser.Email = "aadminator@bugtracker.com";
                adminUser.Name = "Admin";
                adminUser.FinalName = "Admin";
                userManager.Create(adminUser, "789!Shl");
            }
            else
            {
                adminUser = context.Users.Where(p => p.UserName == "adminator@bugtracker.com").FirstOrDefault();
            }
            if (!userManager.IsInRole(adminUser.Id, "Admin"))
            {
                userManager.AddToRole(adminUser.Id, "Admin");
            }
            ApplicationUser submitUser;
             if (!context.Users.Any(p => p.UserName == "submitter@bugtracker.com"))
            {
                submitUser = new ApplicationUser();
                submitUser.UserName = "submitter@bugtracker.com";
                submitUser.Email = "submitter@bugtracker.com";
                submitUser.Name = "Submitter";
                submitUser.FinalName = "Submitter";
                userManager.Create(submitUser, "567!Shl");
            }
            else
            {
                submitUser = context.Users.Where(p => p.UserName == "submitter@bugtracker.com").FirstOrDefault();
            }
            if (!userManager.IsInRole(submitUser.Id, "Submitter"))
            {
                userManager.AddToRole(submitUser.Id, "Submitter");
            }
            ApplicationUser developUser;
            if (!context.Users.Any(p => p.UserName == "developer@bugtracker.com"))
            {
                developUser = new ApplicationUser();
                developUser.UserName = "developer@bugtracker.com";
                developUser.Email = "developer@bugtracker.com";
                developUser.Name = "Developer";
                developUser.FinalName = "Developer";
                userManager.Create(developUser, "235!Shl");
            }
            else
            {
                developUser = context.Users.Where(p => p.UserName == "developer@bugtracker.com").FirstOrDefault();
            }
            if (!userManager.IsInRole(developUser.Id, "Developer"))
            {
                userManager.AddToRole(developUser.Id, "Developer");
            }
            ApplicationUser projManagerUser;
            if (!context.Users.Any(p => p.UserName == "projectmanager@bugtracker.com"))
            {
                projManagerUser = new ApplicationUser();
                projManagerUser.UserName = "projectmanager@bugtracker.com";
                projManagerUser.Email = "projectmanager@bugtracker.com";
                projManagerUser.Name = "Project Manager";
                projManagerUser.FinalName = "Project Manager";
                userManager.Create(projManagerUser, "135!Shl");
            }
            else
            {
                projManagerUser = context.Users.Where(p => p.UserName == "projectmanager@bugtracker.com").FirstOrDefault();
            }
            if (!userManager.IsInRole(projManagerUser.Id, "Project Manager"))
            {
                userManager.AddToRole(projManagerUser.Id, "Project Manager");
            }
            context.PriorityOfTickets.AddOrUpdate(
                new PriorityOfTicket() { Id = 1, Name = "Very Low" },
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
