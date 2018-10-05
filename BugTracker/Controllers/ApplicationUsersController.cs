﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BugTracker.Helper;
using BugTracker.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BugTracker.Controllers
{
    [Authorize]
    public class ApplicationUsersController : Controller
    {

        // GET: ApplicationUsers
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ApplicationUsers
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }
        public ActionResult ChangeRole(string id)
        {
            var model = new UserRoleViewModel();
            var helper = new RoleHelper();
            model.Id = id;
            model.Name = User.Identity.Name;
            var roles = helper.GetAllRoles();
            var userRoles = helper.GetUserRoles(id);

            model.Roles = new MultiSelectList(roles, "Name", "Name", userRoles);
            return View(model);
        }
        [HttpPost]
        public ActionResult ChangeRole(UserRoleViewModel model)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = userManager.FindById(model.Id);
            var userRoles = userManager.GetRoles(model.Id);
            foreach (var role in userRoles)
            {
                userManager.RemoveFromRole(model.Id, role);
            }
            foreach (var role in model.SelectedRoles)
            {
                userManager.AddToRole(model.Id, role);
            }
            return RedirectToAction("Index");
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
