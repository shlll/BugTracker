using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using BugTracker.Models.Classes;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BugTracker.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    { 
        public string Name { get; set; }
        public ApplicationUser()
        {
            Users = new HashSet<ApplicationUser>();
            Projects = new HashSet<Project>();
            UsersCreatedTheTickets = new HashSet<TicketModel>();
            UsersAssignedTheTickets = new HashSet<TicketModel>();
            TicketAttachments = new HashSet<TicketAttachmentsModel>();
            TicketComments = new HashSet<TicketCommentsModel>();
            TicketHistories = new HashSet<TicketHistoriesModel>();
        }
        public string FinalName { get; set; }
        public ICollection<Project> Projects { get; set; }
        [InverseProperty("Creating")]
        public ICollection<TicketModel> UsersCreatedTheTickets { get; set; }
        [InverseProperty("Assigned")]
        public ICollection<TicketModel> UsersAssignedTheTickets { get; set; }
        public virtual ICollection<TicketAttachmentsModel> TicketAttachments { get; set; }
        public virtual ICollection<TicketCommentsModel> TicketComments { get; set; }
        public virtual ICollection<TicketHistoriesModel> TicketHistories { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public System.Data.Entity.DbSet<BugTracker.Models.Classes.Project> Projects { get; set; }
        public System.Data.Entity.DbSet<BugTracker.Models.TicketModel> TicketModels { get; set; }
        public System.Data.Entity.DbSet<BugTracker.Models.PriorityOfTicket> PriorityOfTickets { get; set; }
        public System.Data.Entity.DbSet<BugTracker.Models.StatusOfTicket> StatusOfTickets { get; set; }
        public System.Data.Entity.DbSet<BugTracker.Models.TypeOfTicket> TypeOfTickets { get; set; }
        public System.Data.Entity.DbSet<BugTracker.Models.TicketCommentsModel> TicketCommentsModels { get; set; }
        public System.Data.Entity.DbSet<BugTracker.Models.TicketAttachmentsModel> TicketAttachmentsModels { get; set; }
        public System.Data.Entity.DbSet<BugTracker.Models.TicketHistoriesModel> TicketHistoriesModels { get; set; }
    }
}