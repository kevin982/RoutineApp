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
            modelBuilder.Entity<Category>().HasData(new Category { CategoryName = "Legs"});
            modelBuilder.Entity<Category>().HasData(new Category { CategoryName = "Abs"});
            modelBuilder.Entity<Category>().HasData(new Category { CategoryName = "Chest"});
            modelBuilder.Entity<Category>().HasData(new Category { CategoryName = "Shoulders"});
            modelBuilder.Entity<Category>().HasData(new Category { CategoryName = "Biceps"});
            modelBuilder.Entity<Category>().HasData(new Category { CategoryName = "Triceps"});
            modelBuilder.Entity<Category>().HasData(new Category { CategoryName = "Forearms"});
            modelBuilder.Entity<Category>().HasData(new Category { CategoryName = "Back"});
            modelBuilder.Entity<Category>().HasData(new Category { CategoryName = "Cardio"});
        }

    }
}
