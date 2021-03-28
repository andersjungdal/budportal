using Microsoft.EntityFrameworkCore;
using RawDatabaseModelling.Models;

namespace RawDatabaseModelling.Context
{
    public class RawDbContext:DbContext
    {
        public DbSet<Aktion> Aktions { get; set; }
        public DbSet<RawBid> RawBids { get; set; }

        public RawDbContext()
        {
        }

        public RawDbContext(DbContextOptions<RawDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(
                    "Data Source=LABTOP-HENRIK;Initial Catalog=PraktikTestRaw;Integrated Security=True");
            base.OnConfiguring(optionsBuilder);
        }
    }
}