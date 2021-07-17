using DomainRoutineLibrary;
using DomainRoutineLibrary.Entities;
using DomainRoutineLibrary.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModifyRoutineMicroservice.Repository
{
    public class ModifyRoutineRepository : IModifyRoutineRepository
    {

        private readonly RoutineContext _context = null;

        public ModifyRoutineRepository(RoutineContext context)
        {
            _context = context;
        }

        public async Task<Exercise> GetExerciseByIdAsync(int exerciseId, string userId)
        {
            var exercise = await _context.Exercises
                .Include(e => e.Category)
                .Include(e => e.ExerciseSetDetails).AsSplitQuery()
                .Include(e => e.DaysToTrain).AsSplitQuery()
                .Where(e => e.Id == exerciseId && e.UserId == userId)
                .FirstOrDefaultAsync();

            if (exercise is not null) return exercise;

            throw new ApiException
            {
                Error = "The exercise does not exist.",
                HttpCode = 404,
                Microservice = nameof(ModifyRoutineMicroservice),
                Class = nameof(ModifyRoutineRepository),
                Method = nameof(GetExerciseByIdAsync)
            };



        }

        public async Task AddDayToExercise(int exerciseId, int dayId)
        {
            await _context
                .DayExercises
                .AddAsync(new DayExercise { DayId = dayId, ExerciseId = exerciseId });

            await _context.SaveChangesAsync();
        }

        public async Task<Day> GetDayByIdAsync(int dayId)
        {
            return await _context
                .Days
                .AsNoTracking()
                .Where(d => d.Id == dayId)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateExerciseAsync(Exercise ex)
        {
 
            _context.Entry(ex).State = EntityState.Modified;

            await _context.SaveChangesAsync();

        }



    }
}
