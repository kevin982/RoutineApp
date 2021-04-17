using DomainRoutineApp.Models.Requests.ExerciseDetail;
using DomainRoutineApp.Repositores.Interfaces;
using DomainRoutineApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureRoutineApp.Services
{
    public class ExerciseDetailService : IExerciseDetailService
    {
        private IExerciseDetailRepository _exerciseDetailRepository = null;

        public ExerciseDetailService(IExerciseDetailRepository exerciseDetailRepository)
        {
            _exerciseDetailRepository = exerciseDetailRepository;
        }

        public async Task<int> GetExerciseSetsDoneAsync(GetExerciseSetsDoneTodayRequestModel model)
        {
            return await _exerciseDetailRepository.GetExerciseSetsDoneTodayAsync(model);
        }
    }
}
