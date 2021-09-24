using Microsoft.EntityFrameworkCore;
using RoutineMS_Core.Dtos;
using RoutineMS_Core.Exceptions;
using RoutineMS_Core.Models.Entities;
using RoutineMS_Core.Repositories;
using RoutineMS_Infraestructure.Data;
using RoutineMS_Infraestructure.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RoutineMS_Infraestructure.Repositories
{
    public class RoutineRepository : Repository<Routine>, IRoutineRepository
    {
        public RoutineRepository(RoutineMsDbContext context) : base(context) { }

        public async Task<ExerciseToDoDto> GetExerciseToDoFromRoutineAsync(Guid userId)
        {
            try
            {
                int day = DateTime.UtcNow.GetDayOfWeek();

                var routinesForToday = await
                    _context
                    .Routines
                    .AsNoTrackingWithIdentityResolution()
                    .Include(r => r.Days).AsSplitQuery()
                    .Include(r => r.Exercise).ThenInclude(e => e.SetDetail)
                    .Where(r => (r.UserId == userId) && (r.Days.Any(d => d.Id == day)))
                    .OrderBy(r => r.Exercise.CategoryName)
                    .ToListAsync();

                if (routinesForToday is null || routinesForToday?.Count == 0) return null;

                Exercise exercise = null;

                int setsLeft = 0;

                foreach (var routine in routinesForToday)
                {
                    if (routine.Exercise.SetDetail is null || routine.Exercise?.SetDetail?.SetsCompleted < routine.Sets)
                    {
                        exercise = routine.Exercise;
                        setsLeft = (routine.Exercise.SetDetail is not null)? routine.Sets - routine.Exercise.SetDetail.SetsCompleted : routine.Sets;
                        break;
                    }
                }

                if (exercise is null) return null;

                return new ExerciseToDoDto()
                {
                    Id = exercise.Id,
                    ImageUrl = exercise.ImageUrl,
                    Name = exercise.Name,
                    SetsLeft = setsLeft
                };
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<bool> RemoveRoutineByExercise(Guid exerciseId)
        {
            try
            {
                Routine routine = await _context
                    .Routines
                    .Include(r => r.Exercise).ThenInclude(e => e.SetDetail)
                    .FirstOrDefaultAsync(r => r.ExerciseId == exerciseId);

                if (routine is null) throw new RoutineMSException("Invalid exercise to remove!") { StatusCode = 404};

                _context.Routines.Remove(routine);
                _context.Exercises.Remove(routine.Exercise);
                _context.SetsDetails.Remove(routine.Exercise.SetDetail);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
