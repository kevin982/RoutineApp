using ExerciseMS_Core.Models.Entities;
using ExerciseMS_Core.Repositories;
using ExerciseMS_Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ExerciseMS_Core.Services;

namespace ExerciseMS_Infraestructure.Repositories
{
    public class ExerciseRepository : Repository<Exercise>, IExerciseRepository
    {
        private readonly IUserService _userService;

        public ExerciseRepository(ExerciseMsDbContext context, ILogger logger, IUserService userService) : base(context, logger)
        {
            _userService = userService;
        }


        public async Task<IEnumerable<Exercise>> GetAllExercisesByCategoryAsync(Guid categoryId, int index, int size)
        {
            try
            {
                Guid userId = new Guid(_userService.GetUserId());


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
                _logger.LogError($"Error while getting all exercises by category because of {ex.Message}");
                return null;
            }

          
        }

        public int? GetExerciseCountByCategory(Guid categoryId)
        {
            try
            {
                Guid userId = new Guid(_userService.GetUserId());
 
                return _context
                .Exercises
                .Where(e => e.CategoryId == categoryId && e.UserId == userId)
                .Count();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting the exercises count of a specific category because of {ex.Message}");
                return null;
            }
 
        }

        public override async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                Guid userId = new Guid(_userService.GetUserId());
 

                _context.
                    Remove(await _context
                    .Exercises
                    .Where(e => e.UserId == userId && e.ExerciseId == id)
                    .FirstOrDefaultAsync());

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while deleting the exercise because of {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateIsInTheRoutine(bool newValue, Guid exerciseId, Guid userId)
        {
            try
            {

                var exercise = await _context
                    .Exercises
                    .Where(e => e.ExerciseId == exerciseId && e.UserId == userId)
                    .FirstOrDefaultAsync();

                exercise.IsInTheRoutine = newValue;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while updating is in the routing prop because of {ex.Message}");
                return false;
            }
        }
    }
}
