using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BugTracker.Models;

namespace BugTracker.Controllers
{
    public class TicketModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TicketModels
        public ActionResult Index()
        {
            var ticketModels = db.TicketModels.Include(t => t.Project);
            return View(ticketModels.ToList());
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
                db.TicketModels.Add(ticketModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreatingId = new SelectList(db.Users, "Id", "Name",ticketModel.CreatingId);
            ViewBag.TicketPriorityId = new SelectList(db.PriorityOfTickets, "Id", "Name", ticketModel.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.StatusOfTickets, "Id", "Name", ticketModel.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TypeOfTickets, "Id", "Name", ticketModel.TicketTypeId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticketModel.ProjectId);
            return View(ticketModel);
        }

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
                db.Entry(ticketModel).State = EntityState.Modified;
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
