using DomainRoutineApp.Models.Entities;
using DomainRoutineApp.Models.Requests.Exercise;
using DomainRoutineApp.Repositores.Interfaces;
using DomainRoutineApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using RoutineApp.Mappers.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureRoutineApp.Repositories.Classes
{
    public class ExerciseRepository : IExerciseRepository
    {

        private readonly RoutineContext _context = null;
        private readonly IUserService _userService = null;
        private readonly IExerciseMapper _exerciseMapper = null;
        private readonly IDayService _dayService = null;


        public ExerciseRepository(RoutineContext context, IUserService userService, IExerciseMapper exerciseMapper, IDayService dayService)
        {
            _context = context;
            _userService = userService;
            _exerciseMapper = exerciseMapper;
            _dayService = dayService;
        }

        public async Task<Exercise> GetExerciseByIdAsync(GetExerciseRequestModel model)
        {
            return await _context.Exercises
                .AsNoTrackingWithIdentityResolution()
                .Include(e => e.Category)
                .Include(e => e.Images).AsSplitQuery()
                .Include(e => e.ExerciseDetails).AsSplitQuery()
                .Include(e => e.User)
                .Include(e => e.DaysToTrain).AsSplitQuery()
                .Where(e => e.Id == model.ExerciseId)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateExerciseAsync(Exercise exercise)
        {
            _context.Entry(exercise).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task CreateExerciseAsync(Exercise exercise)
        {
            var id = _userService.GetUserId();

            var user = await _context.Users.FirstOrDefaultAsync(e => e.Id == id);

            user.Exercises.Add(exercise);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Exercise>> GetAllUserExercisesAsync(GetAllExercisesRequestModel model)
        {
            Stopwatch watch = new();

            watch.Start();

            var user = await _context.Users
                .AsNoTracking()
                .Include(u => u.Exercises).ThenInclude(e => e.Images)
                .Include(u => u.Exercises).ThenInclude(e => e.Category)
                .FirstOrDefaultAsync(u => u.Id == model.UserId);

            watch.Stop();

            var miliseconds = watch.ElapsedMilliseconds;

            return user.Exercises;
        }


        public async Task<List<Exercise>> GetTodayExercisesAsync(GetTodayExercisesRequestModel model)
        {
            int dayNumber = await _dayService.GetDayIdAsync();

            Stopwatch watch = new();

            watch.Start();

            var user = await _context.Users
                .AsNoTracking()
                .Where(u => u.Id == model.UserId)
                .Include(u => u.Exercises.Where(e => e.DaysToTrain.Contains(new Day { Id = dayNumber, DayName = DateTime.Now.DayOfWeek.ToString() })))
                .ThenInclude(e => e.Images)
                .FirstOrDefaultAsync();

            watch.Stop();

            var miliseconds = watch.ElapsedMilliseconds;


            return user.Exercises;
        }

        public async Task DeleteDaysToTrainAsync(DeleteDayToTrainRequestModel model)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM DayExercise where ExercisesId = {model.ExerciseId}");
        }

        public async Task<List<Exercise>> GetUserExerciseByCategoryAsync(GetUserExercisesByCategoryRequestModel model)
        {
            return await _context.Exercises
                .AsNoTrackingWithIdentityResolution()
                .Where(e => e.UserId == model.UserId && e.CategoryId == model.CategoryId)
                .Include(e => e.Images).AsSplitQuery()
                .Include(e => e.Category)
                .ToListAsync();
        }
    }
}
