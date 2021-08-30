using ExerciseMS_Core.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_Infraestructure.Data
{
    public class ExerciseMsDbContext : DbContext
    {
        public DbSet<Exercise> Exercises { get; set; }

        public DbSet<Category> Categories { get; set; }

        public ExerciseMsDbContext(DbContextOptions<ExerciseMsDbContext> options):base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = Guid.NewGuid(),CategoryName = "Legs"});
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = Guid.NewGuid(),CategoryName = "Abs"});
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = Guid.NewGuid(),CategoryName = "Chest"});
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = Guid.NewGuid(),CategoryName = "Shoulders"});
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = Guid.NewGuid(),CategoryName = "Biceps"});
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = Guid.NewGuid(),CategoryName = "Triceps"});
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = Guid.NewGuid(),CategoryName = "Forearms"});
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = Guid.NewGuid(),CategoryName = "Back"});
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = Guid.NewGuid(),CategoryName = "Cardio"});
        }

    }
}
