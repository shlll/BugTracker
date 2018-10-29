using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BugTracker.Models.Classes;

namespace BugTracker.Models
{
    public class TicketModel
    {
        public TicketModel()
        {
            Users = new HashSet<ApplicationUser>();
            TicketAttachments = new HashSet<TicketAttachmentsModel>();
            TicketComments = new HashSet<TicketCommentsModel>();
            TicketHistories = new HashSet<TicketHistoriesModel>();
        }
        public int Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }
        public int TicketPriorityId { get; set; }
        public virtual PriorityOfTicket TicketPriority { get; set; }
        public int TicketStatusId { get; set; }
        public virtual StatusOfTicket TicketStatus { get; set; }
        public int TicketTypeId { get; set; }
        public virtual TypeOfTicket TicketType { get; set; } 
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public string CreatingId { get; set; }
        public virtual ApplicationUser Creating { get; set; }
        public string AssignedId { get; set; }
        public virtual ApplicationUser Assigned { get; set; }
        public virtual ICollection<TicketAttachmentsModel> TicketAttachments { get; set; }
        public virtual ICollection<TicketCommentsModel> TicketComments { get; set; }
        public virtual ICollection<TicketHistoriesModel> TicketHistories { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}