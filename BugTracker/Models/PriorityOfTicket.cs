using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class PriorityOfTicket
    {
        public int Id { get; set; }
      
        public string Name { get; set; }
        

        public ICollection<TicketModel> Ticket { get; set; }
    }
}