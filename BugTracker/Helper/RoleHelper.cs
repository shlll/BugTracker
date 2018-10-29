using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using BugTracker.Models;
using System.Web;
using System;

namespace BugTracker.Helper
{
    public class RoleHelper
    {
        private ApplicationDbContext Db;
        private UserManager<ApplicationUser> UserManager;
        private RoleManager<IdentityRole> RoleManager;
        public RoleHelper()
        {
            Db = new ApplicationDbContext();
            UserManager = new
            UserManager<ApplicationUser>(new UserStore<ApplicationUser>(Db));
            RoleManager = new
            RoleManager<IdentityRole>(new RoleStore<IdentityRole>(Db));
        }
        public List<IdentityRole> GetAllRoles()
        {
            return RoleManager.Roles.ToList();
        }
        public List<string> GetUserRoles(string id)
        {
            return UserManager.GetRoles(id).ToList();
        }
    }
}