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
using ExerciseMS_Core.Exceptions;

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

            string id = _userService.GetUserId();

            if (string.IsNullOrEmpty(id)) throw new ExerciseMSException("The user is not authenticated.") { StatusCode = 401 };

            Guid userId = new Guid(id);

            return await _context
            .Exercises
            .AsNoTrackingWithIdentityResolution()
            .Where(e => e.CategoryId == categoryId && e.UserId == userId)
            .Skip(index * size)
            .Take(size)
            .ToListAsync();


        }

        public int GetExerciseCountByCategory(Guid categoryId)
        {

            string id = _userService.GetUserId();

            if (string.IsNullOrEmpty(id)) throw new ExerciseMSException("The user is not authenticated.") { StatusCode = 401 };

            Guid userId = new Guid(id);

            int count = _context
            .Exercises
            .Where(e => e.CategoryId == categoryId && e.UserId == userId)
            .Count();

            if (count == 0) throw new ExerciseMSException("There are not exercises with that category") { StatusCode = 404};

            return count;
        }

        public override async Task<Exercise> DeleteAsync(Guid exerciseId)
        {
            string id = _userService.GetUserId();

            if (string.IsNullOrEmpty(id)) throw new ExerciseMSException("The user is not authenticated.") { StatusCode = 401 };

            Guid userId = new Guid(id);

            Exercise exerciseToEliminate = await GetByIdAsync(exerciseId);

            if (exerciseToEliminate is null) throw new ExerciseMSException("The exercise has not been found!") { StatusCode = 404 };

            if(exerciseToEliminate.UserId != userId) throw new ExerciseMSException("The exercise has not been found!") { StatusCode = 404 };

            _context.Exercises.Remove(exerciseToEliminate);

            return exerciseToEliminate;
        }

        public async Task<bool> UpdateIsInTheRoutine(bool newValue, Guid exerciseId, Guid userId)
        {

            var exercise = await _context
                .Exercises
                .Where(e => e.ExerciseId == exerciseId && e.UserId == userId)
                .FirstOrDefaultAsync();

            exercise.IsInTheRoutine = newValue;

            return true;

        }
    }
}
