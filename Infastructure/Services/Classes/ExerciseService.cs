using DomainRoutineApp.Mappers.Interfaces;
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

namespace InfrastructureRoutineApp.Services.Classes
{
    public class ExerciseService : IExerciseService
    {
        private readonly IExerciseRepository _exerciseRepository = null;
        private readonly IUserService _userService = null;
        private readonly IDayService _dayService = null;
        private readonly IExerciseMapper _exerciseMapper = null;
        private readonly IExerciseCategoryService _exerciseCategoryService = null;
        private readonly IExerciseDetailMapper _exerciseDetailMapper = null;


        public ExerciseService(IExerciseRepository exerciseRepository, IUserService userService, IDayService dayService, IExerciseMapper exerciseMapper, IExerciseCategoryService exerciseCategoryService, IExerciseDetailMapper exerciseDetailMapper)
        {
            _exerciseRepository = exerciseRepository;
            _userService = userService;
            _dayService = dayService;
            _exerciseMapper = exerciseMapper;
            _exerciseCategoryService = exerciseCategoryService;
            _exerciseDetailMapper = exerciseDetailMapper;
        }


        public async Task AddExerciseToRoutineAsync(AddExerciseToRoutineRequestModel model)
        {
            var exercise = await _exerciseRepository.GetExerciseByIdAsync(new GetExerciseRequestModel { ExerciseId = model.ExerciseId });


            if (exercise.UserId == _userService.GetUserId())
            {
                exercise.IsInTheRoutine = true;
                exercise.Sets = model.Sets;

                foreach (int day in model.Days)
                {
                    var d = await _dayService.GetDayByIdAsync(new GetDayRequestModel { DayId = day });

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

        public async Task<List<(string, List<CreateRoutineExerciseResponseModel>)>> GetAllUserExercises()
        {
            var result = await _exerciseRepository.GetAllUserExercisesAsync(new GetAllExercisesRequestModel { UserId = _userService.GetUserId() });

            var list = _exerciseMapper.MapDomainToCreateRoutineExerciseResponse(result);

            return OrganizeExercisesDependendingOnTheirCategory(list);
        }

        private List<(string, List<CreateRoutineExerciseResponseModel>)> OrganizeExercisesDependendingOnTheirCategory(List<CreateRoutineExerciseResponseModel> exercises)
        {
            List<(string, List<CreateRoutineExerciseResponseModel>)> response = new()
            {
                ("Legs", new List<CreateRoutineExerciseResponseModel>()),
                ("Abs", new List<CreateRoutineExerciseResponseModel>()),
                ("Chest", new List<CreateRoutineExerciseResponseModel>()),
                ("Shoulders", new List<CreateRoutineExerciseResponseModel>()),
                ("Biceps", new List<CreateRoutineExerciseResponseModel>()),
                ("Triceps", new List<CreateRoutineExerciseResponseModel>()),
                ("Forearms", new List<CreateRoutineExerciseResponseModel>()),
                ("Back", new List<CreateRoutineExerciseResponseModel>()),
                ("Cardio", new List<CreateRoutineExerciseResponseModel>())
            };

            for (int i = 0; i < exercises.Count; i++)
            {
                switch (exercises[i].Category)
                {
                    case "Legs":
                        response[0].Item2.Add(exercises[i]);
                        break;

                    case "Abs":
                        response[1].Item2.Add(exercises[i]);
                        break;

                    case "Chest":
                        response[2].Item2.Add(exercises[i]);
                        break;

                    case "Shoulders":
                        response[3].Item2.Add(exercises[i]);
                        break;

                    case "Biceps":
                        response[4].Item2.Add(exercises[i]);
                        break;

                    case "Triceps":
                        response[5].Item2.Add(exercises[i]);
                        break;

                    case "Forearms":
                        response[6].Item2.Add(exercises[i]);
                        break;

                    case "Back":
                        response[7].Item2.Add(exercises[i]);
                        break;

                    default:
                        response[8].Item2.Add(exercises[i]);
                        break;
                }
            }


            return response;
        }

        public async Task<List<Exercise>> GetTodayExercisesAsync(GetTodayExercisesRequestModel model)
        {
            var result = await _exerciseRepository.GetTodayExercisesAsync(model);

            return result;
        }

        public async Task RemoveExerciseFromRoutineAsync(RemoveExerciseFromRoutineRequestModel model)
        {
            var exercise = await _exerciseRepository.GetExerciseByIdAsync(new GetExerciseRequestModel { ExerciseId = model.ExerciseId });

            if (exercise is not null)
            {
                exercise.Sets = 0;
                exercise.IsInTheRoutine = false;
                await _exerciseRepository.DeleteDaysToTrainAsync(new DeleteDayToTrainRequestModel { ExerciseId = exercise.Id });
                await _exerciseRepository.UpdateExerciseAsync(exercise);
            }

        }

        public async Task<Exercise> GetExerciseByIdAsync(GetExerciseRequestModel model)
        {
            return await _exerciseRepository.GetExerciseByIdAsync(model);
        }

        public async Task AddExerciseDetailAsync(ExerciseDoneRequestModel model)
        {
            Exercise exercise = await GetExerciseByIdAsync(new GetExerciseRequestModel { ExerciseId = model.ExerciseId });

            ExerciseDetail exerciseDetail = _exerciseDetailMapper.MapExerciseDoneRequestToDomain(model);

            exercise.ExerciseDetails.Add(exerciseDetail);

            await _exerciseRepository.UpdateExerciseAsync(exercise);
        }

        public async Task UpdateExerciseAsync(Exercise exercise)
        {
            await _exerciseRepository.UpdateExerciseAsync(exercise);
        }

        public async Task<List<CreateRoutineExerciseResponseModel>> GetUserExercisesByCategoryAsync(GetUserExercisesByCategoryRequestModel model)
        {
            model.UserId = _userService.GetUserId();
            //model.UserId = "12e0e176-1580-4833-8642-c45f715d36ce";
            var exercises = await _exerciseRepository.GetUserExerciseByCategoryAsync(model);

            return _exerciseMapper.MapDomainToCreateRoutineExerciseResponse(exercises);
        }
    }
}
