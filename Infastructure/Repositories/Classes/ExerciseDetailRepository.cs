using DomainRoutineApp.Models.Entities;
using DomainRoutineApp.Models.Requests.ExerciseDetail;
using DomainRoutineApp.Repositores.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureRoutineApp.Repositories.Classes
{
    public class ExerciseDetailRepository : IExerciseDetailRepository
    {
        private readonly RoutineContext _context = null;

        public ExerciseDetailRepository(RoutineContext context)
        {
            _context = context;
        }


        public async Task<int> GetExerciseSetsDoneTodayAsync(GetExerciseSetsDoneTodayRequestModel model)
        {
            var details =
               await _context.ExerciseDetails
               .AsNoTracking()
               .Where(ed => ed.ExerciseId == model.ExerciseId)  //&& ed.DayDone.ToShortDateString() == DateTime.Now.ToShortDateString())
               .ToListAsync();

            if (details.Count == 0 || details is null) return 0;

            for (int i = 0; i < details.Count; i++)
            {
                if (details[i].DayDone.ToShortDateString() != DateTime.Now.ToShortDateString())
                {
                    details.RemoveAt(i);
                }
            }


            return details.Count;
        }

        public async Task<int> CreateExerciseDetailAsync(ExerciseDetail exerciseDetail)
        {
            var exercise =  await _context.Exercises.Where(e => e.Id == exerciseDetail.ExerciseId).SingleOrDefaultAsync();

            exercise.ExerciseDetails.Add(exerciseDetail);

            await _context.SaveChangesAsync();

            return exerciseDetail.Id;
        }

        public async Task<ExerciseDetail> GetExerciseDetailByIdAsync(GetExerciseDetailByIdRequestModel model)
        {
            return await _context.ExerciseDetails
                .Where(ex => ex.Id == model.ExerciseDetailId)
                .FirstOrDefaultAsync();
        }
    }
}
