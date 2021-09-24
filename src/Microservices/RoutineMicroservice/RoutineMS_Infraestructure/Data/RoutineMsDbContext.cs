using Microsoft.EntityFrameworkCore;
using RoutineMS_Core.Models.Entities;

namespace RoutineMS_Infraestructure.Data
{
    public class RoutineMsDbContext : DbContext
    {
        public DbSet<Routine> Routines { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<SetDetail> SetsDetails { get; set; }

        public RoutineMsDbContext(DbContextOptions<RoutineMsDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Day>().HasData(new Day { Id = 1, Name = "Monday" });
            modelBuilder.Entity<Day>().HasData(new Day { Id = 2, Name = "Tuesday" });
            modelBuilder.Entity<Day>().HasData(new Day { Id = 3, Name = "Wednesday" });
            modelBuilder.Entity<Day>().HasData(new Day { Id = 4, Name = "Thursday" });
            modelBuilder.Entity<Day>().HasData(new Day { Id = 5, Name = "Friday" });
            modelBuilder.Entity<Day>().HasData(new Day { Id = 6, Name = "Saturday" });
            modelBuilder.Entity<Day>().HasData(new Day { Id = 7, Name = "Sunday" });
        }
    }
}
