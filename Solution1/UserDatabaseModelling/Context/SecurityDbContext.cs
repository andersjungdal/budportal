using System;
using System.Collections.Generic;
using System.Text;
using DatabaseModelling.DbModels;
using DatabaseModelling.DbModels.LockData.ProductionPlanLock;
using DatabaseModelling.DbModels.LockData.RawBidLock;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ModelsInterfaces;

namespace DatabaseModelling.Context
{
    public class SecurityDbContext : IdentityDbContext<User, IdentityRole<Guid>,Guid>
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<RawBid> RawBids { get; set; }
        public DbSet<ProductionPlan> ProductionPlans { get; set; }
        public DbSet<XmlTemplate> XmlTemplates { get; set; }
        public DbSet<ProductionPlanCell> ProductionPlanCells { get; set; }
        public DbSet<ProductionPlanColumn> ProductionPlanColumns { get; set; }
        public DbSet<RawBidCell> RawBidCells { get; set; }
        public DbSet<RawBidColumn> RawBidColumns { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<Roaden> Roads { get; set; }
        public SecurityDbContext()
        {
        }
        public SecurityDbContext(DbContextOptions<SecurityDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(
                    "Data Source=LAPTOP-U3V1724K;Initial Catalog=PraktikTestRaw;Integrated Security=True");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //Foreign keys
            builder.Entity<RawBidCell>()
                .HasOne(p => p.rawBidColumn)
                .WithMany(b => b.Rows)
                .HasForeignKey(p => p.RawBidColumnId);
            builder.Entity<ProductionPlanCell>()
                .HasOne(p => p.ProductionPlanColumn)
                .WithMany(b => b.Rows)
                .HasForeignKey(p => p.ColumnId);
            //Composite id's
            builder.Entity<RawBidCell>()
                .HasKey(x => new {x.RawBidColumnId, x.Index});
            builder.Entity<ProductionPlanCell>()
                .HasKey(x => new {x.ColumnId, x.Index});

        }
    }
}
