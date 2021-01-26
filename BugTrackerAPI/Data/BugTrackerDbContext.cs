using BugTrackerAPI.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerAPI.Data
{
    public class BugTrackerDbContext : IdentityDbContext<User, Role, int, 
        IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DbSet<Project> Projects { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<ProjectMember> ProjectMembers { get; set; }

        public DbSet<ProjectTicket> ProjectTickets { get; set;}

        public DbSet<UserAssignedTicket> UserAssignedTickets { get; set; }

        public BugTrackerDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            builder.Entity<Role>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(r => r.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            builder.Entity<ProjectMember>()
                .HasKey(pm => new { pm.UserId, pm.ProjectId });

            builder.Entity<ProjectMember>()
                .HasOne(pm => pm.User)
                .WithMany(u => u.ProjectMembers)
                .HasForeignKey(pm => pm.UserId)
                .IsRequired();

            builder.Entity<ProjectMember>()
                .HasOne(pm => pm.Project)
                .WithMany(p => p.ProjectMembers)
                .HasForeignKey(pm => pm.ProjectId)
                .IsRequired();

            builder.Entity<ProjectTicket>()
                .HasKey(pt => new { pt.ProjectId, pt.TicketId });

            builder.Entity<ProjectTicket>()
                .HasOne(pt => pt.Project)
                .WithMany(p => p.ProjectTickets)
                .HasForeignKey(pt => pt.ProjectId)
                .IsRequired();

            builder.Entity<ProjectTicket>()
                .HasOne(pt => pt.Ticket)
                .WithMany(t => t.ProjectTickets)
                .HasForeignKey(pt => pt.TicketId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.Entity<UserAssignedTicket>()
                .HasKey(uat => new { uat.TicketId, uat.UserId });

            builder.Entity<UserAssignedTicket>()
                .HasOne(uat => uat.Ticket)
                .WithMany(t => t.UserAssignedTickets)
                .HasForeignKey(uat => uat.TicketId)
                .IsRequired();

            builder.Entity<UserAssignedTicket>()
                .HasOne(uat => uat.User)
                .WithMany(t => t.UserAssignedTickets)
                .HasForeignKey(uat => uat.UserId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}
