using ExerciseMS_Core.Models.Entities;
using ExerciseMS_Core.Repositories;
using ExerciseMS_Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ExerciseMS_Core.Services;
using ExerciseMS_Core.Exceptions;
using ExerciseMS_Infraestructure.Extensions;

namespace ExerciseMS_Infraestructure.Repositories
{
    public class ExerciseRepository : Repository<Exercise>, IExerciseRepository
    {
        private readonly IUserService _userService;

        public ExerciseRepository(ExerciseMsDbContext context, IUserService userService) : base(context)
        {
            _userService = userService;
        }


        public async Task<IEnumerable<Exercise>> GetAllExercisesByCategoryAsync(Guid categoryId, int index, int size)
        {
            try
            {
                if (categoryId == Guid.Empty || size == 0) throw new ExerciseMSException("Bad request to get all the exercises by category") { StatusCode = 400 };

                Guid userId = new Guid(_userService.GetUserId());

                var exercises = await _context
                .Exercises
                .AsNoTracking()
                .Where(e => e.CategoryId == categoryId && e.UserId == userId)
                .OrderBy(e => e.ExerciseName)
                .Skip(index * size)
                .Take(size)
                .ToListAsync();

                if (exercises is null) throw new ExerciseMSException("The user does not have exercises with that category") {StatusCode = 404 };
                if (exercises.Count == 0) throw new ExerciseMSException("The user does not have exercises with that category") {StatusCode = 404 };

                return exercises;
            }   
            catch (Exception)
            {
                throw;
            }
        }

        public int GetIndexesCountAsync(Guid categoryId, int size)
        {
            try
            {
                if (categoryId == Guid.Empty) throw new ExerciseMSException("The category id can not be empty") { StatusCode = 400 };

                Guid userId = new Guid(_userService.GetUserId());

                int count = _context
                .Exercises
                .Where(e => e.CategoryId == categoryId && e.UserId == userId)
                .Count();

                if (count == 0) throw new ExerciseMSException("There are not exercises with that category") { StatusCode = 404 };

                decimal indexes = Math.Ceiling(count.ToDecimal() / size.ToDecimal());

                return (int)indexes;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public override async Task<Exercise> DeleteAsync(Guid exerciseId)
        {
            try
            {
                if (exerciseId == Guid.Empty) throw new ExerciseMSException("The exercise id to delete the exercise can not be empty") { StatusCode = 400 };

                Guid userId = new Guid(_userService.GetUserId());

                Exercise exerciseToEliminate = await GetByIdAsync(exerciseId);

                if (exerciseToEliminate.UserId != userId) throw new ExerciseMSException("The exercise has not been found!") { StatusCode = 404 };

                _context.Exercises.Remove(exerciseToEliminate);

                return exerciseToEliminate;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateIsInTheRoutine(bool newValue, Guid exerciseId, Guid userId)
        {
            try
            {
                if (exerciseId == Guid.Empty || userId == Guid.Empty) throw new ExerciseMSException("To update the exercise the user and exercise id must not be empty") {StatusCode = 400 };

                Exercise exerciseToUpdate = await GetByIdAsync(exerciseId);

                if (exerciseToUpdate.UserId != userId) throw new ExerciseMSException("The exercise has not been found!") { StatusCode = 404 };

                exerciseToUpdate.IsInTheRoutine = newValue;

                return true;
            }
            catch (Exception)
            {
                throw;
            }
 
        }
    }
}
