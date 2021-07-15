using DomainRoutineApp.Models.Entities;
using DomainRoutineApp.Models.Requests.ExerciseCategory;
using DomainRoutineApp.Repositores.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureRoutineApp.Repositories.Classes
{
    public class ExerciseCategoryRepository : IExerciseCategoryRepository
    {
        private readonly RoutineContext _context = null;

        public ExerciseCategoryRepository(RoutineContext context)
        {
            _context = context;
        }

        public async Task<List<ExerciseCategory>> GetAllCategoriesAsync()
        {
            var result = await _context.ExerciseCategories.
                AsNoTracking()
                .ToListAsync();

            if (result is not null && result.Count  > 0) return result;

            await SeedCategories();

            return await GetAllCategoriesAsync();
        }

        private async Task SeedCategories()
        {

            await _context.ExerciseCategories.AddAsync(new ExerciseCategory { CategoryName = "Shoulders" });
            await _context.ExerciseCategories.AddAsync(new ExerciseCategory { CategoryName = "Triceps" });
            await _context.ExerciseCategories.AddAsync(new ExerciseCategory { CategoryName = "Biceps" });
            await _context.ExerciseCategories.AddAsync(new ExerciseCategory { CategoryName = "Forearms" });
            await _context.ExerciseCategories.AddAsync(new ExerciseCategory { CategoryName = "Chest" });
            await _context.ExerciseCategories.AddAsync(new ExerciseCategory { CategoryName = "Back" });
            await _context.ExerciseCategories.AddAsync(new ExerciseCategory { CategoryName = "Abs" });
            await _context.ExerciseCategories.AddAsync(new ExerciseCategory { CategoryName = "Legs" });
            await _context.ExerciseCategories.AddAsync(new ExerciseCategory { CategoryName = "Cardio" });

            await _context.SaveChangesAsync();

        }

        public async Task<ExerciseCategory> GetCategoryByIdAsync(GetExerciseCategoryByIdRequestModel model)
        {
            return await _context.ExerciseCategories
                .AsNoTrackingWithIdentityResolution()
                .Where(ec => ec.Id == model.ExerciseCategoryId)
                .FirstOrDefaultAsync();
        }
    }
}
