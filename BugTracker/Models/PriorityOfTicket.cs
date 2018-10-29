using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class PriorityOfTicket
    {
        public PriorityOfTicket()
        {
            Ticket = new HashSet<TicketModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<TicketModel> Ticket { get; set; }
    }
}