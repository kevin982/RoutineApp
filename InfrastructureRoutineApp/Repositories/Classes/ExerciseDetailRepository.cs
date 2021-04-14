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
               .Where(ed => ed.ExerciseId == model.ExerciseId && ed.DayDone.ToShortDateString() == DateTime.Now.ToShortDateString())
               .ToListAsync();

            return details.Count;
        }
    }
}
