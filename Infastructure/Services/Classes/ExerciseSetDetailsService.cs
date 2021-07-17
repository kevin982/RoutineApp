using DomainRoutineApp.Mappers.Interfaces;
 
using DomainRoutineApp.Models.Requests.Exercise;
using DomainRoutineApp.Models.Requests.ExerciseDetail;
using DomainRoutineApp.Repositores.Interfaces;
using DomainRoutineApp.Services.Interfaces;
using DomainRoutineLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureRoutineApp.Services.Classes
{
    public class ExerciseSetDetailsService : IExerciseSetDetailsService
    {
        private readonly IExerciseSetDetailsRepository _exerciseDetailRepository = null;
        private readonly IExerciseService _exerciseService = null;
        private readonly IExerciseDetailMapper _exerciseDetailMapper = null;

        public ExerciseSetDetailsService(IExerciseSetDetailsRepository exerciseDetailRepository, IExerciseDetailMapper exerciseDetailMapper, IExerciseService exerciseService)
        {
            _exerciseDetailRepository = exerciseDetailRepository;
            _exerciseDetailMapper = exerciseDetailMapper;
            _exerciseService = exerciseService;

        }

        public async Task<int> CreateExerciseSetDetailAsync(ExerciseDoneRequestModel model)
        {
            ExerciseSetDetail exerciseDetail = _exerciseDetailMapper.MapExerciseDoneRequestToDomain(model);

            exerciseDetail.Exercise = await _exerciseService.GetExerciseByIdAsync(new GetExerciseRequestModel { ExerciseId = model.ExerciseId});

            exerciseDetail.SetNumber = await _exerciseDetailRepository.GetExerciseSetsDoneTodayAsync(new GetExerciseSetsDoneTodayRequestModel { ExerciseId = model.ExerciseId }) + 1;

            return await _exerciseDetailRepository.CreateExerciseSetDetailAsync(exerciseDetail);

        }

        public async Task<ExerciseSetDetail> GetExerciseSetDetailsByIdAsync(GetExerciseDetailByIdRequestModel model)
        {
            return await _exerciseDetailRepository.GetExerciseSetDetailsByIdAsync(model);
        }

        public async Task<int> GetExerciseSetsDoneAsync(GetExerciseSetsDoneTodayRequestModel model)
        {
            return await _exerciseDetailRepository.GetExerciseSetsDoneTodayAsync(model);
        }
    }
}
