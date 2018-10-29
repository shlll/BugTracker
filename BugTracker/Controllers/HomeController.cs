using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
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
            var db = new ApplicationDbContext();
            db.TicketModels.Where(p => p.CreatingId == "1");
            user.UsersCreatedTheTickets.ToList();
            user.UsersAssignedTheTickets.ToList();
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(EmailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var body = "<p>Email From: <bold>{0}</bold> ({1})</p><p> Message:</p><p>{2}</p>";
                    var from = "MyBugTracker<mike.shenhanlin@gmail.com>";
                    model.Body = "This is a message from your bugtracker site. The name and the email of the contacting person is above.";

                    var email = new MailMessage(from,
                                ConfigurationManager.AppSettings["emailto"])
                    {
                        Subject = "BugTracker Contact Email",
                        Body = string.Format(body, model.FromName, model.FromEmail,
                                             model.Body),
                        IsBodyHtml = true
                    };
                    var svc = new PersonalEmailOfTheService();
                    await svc.SendAsync(email);
                    return View(new EmailModel());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await Task.FromResult(0);
                }
            }
            return View(model);
        }
        public ActionResult Contact()
        {
            EmailModel model = new EmailModel();
            return View(model);
        }
    }
}