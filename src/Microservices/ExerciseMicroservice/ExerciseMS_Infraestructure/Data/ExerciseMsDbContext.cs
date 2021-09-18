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

        public ExerciseMsDbContext(DbContextOptions<ExerciseMsDbContext> options):base(options){}

    }
}
