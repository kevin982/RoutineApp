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
            return await _context.ExerciseCategories.AsNoTracking().ToListAsync();
        }

        public async Task<ExerciseCategory> GetCategoryByIdAsync(GetExerciseCategoryByIdRequestModel model)
        {
            return await _context.ExerciseCategories
                .AsNoTracking()
                .Where(ec => ec.Id == model.ExerciseCategoryId)
                .FirstOrDefaultAsync();
        }
    }
}
