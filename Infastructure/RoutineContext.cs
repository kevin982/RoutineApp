using DomainRoutineApp.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureRoutineApp
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

        public DbSet<Weight> Weights { get; set; }

    }
}
