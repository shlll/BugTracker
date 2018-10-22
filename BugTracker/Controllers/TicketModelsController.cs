﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BugTracker.Helper;
using BugTracker.Models;
using Microsoft.AspNet.Identity;

namespace BugTracker.Controllers
{
    public class TicketModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private readonly RoleHelper RoleHelper = new RoleHelper();
        // GET: TicketModels  
        public ActionResult Index()
        {
            var ticketModels = db.TicketModels.
                Include(t => t.Project).
                Include(t => t.Assigned).
                Include(t => t.Creating).
                Include(t => t.TicketPriority).
                Include(t => t.TicketStatus).
                Include(t => t.TicketType);
            return View(ticketModels.ToList());
        }
        [Authorize(Roles = "Submitter")]
        public ActionResult SubmitterOfTheTickets()
        {
            var userId = User.Identity.GetUserId();

            var ticketModels = db.TicketModels.
                Where(p => p.CreatingId == userId).
                Include(t => t.Project).
                Include(t => t.TicketComments).
                Include(t => t.Assigned).
                Include(t => t.Creating).
                Include(t => t.TicketPriority).
                Include(t => t.TicketStatus).
                Include(t => t.TicketType);
            return View("Index", ticketModels.ToList());
        }
        [Authorize(Roles = "Developer")]
        public ActionResult DeveloperOfTheTickets()
        {
            var userId = User.Identity.GetUserId();

            var ticketModels = db.TicketModels.
                Where(p=>p.AssignedId == userId).
                Include(t => t.Project).
                Include(t => t.TicketComments).
                Include(t => t.Assigned).
                Include(t => t.Creating).
                Include(t => t.TicketPriority).
                Include(t => t.TicketStatus).
                Include(t => t.TicketType);

            return View("Index", ticketModels.ToList());
        }
        [Authorize(Roles = "Project Manager, Developer")]
        public ActionResult TheProjManagerTicketsAndTheDeveloperTickets()
        {
            var userId = User.Identity.GetUserId();
            var projectModel = db.Users.Where(p => p.Id == userId).Include(t => t.TicketComments).FirstOrDefault().
                Projects.Select(p=>p.Id).FirstOrDefault();
            return View("Index", db.TicketModels.Where(p => p.Id == projectModel).ToList());
        }
        
      
        // GET: TicketModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketModel ticketModel = db.TicketModels.Find(id);
            if (ticketModel == null)
            {
                return HttpNotFound();
            }
            return View(ticketModel);
        }

        // GET: TicketModels/Create
        public ActionResult Create()
        {
            ViewBag.AssignedId = new SelectList(db.Users, "Id", "Name");
            ViewBag.CreatingId = new SelectList(db.Users, "Id", "Name");
            ViewBag.TicketPriorityId = new SelectList(db.PriorityOfTickets, "Id", "Name");
            ViewBag.TicketStatusId = new SelectList(db.StatusOfTickets, "Id", "Name");
            ViewBag.TicketTypeId = new SelectList(db.TypeOfTickets, "Id", "Name");
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name");
            return View();
        }

        // POST: TicketModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,Name,Created,Updated,TicketPriorityId,TicketStatusId,TicketTypeId,ProjectId,CreatingId")] TicketModel ticketModel)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                ticketModel.CreatingId = userId;
                ticketModel.Created = DateTimeOffset.Now;
                ticketModel.TicketStatusId = 2;
                db.TicketModels.Add(ticketModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssignedId = new SelectList(db.Users, "Id", "Name", ticketModel.AssignedId);
            ViewBag.CreatingId = new SelectList(db.Users, "Id", "Name",ticketModel.CreatingId);
            ViewBag.TicketPriorityId = new SelectList(db.PriorityOfTickets, "Id", "Name", ticketModel.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.StatusOfTickets, "Id", "Name", ticketModel.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TypeOfTickets, "Id", "Name", ticketModel.TicketTypeId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticketModel.ProjectId);
            return View(ticketModel);
        }
        [HttpPost]
        public ActionResult CreateComment(int id, string body)
        {
            var ticketModels = db.TicketModels.Where(p => p.Id == id).FirstOrDefault();
            if(ticketModels == null)
            {
                return HttpNotFound();
            }
            if (string.IsNullOrWhiteSpace(body))
            {
                TempData["ErrorMessage"] = "Comment is required";
                return RedirectToAction("Details", new { id });
            }
            var comments = new TicketCommentsModel();
            comments.UserId = User.Identity.GetUserId();
            comments.Created = DateTime.Now;
            comments.TicketId = ticketModels.Id;
            comments.Comment = body;
            db.TicketCommentsModels.Add(comments);
            db.SaveChanges();
            return RedirectToAction("Details", new { id });
        }
        [HttpPost]
        public ActionResult CreateAttachments(int id, HttpPostedFileBase img, string content)
        {
            var attachments = new TicketAttachmentsModel();
            var attachmentsModel = db.TicketModels.Where(p => p.Id == id).FirstOrDefault();
            if (ModelState.IsValid)
            {
            if(attachmentsModel == null)
            {
                return HttpNotFound();
            }
            if (ImageUploadValidator.IsWebFriendlyImage(img))
            {
                img.SaveAs(Path.Combine(Server.MapPath("~/Uploads/"), Path.GetFileName(img.FileName)));
                attachments.FilePath = "/Uploads/ " + Path.GetFileName(img.FileName);
            }
                attachments.UserId = User.Identity.GetUserId();
                attachments.TicketId = attachmentsModel.Id;
                attachments.Created = DateTime.Now;
                attachments.Description = content;
            
            db.TicketAttachmentsModels.Add(attachments);
            db.SaveChanges();
            return RedirectToAction("Details", new { id });
            }
            return View(attachments);
        }
        //public ActionResult CreateNotification(int id)
        //{
        //    var notifications = new TicketNotificationsModel();
        //    var notificationModel = db.TicketModels.Where().

        //}
        // GET: TicketModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketModel ticketModel = db.TicketModels.Find(id);
            if (ticketModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreatingId = new SelectList(db.Users, "Id", "Name", ticketModel.CreatingId);
            ViewBag.TicketPriorityId = new SelectList(db.PriorityOfTickets, "Id", "Name", ticketModel.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.StatusOfTickets, "Id", "Name", ticketModel.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TypeOfTickets, "Id", "Name", ticketModel.TicketTypeId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticketModel.ProjectId);
            return View(ticketModel);
        }

        // POST: TicketModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,Name,Created,Updated,TicketPriorityId,TicketStatusId,TicketTypeId,ProjectId,CreatingId")] TicketModel ticketModel)
        {
            if (ModelState.IsValid)
            {
                var dateChanged = DateTimeOffset.Now;
                var changes = new List<TicketHistoriesModel>();
                var ticketModels = db.TicketModels.Where(p => p.Id == ticketModel.Id).FirstOrDefault();
                
                ticketModels.Updated = dateChanged;
                ticketModels.Name = ticketModel.Name;

                ticketModels.Description = ticketModel.Description;
                var originalValue = db.Entry(ticketModels).OriginalValues;
                var currentValue = db.Entry(ticketModels).CurrentValues;
                foreach(var property in originalValue.PropertyNames)
                {
                    var originalVal = originalValue[property]?.ToString(); 
                    var currentVal = currentValue[property]?.ToString();
                    if(originalVal != currentVal)
                    {
                        var history = new TicketHistoriesModel();
                        history.Changed = dateChanged;
                        history.NewValue = GetTheValuesFromTheKey(property,currentVal);
                        history.OldValue = GetTheValuesFromTheKey(property,originalVal);
                        history.Property = property;
                        history.TicketId = ticketModel.Id;
                        history.UserId = User.Identity.GetUserId();
                        changes.Add(history);
                    }
                }
                db.TicketHistoriesModels.AddRange(changes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreatingId = new SelectList(db.Users, "Id", "Name", ticketModel.CreatingId);
            ViewBag.TicketPriorityId = new SelectList(db.PriorityOfTickets, "Id", "Name", ticketModel.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.StatusOfTickets, "Id", "Name", ticketModel.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TypeOfTickets, "Id", "Name", ticketModel.TicketTypeId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticketModel.ProjectId);
            return View(ticketModel);
        }
        private string GetTheValuesFromTheKey(string propName, string key)
        {
            if(propName == "TicketTypeId")
            {
                return db.TypeOfTickets.Find(Convert.ToInt32(key)).Name;
            }
            if(propName == "TicketStatusId")
            {
                return db.StatusOfTickets.Find(Convert.ToInt32(key)).Name;
            }
            if (propName == "TicketPriorityId")
            {
                return db.PriorityOfTickets.Find(Convert.ToInt32(key)).Name;
            }
            return key;
        }

        // GET: TicketModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketModel ticketModel = db.TicketModels.Find(id);
            if (ticketModel == null)
            {
                return HttpNotFound();
            }
            return View(ticketModel);
        }

        // POST: TicketModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TicketModel ticketModel = db.TicketModels.Find(id);
            db.TicketModels.Remove(ticketModel);
            db.SaveChanges();
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
