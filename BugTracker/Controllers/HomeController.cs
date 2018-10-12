﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BugTracker.Models;

namespace BugTracker.Controllers
{
    //[Authorize(Roles ="Admin")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var user = new ApplicationUser();
            user.UsersCreatedTheTickets.ToList();
            user.UsersAssignedTheTickets.ToList();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}