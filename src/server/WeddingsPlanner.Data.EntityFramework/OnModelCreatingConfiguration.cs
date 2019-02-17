using Microsoft.EntityFrameworkCore;
using WeddingsPlanner.Data.Entities;

namespace WeddingsPlanner.Data.EntityFramework
{
    internal static class OnModelCreatingConfiguration
    {
        internal static void ConfigureAgencyWeddingRelations(this ModelBuilder builder)
        {
            builder
                .Entity<Wedding>()
                .HasOne(wedding => wedding.Agency)
                .WithMany(agency => agency.OrganizedWeddings)
                .HasForeignKey(wedding => wedding.AgencyId);
        }

        internal static void ConfigurePresentInvitationRelations(this ModelBuilder builder)
        {
            builder.Entity<Present>()
                .HasKey(present => present.InvitationId);

            builder.Entity<Present>()
                .HasOne(present => present.Invitation)
                .WithOne(invitation => invitation.Present)
                .HasForeignKey<Invitation>(invitation => invitation.PresentId);

            builder.Entity<Invitation>()
                .HasOne(invitation => invitation.Present)
                .WithOne(present => present.Invitation)
                .HasForeignKey<Present>(present => present.InvitationId);
        }

        internal static void ConfigureWeddingInvitationRelations(this ModelBuilder builder)
        {
            builder.Entity<Invitation>()
                .HasOne(invitation => invitation.Wedding)
                .WithMany(wedding => wedding.Invitations)
                .HasForeignKey(invitation => invitation.WeddingId);
        }

        internal static void ConfigurePersonInvitationRelations(this ModelBuilder builder)
        {
            builder.Entity<Invitation>()
                .HasOne(invitation => invitation.Guest)
                .WithMany(person => person.Invitations)
                .HasForeignKey(invitation => invitation.GuestId);
        }

        internal static void ConfigureWeddingVenueRelations(this ModelBuilder builder)
        {
            builder.Entity<WeddingVenue>()
                .HasKey(weddingVenue => new {weddingVenue.VenueId, weddingVenue.WeddingId});

            builder.Entity<WeddingVenue>()
                .HasOne(weddingVenue => weddingVenue.Wedding)
                .WithMany(wedding => wedding.WeddingVenues)
                .HasForeignKey(weddingVenue => weddingVenue.WeddingId);

            builder.Entity<WeddingVenue>()
                .HasOne(weddingVenue => weddingVenue.Venue)
                .WithMany(venue => venue.WeddingVenues)
                .HasForeignKey(weddingVenue => weddingVenue.VenueId);
        }
    }
}