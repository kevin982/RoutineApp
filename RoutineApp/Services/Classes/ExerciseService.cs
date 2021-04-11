using Microsoft.EntityFrameworkCore;
using RoutineApp.Data;
using RoutineApp.Data.Entities;
using RoutineApp.Models;
using RoutineApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RoutineApp.Services.Classes
{
    public class ExerciseService : IExerciseService
    {
        private readonly RoutineContext _context = null;
        private readonly IUserService _userService = null;
        private readonly IDayService _dayService = null;

        public ExerciseService(RoutineContext context, IUserService userService, IDayService dayService)
        {
            _context = context;
            _userService = userService;
            _dayService = dayService;
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
            Stopwatch watch = new();

            watch.Start();

            //var idUser = _userService.GetUserId();

            //var user = await _context.Users
            //    .Where(u => u.Id == idUser)
            //    .Include(u => u.Exercises).ThenInclude(e => e.Category)
            //    .ToListAsync();

            var result = await _context.Images.ToListAsync();



            watch.Stop();

            var miliseconds = watch.ElapsedMilliseconds;

 
            
           return new List<Exercise>();
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

        public async Task<List<Exercise>> GetExercisesForTodayAsync(string id)
        {

            int dayNumber = await _dayService.GetDayIdAsync();

            Stopwatch watch = new();
            
            watch.Start();

            var user = await _context.Users
                .AsNoTracking()
                .Where(u => u.Id == id)
                .Include(u => u.Exercises.Where(e => e.DaysToTrain.Contains(new Day {Id = dayNumber, DayName = DateTime.Now.DayOfWeek.ToString()})))
                .ThenInclude(e=> e.Images)
                .FirstOrDefaultAsync();

            watch.Stop();

            var miliseconds = watch.ElapsedMilliseconds;
 
 
            return user.Exercises;

        }

        public async Task<int> GetExerciseSetsDoneToday(int id)
        {

            var details =
                await _context.ExerciseDetails
                .AsNoTracking()
                .Where(ed => ed.ExerciseId == id && ed.DayDone.ToShortDateString() == DateTime.Now.ToShortDateString())
                .ToListAsync();

            return details.Count;
        }
    }
}
