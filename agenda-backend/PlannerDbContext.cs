using agenda_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace agenda_backend
{
    public class PlannerDbContext : DbContext
    {
        public PlannerDbContext(DbContextOptions<PlannerDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>()
                .ToTable("task");

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Task> Tasks { get; set; }
    }
}
