using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.Models
{
    public class AssignedOfTheTicketsModel
    {
        public int Id { get; set; }
        public MultiSelectList TicketList { get; set; }
        public string SelectedTicket { get; set; }
        public string Name { get; set; }
    }
}