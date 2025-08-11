using Microsoft.EntityFrameworkCore;
using VotingLibrary.Data.Entities;

namespace VotingLibrary.Data.Persistent.Ef
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Election> Elections { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Vote> Votes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
