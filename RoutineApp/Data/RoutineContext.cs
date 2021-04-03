using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RoutineApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoutineApp.Data
{
    public class RoutineContext : IdentityDbContext
    {
        public RoutineContext(DbContextOptions<RoutineContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Exercise> Exercises { get; set; }

        public DbSet<ExerciseDetail> ExerciseDetails { get; set; }

        public DbSet<Image> Images { get; set; }
 

        public DbSet<ExerciseCategory> ExerciseCategories { get; set; }

        public DbSet<Day> Days { get; set; }

    }
}
