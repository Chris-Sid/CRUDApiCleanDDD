using Microsoft.EntityFrameworkCore;
using MyApiProject.Domain.Entities;
using MyApiProject.Domain.Entities.Activity;
using MyApiProject.Domain.Entities.Customers;
using MyApiProject.Domain.Entities.FollowUps;
using MyApiProject.Domain.Entities.Leads;
using MyApiProject.Infrastructure.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Customer> customers { get; set; }
        public DbSet<ContactPerson> contactpersons { get; set; }
        public DbSet<CommunicationLog> communicationlogs { get; set; }
        public DbSet<Lead> leads { get; set; }
        public DbSet<FollowUp> followups { get; set; }
        public DbSet<Note> notes { get; set; }
        public DbSet<Opportunity> opportunities { get; set; }
        public DbSet<ActivityLog> activitylogs { get; set; }
        public DbSet<Admin> Admins { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .HasColumnName("id") // Maps C# `Id` to DB column `id`
                      .IsRequired()
                      .ValueGeneratedNever();
            });

            // Customer → ContactPerson (One-to-Many)
            modelBuilder.Entity<Customer>()
                    .HasMany(c => c.contacts)
                    .WithOne(p => p.Customer)
                    .HasForeignKey(p => p.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.communicationlogs)
                .WithOne(cl => cl.Customer)
                .HasForeignKey(cl => cl.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.notes)
                .WithOne(n => n.RelatedCustomer)
                .HasForeignKey(n => n.RelatedCustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.opportunities)
                .WithOne(o => o.RelatedCustomer)
                .HasForeignKey(o => o.RelatedCustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Opportunity>()
                .HasMany(o => o.ActivityLogs)
                .WithOne(a => a.Opportunity)
                .HasForeignKey(a => a.OpportunityId)
                .OnDelete(DeleteBehavior.Cascade);

            // constraints for ContactPerson
            modelBuilder.Entity<ContactPerson>(entity =>
            {
                entity.HasKey(cp => cp.Id);
              //  entity.Property(cp => cp.Name).IsRequired();
              //  entity.Property(cp => cp.Email).IsRequired();
            });

            modelBuilder.Entity<CommunicationLog>()
                .HasOne(cl => cl.Customer)
                .WithMany(c => c.communicationlogs)
                .HasForeignKey(cl => cl.CustomerId);

            modelBuilder.Entity<Note>()
                .HasOne(n => n.RelatedCustomer)
                .WithMany(c => c.notes)
                .HasForeignKey(n => n.RelatedCustomerId);

            modelBuilder.Entity<Opportunity>()
                .HasOne(o => o.RelatedCustomer)
                .WithMany(c => c.opportunities)
                .HasForeignKey(o => o.RelatedCustomerId);
 
            modelBuilder.Entity<ActivityLog>()
                .HasOne(al => al.Opportunity)
                .WithMany(o => o.ActivityLogs)
                .HasForeignKey(al => al.OpportunityId);

            modelBuilder.Entity<FollowUp>()
                .HasOne(f => f.Lead)
                .WithMany(l => l.FollowUps)
                .HasForeignKey(f => f.LeadId);
 
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admins"); // Make sure this matches your actual DB table name

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username).IsRequired();
                entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Role).IsRequired();

                entity.HasData(new Admin
                {
                    Id = Guid.Parse("d3e2bff3-4cbf-42ab-bd8b-dc6fbd3ec7a1"),
                    Username = "admin",
                    PasswordHash = "$2a$11$vdDGr1eVpQWaHo2EYiCxUubB7yoftvlHr2CA3L3GvYxAo/Rh0IBrW",
                    Role = "Admin"
                });
            });

            /// <summary>
            /// Configures optimistic concurrency for every entity inheriting from
            /// ConcurrentEntity (e.g. Lead), mapping ConcurrencyToken to the
            /// database's native row-versioning mechanism.
            ///
            /// --- SQL SERVER APPROACH ---
            /// If this project targeted SQL Server, the [Timestamp] attribute on
            /// ConcurrentEntity.ConcurrencyToken alone would be sufficient — EF's
            /// SQL Server provider recognizes it as shorthand for:
            ///
            ///     entity.Property(e => e.ConcurrencyToken)
            ///           .IsRowVersion();
            ///
            /// which maps to a `rowversion` column, auto-generated and incremented
            /// by SQL Server on every write. No further Fluent API config needed.
            ///
            /// --- POSTGRESQL APPROACH (used here) ---
            /// Npgsql has no `rowversion` concept, so we instead bind
            /// ConcurrencyToken to PostgreSQL's hidden `xmin` system column, which
            /// the database already updates automatically on every row write due to
            /// its MVCC model (each UPDATE creates a new tuple whose `xmin` is the
            /// writing transaction's ID). This requires explicit configuration
            /// because `xmin` is a system column EF doesn't know about by default:
            ///
            ///   - HasColumnName("xmin")        -> bind to the real system column
            ///   - HasColumnType("xid")         -> use Postgres's native xid type
            ///   - ValueGeneratedOnAddOrUpdate()-> tell EF never to INSERT/UPDATE
            ///                                    this value itself; the database
            ///                                    always owns it
            ///   - IsConcurrencyToken()         -> include it in the WHERE clause of
            ///                                    generated UPDATE/DELETE statements,
            ///                                    so a stale value causes zero rows
            ///                                    to match and throws
            ///                                    DbUpdateConcurrencyException
            ///
            /// Applied generically (via reflection over ConcurrentEntity) so any
            /// new entity inheriting ConcurrentEntity automatically gets correct
            /// concurrency mapping without repeating this block per entity.
            /// </summary>
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ConcurrentEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property(nameof(ConcurrentEntity.ConcurrencyToken))
                        .HasColumnName("xmin")
                        .HasColumnType("xid")
                        .ValueGeneratedOnAddOrUpdate()
                        .IsConcurrencyToken();
                }
            }
        }
    }

}
