using Microsoft.EntityFrameworkCore;
using Back.Models;

namespace Back
{
    public class AppDBContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<TimeInterval> TimeIntervals { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Master> Masters { get; set; }

        public AppDBContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseNpgsql("Host=localhost;Port=5432;Database=pchmi;Username=postgres;Password=123qwe");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}