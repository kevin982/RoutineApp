using DomainRoutineApp.Mappers.Interfaces;
using DomainRoutineApp.Models.Entities;
using DomainRoutineApp.Models.Requests.Exercise;
using DomainRoutineApp.Models.Requests.ExerciseDetail;
using DomainRoutineApp.Repositores.Interfaces;
using DomainRoutineApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureRoutineApp.Services.Classes
{
    public class ExerciseDetailService : IExerciseDetailService
    {
        private readonly IExerciseDetailRepository _exerciseDetailRepository = null;
        private readonly IExerciseService _exerciseService = null;
        private readonly IExerciseDetailMapper _exerciseDetailMapper = null;

        public ExerciseDetailService(IExerciseDetailRepository exerciseDetailRepository, IExerciseDetailMapper exerciseDetailMapper, IExerciseService exerciseService)
        {
            _exerciseDetailRepository = exerciseDetailRepository;
            _exerciseDetailMapper = exerciseDetailMapper;
            _exerciseService = exerciseService;

        }

        public async Task<int> CreateExerciseDetailAsync(ExerciseDoneRequestModel model)
        {
            ExerciseDetail exerciseDetail = _exerciseDetailMapper.MapExerciseDoneRequestToDomain(model);

            exerciseDetail.Exercise = await _exerciseService.GetExerciseByIdAsync(new GetExerciseRequestModel { ExerciseId = model.ExerciseId});

            exerciseDetail.SetNumber = await _exerciseDetailRepository.GetExerciseSetsDoneTodayAsync(new GetExerciseSetsDoneTodayRequestModel { ExerciseId = model.ExerciseId }) + 1;

            return await _exerciseDetailRepository.CreateExerciseDetailAsync(exerciseDetail);

        }

        public async Task<ExerciseDetail> GetExerciseDetailByIdAsync(GetExerciseDetailByIdRequestModel model)
        {
            return await _exerciseDetailRepository.GetExerciseDetailByIdAsync(model);
        }

        public async Task<int> GetExerciseSetsDoneAsync(GetExerciseSetsDoneTodayRequestModel model)
        {
            return await _exerciseDetailRepository.GetExerciseSetsDoneTodayAsync(model);
        }
    }
}
