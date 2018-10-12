using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BugTracker.Models.Classes;

namespace BugTracker.Models
{
    public class TicketModel
    {
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
        public int CreatingId { get; set; }
        public virtual ApplicationUser Creating { get; set; }
        public int AssignedId { get; set; }
        public virtual ApplicationUser Assigned { get; set; }


    }
}