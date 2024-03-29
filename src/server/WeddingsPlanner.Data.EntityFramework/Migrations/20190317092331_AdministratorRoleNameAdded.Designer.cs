﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WeddingsPlanner.Data.EntityFramework;

namespace WeddingsPlanner.Data.EntityFramework.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190317092331_AdministratorRoleNameAdded")]
    partial class AdministratorRoleNameAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("WeddingsPlanner.Data.Entities.Agency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EmployeesCount");

                    b.Property<string>("Name");

                    b.Property<string>("Town");

                    b.HasKey("Id");

                    b.ToTable("Agencies");
                });

            modelBuilder.Entity("WeddingsPlanner.Data.Entities.Invitation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Family");

                    b.Property<int>("GuestId");

                    b.Property<bool>("IsAttending");

                    b.Property<int>("PresentId");

                    b.Property<int>("WeddingId");

                    b.HasKey("Id");

                    b.HasIndex("GuestId");

                    b.HasIndex("WeddingId");

                    b.ToTable("Invitations");
                });

            modelBuilder.Entity("WeddingsPlanner.Data.Entities.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("Birthdate");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.Property<int>("Gender");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("MiddleNameInitial")
                        .IsRequired()
                        .HasMaxLength(1);

                    b.Property<string>("Phone");

                    b.HasKey("Id");

                    b.ToTable("People");
                });

            modelBuilder.Entity("WeddingsPlanner.Data.Entities.Present", b =>
                {
                    b.Property<int>("InvitationId");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.HasKey("InvitationId");

                    b.ToTable("Presents");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Present");
                });

            modelBuilder.Entity("WeddingsPlanner.Data.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<DateTime>("RegistrationDate");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("WeddingsPlanner.Data.Entities.Venue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Capacity");

                    b.Property<string>("Name");

                    b.Property<string>("Town");

                    b.HasKey("Id");

                    b.ToTable("Venues");
                });

            modelBuilder.Entity("WeddingsPlanner.Data.Entities.Wedding", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AgencyId");

                    b.Property<int?>("BrideId");

                    b.Property<int?>("BridegroomId");

                    b.Property<DateTime>("Date");

                    b.HasKey("Id");

                    b.HasIndex("AgencyId");

                    b.HasIndex("BrideId");

                    b.HasIndex("BridegroomId");

                    b.ToTable("Weddings");
                });

            modelBuilder.Entity("WeddingsPlanner.Data.Entities.WeddingVenue", b =>
                {
                    b.Property<int>("VenueId");

                    b.Property<int>("WeddingId");

                    b.Property<int>("Id");

                    b.HasKey("VenueId", "WeddingId");

                    b.HasAlternateKey("Id");

                    b.HasIndex("WeddingId");

                    b.ToTable("WeddingVenues");
                });

            modelBuilder.Entity("WeddingsPlanner.Data.Entities.Cash", b =>
                {
                    b.HasBaseType("WeddingsPlanner.Data.Entities.Present");

                    b.Property<decimal>("Amount");

                    b.ToTable("Cash");

                    b.HasDiscriminator().HasValue("Cash");
                });

            modelBuilder.Entity("WeddingsPlanner.Data.Entities.Gift", b =>
                {
                    b.HasBaseType("WeddingsPlanner.Data.Entities.Present");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Size");

                    b.ToTable("Gift");

                    b.HasDiscriminator().HasValue("Gift");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("WeddingsPlanner.Data.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WeddingsPlanner.Data.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WeddingsPlanner.Data.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("WeddingsPlanner.Data.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WeddingsPlanner.Data.Entities.Invitation", b =>
                {
                    b.HasOne("WeddingsPlanner.Data.Entities.Person", "Guest")
                        .WithMany("Invitations")
                        .HasForeignKey("GuestId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WeddingsPlanner.Data.Entities.Wedding", "Wedding")
                        .WithMany("Invitations")
                        .HasForeignKey("WeddingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WeddingsPlanner.Data.Entities.Present", b =>
                {
                    b.HasOne("WeddingsPlanner.Data.Entities.Invitation", "Invitation")
                        .WithOne("Present")
                        .HasForeignKey("WeddingsPlanner.Data.Entities.Present", "InvitationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WeddingsPlanner.Data.Entities.Wedding", b =>
                {
                    b.HasOne("WeddingsPlanner.Data.Entities.Agency", "Agency")
                        .WithMany("OrganizedWeddings")
                        .HasForeignKey("AgencyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WeddingsPlanner.Data.Entities.Person", "Bride")
                        .WithMany("Brides")
                        .HasForeignKey("BrideId");

                    b.HasOne("WeddingsPlanner.Data.Entities.Person", "Bridegroom")
                        .WithMany("Bridegrooms")
                        .HasForeignKey("BridegroomId");
                });

            modelBuilder.Entity("WeddingsPlanner.Data.Entities.WeddingVenue", b =>
                {
                    b.HasOne("WeddingsPlanner.Data.Entities.Venue", "Venue")
                        .WithMany("WeddingVenues")
                        .HasForeignKey("VenueId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WeddingsPlanner.Data.Entities.Wedding", "Wedding")
                        .WithMany("WeddingVenues")
                        .HasForeignKey("WeddingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
