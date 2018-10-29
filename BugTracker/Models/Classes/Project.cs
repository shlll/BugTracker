using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BugTracker.Models.Classes
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Project()
        {
            Users = new HashSet<ApplicationUser>();
            Tickets = new HashSet<TicketModel>();
        }
        public virtual ICollection<ApplicationUser> Users { get; set; }
        public virtual ICollection<TicketModel> Tickets { get; set; }
    }
}