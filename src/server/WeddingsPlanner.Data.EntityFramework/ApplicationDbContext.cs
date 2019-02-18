using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WeddingsPlanner.Data.Entities;

namespace WeddingsPlanner.Data.EntityFramework
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base (options)
        {
        }

        public DbSet<Venue> Venues { get; set; }
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Wedding> Weddings { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<Present> Presents { get; set; }
        public DbSet<Gift> Gifts { get; set; }
        public DbSet<Cash> Cash { get; set; }
        public DbSet<WeddingVenue> WeddingVenues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureAgencyWeddingRelations();
            modelBuilder.ConfigurePresentInvitationRelations();
            modelBuilder.ConfigureWeddingInvitationRelations();
            modelBuilder.ConfigurePersonInvitationRelations();
            modelBuilder.ConfigureWeddingVenueRelations();
            modelBuilder.ConfigureWeddingPersonRelations();
            base.OnModelCreating(modelBuilder);
        }
    }
}
