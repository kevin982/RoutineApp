using Microsoft.EntityFrameworkCore;
using RoutineMS_Core.Dtos;
using RoutineMS_Core.Exceptions;
using RoutineMS_Core.Models.Entities;
using RoutineMS_Core.Repositories;
using RoutineMS_Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoutineMS_Infraestructure.Repositories
{
    public class RoutineRepository : Repository<Routine>, IRoutineRepository
    {
        public RoutineRepository(RoutineMsDbContext context) : base(context) { }

        public override async Task<Routine> GetByIdAsync(Guid id)
        {
            try
            {
                var routine = await _context.Routines.Include(r => r.Exercise).ThenInclude(e => e.SetDetail).Where(r => r.ExerciseId == id).FirstOrDefaultAsync();

                if (routine is null) throw new RoutineMSException($"Routine with id {id} could not be found") { StatusCode = 404};
                
                return routine;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ExerciseToDoDto> GetExerciseToDoFromRoutineAsync(Guid userId, int day)
        {
            try
            {
                var routinesForToday = await
                    _context
                    .Routines
                    .AsNoTrackingWithIdentityResolution()
                    .Include(r => r.Days).AsSplitQuery()
                    .Include(r => r.Exercise).ThenInclude(e => e.SetDetail)
                    .OrderBy(r => r.Exercise.CategoryName).ThenBy(r => r.Exercise.Name)
                    .ToListAsync();

                List<Routine> routines = new();

                foreach (var r in routinesForToday)
                {
                    if (!(r.UserId == userId)) continue;

                    if (!(r.Days.Any(d => d.Id == day))) continue;

                    routines.Add(r);
                }

                if(routines.Count == 0) return null;

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
 
        public async Task<bool> RemoveRoutineByExerciseAsync(Guid exerciseId, Guid userId)
        {
            try
            {
                Routine routine = await _context
                    .Routines
                    .Include(r => r.Exercise).ThenInclude(e => e.SetDetail)
                    .FirstOrDefaultAsync(r => r.ExerciseId == exerciseId && r.UserId == userId);

                if (routine is null) throw new RoutineMSException("Invalid exercise to remove!") { StatusCode = 404};

                _context.Routines.Remove(routine);
                if(routine.Exercise is not null)_context.Exercises.Remove(routine.Exercise);
                if (routine.Exercise.SetDetail is not null) _context.SetsDetails.Remove(routine.Exercise.SetDetail);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> TheExerciseIsInRoutineAsync(Guid exerciseId, Guid userId)
        {
            try
            {
                Routine routine = await _context
                    .Routines
                    .FirstOrDefaultAsync(r => r.ExerciseId == exerciseId && r.UserId == userId);

                return (routine is not null) ?true:false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
