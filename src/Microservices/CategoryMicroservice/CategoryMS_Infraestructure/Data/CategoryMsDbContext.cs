using CategoryMS_Core.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace CategoryMS_Infraestructure.Data
{
    public class CategoryMsDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public CategoryMsDbContext(DbContextOptions<CategoryMsDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = Guid.NewGuid(), CategoryName = "Legs" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = Guid.NewGuid(), CategoryName = "Abs" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = Guid.NewGuid(), CategoryName = "Chest" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = Guid.NewGuid(), CategoryName = "Shoulders" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = Guid.NewGuid(), CategoryName = "Biceps" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = Guid.NewGuid(), CategoryName = "Triceps" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = Guid.NewGuid(), CategoryName = "Forearms" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = Guid.NewGuid(), CategoryName = "Back" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = Guid.NewGuid(), CategoryName = "Cardio" });
        }

    }
}
