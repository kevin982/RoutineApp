using Microsoft.EntityFrameworkCore;
using RoutineMS_Core.Exceptions;
using RoutineMS_Core.Models.Entities;
using RoutineMS_Core.Repositories;
using RoutineMS_Infraestructure.Data;
using System;
using System.Threading.Tasks;

namespace RoutineMS_Infraestructure.Repositories
{
    public class ExerciseRepository : Repository<Exercise>, IExerciseRepository
    {
        public ExerciseRepository(RoutineMsDbContext context) : base(context) { }

        public override async Task<Exercise> GetByIdAsync(Guid id)
        {
            try
            {
                Exercise exercise = await _context
                    .Exercises
                    .Include(e => e.SetDetail)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (exercise is null) throw new RoutineMSException($"The exercise with id {id} could not be found"){ StatusCode = 404};

                return exercise;
            } 
            catch (Exception)
            {
                throw;
            }
        }

    }
}
