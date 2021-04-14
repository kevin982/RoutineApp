using DomainRoutineApp.Models.Entities;
using DomainRoutineApp.Models.Requests.Day;
using DomainRoutineApp.Models.Requests.Exercise;
using DomainRoutineApp.Models.Requests.ExerciseCategory;
using DomainRoutineApp.Models.Responses.Exercise;
using DomainRoutineApp.Repositores.Interfaces;
using DomainRoutineApp.Services.Interfaces;
using RoutineApp.Mappers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureRoutineApp.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly IExerciseRepository _exerciseRepository = null;
        private readonly IUserService _userService = null;
        private readonly IDayService _dayService = null;
        private readonly IExerciseMapper _exerciseMapper = null;
        private readonly IExerciseCategoryService _exerciseCategoryService = null;

        public ExerciseService(IExerciseRepository exerciseRepository, IUserService userService, IDayService dayService, IExerciseMapper exerciseMapper, IExerciseCategoryService exerciseCategoryService)
        {
            _exerciseRepository = exerciseRepository;
            _userService = userService;
            _dayService = dayService;
            _exerciseMapper = exerciseMapper;
            _exerciseCategoryService = exerciseCategoryService;
        }


        public async Task AddExerciseToRoutineAsync(AddExerciseToRoutineRequestModel model)
        {
            var exercise = await _exerciseRepository.GetExerciseByIdAsync(new GetExerciseRequestModel { ExerciseId = model.ExerciseId});


            if (exercise.UserId == _userService.GetUserId())
            {
                exercise.IsInTheRoutine = true;
                exercise.Sets = model.Sets;

                foreach (int day in model.Days)
                {
                    var d = await _dayService.GetDayByIdAsync(new GetDayRequestModel { DayId = day});

                    if (d is not null) exercise.DaysToTrain.Add(d);
                }
            }

            await _exerciseRepository.UpdateExerciseAsync(exercise);
        }

        public async Task CreateExerciseAsync(CreateExerciseRequestModel model)
        {
            Exercise exercise = _exerciseMapper.MapCreateExerciseToDomain(model);

            exercise.Category = await _exerciseCategoryService.GetExerciseByIdAsync(new GetExerciseCategoryByIdRequestModel { ExerciseCategoryId = model.Category });

            await _exerciseRepository.CreateExerciseAsync(exercise);
        }

        public async Task<List<CreateRoutineExerciseResponseModel>> GetAllUserExercises()
        {
            var result = await _exerciseRepository.GetAllUserExercisesAsync(new GetAllExercisesRequestModel { UserId = _userService.GetUserId()});

            return _exerciseMapper.MapDomainToCreateRoutineExerciseResponse(result);
        }
 
        public async Task<List<Exercise>> GetTodayExercisesAsync(GetTodayExercisesRequestModel model)
        {
            var result = await _exerciseRepository.GetTodayExercisesAsync(model);

            return result;
        }

        public async Task RemoveExerciseFromRoutineAsync(RemoveExerciseFromRoutineRequestModel model)
        {
            var exercise = await _exerciseRepository.GetExerciseByIdAsync(new GetExerciseRequestModel { ExerciseId = model.ExerciseId });

            if(exercise is not null)
            {
                exercise.IsInTheRoutine = false;
                exercise.DaysToTrain.Clear();
                await _exerciseRepository.UpdateExerciseAsync(exercise);
            }


        }

 
    }
}
