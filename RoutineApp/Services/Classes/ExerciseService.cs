using Microsoft.EntityFrameworkCore;
using RoutineApp.Data;
using RoutineApp.Data.Entities;
using RoutineApp.Models;
using RoutineApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoutineApp.Services.Classes
{
    public class ExerciseService : IExerciseService
    {
        private readonly RoutineContext _context = null;
        private readonly IUserService _userService = null;

        public ExerciseService(RoutineContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<bool> CreateExerciseAsync(CreateExerciseModel model)
        {
            try
            {
                var id = _userService.GetUserId();

                var user = await _context.Users.FirstOrDefaultAsync(e => e.Id == id);

                user.Exercises.Add((Exercise)model.Clone());
                
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
 
        }


        public async Task<List<ExerciseCategory>> GetCategoriesAsync()
        {
            return await _context.ExerciseCategories.AsNoTracking().ToListAsync();
        } 

        public async Task<List<Exercise>> GetExercisesAsync()
        {
            var idUser = _userService.GetUserId();

            return await _context.Exercises.AsNoTracking().Include(e => e.Images).Where(e => e.UserId == idUser).ToListAsync();
        }

        public async Task<List<Exercise>> GetAllExercisesAsync()
        {
            return await _context.Exercises.AsNoTracking().ToListAsync();
        }

        public async Task AddExerciseToRoutineAsync(AddExerciseModel model)
        {
            var userId = _userService.GetUserId();

            var exercise = await _context.Exercises.FirstOrDefaultAsync(e => e.Id == model.ExerciseId);

            if (exercise.UserId == userId)
            {
                exercise.IsInTheRoutine = true;

                foreach (int day in model.Days)
                {
                    var d = await _context.Days.FirstOrDefaultAsync(d => d.Id == day);

                    if (d is not null) exercise.DaysToTrain.Add(d);
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveExerciseFromRoutineAsync(int id)
        {
            var userId = _userService.GetUserId();

            var exercise = await _context.Exercises.Include(e => e.DaysToTrain).FirstOrDefaultAsync(e => e.Id == id);

            if (exercise.UserId == userId)
            {
                exercise.IsInTheRoutine = false;

                exercise.DaysToTrain.Clear();

                await _context.SaveChangesAsync();
            }
        }
    }
}
