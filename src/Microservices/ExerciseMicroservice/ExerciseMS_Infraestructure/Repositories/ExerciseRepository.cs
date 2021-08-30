﻿using ExerciseMS_Core.Models.Entities;
using ExerciseMS_Core.Repositories;
using ExerciseMS_Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ExerciseMS_Infraestructure.Repositories
{
    public class ExerciseRepository : Repository<Exercise>, IExerciseRepository
    {
        public ExerciseRepository(ExerciseMsDbContext context) : base(context){}


        public async Task<IEnumerable<Exercise>> GetAllExercisesByCategoryAsync(Guid categoryId, Guid userId, int index, int size)
        {
            try
            {
                return await _context
              .Exercises
              .AsNoTrackingWithIdentityResolution()
              .Where(e => e.CategoryId == categoryId && e.UserId == userId)
              .Skip(index * size)
              .Take(size)
              .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }

          
        }

        public int GetExerciseCountByCategory(Guid categoryId, Guid userId)
        {
            try
            {
                return _context
                .Exercises
                .Where(e => e.CategoryId == categoryId && e.UserId == userId)
                .Count();
            }
            catch (Exception)
            {
                return 0;
            }
 
        }
    }
}