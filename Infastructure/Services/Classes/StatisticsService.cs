using DomainRoutineApp.Mappers.Interfaces;
 
using DomainRoutineApp.Models.Requests.Exercise;
using DomainRoutineApp.Models.Requests.Statics;
using DomainRoutineApp.Models.Responses.Statics;
using DomainRoutineApp.Repositores.Interfaces;
using DomainRoutineApp.Services.Interfaces;
using DomainRoutineLibrary.Entities;
using InfrastructureRoutineApp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InfrastructureRoutineApp.Services.Classes
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IStatisticsRepository _statisticsRepository = null;
        private readonly IUserService _userService = null;
        private readonly IExerciseService _exerciseService = null;

        public StatisticsService(IStatisticsRepository statisticsRepository, IUserService userService, IExerciseService exerciseService)
        {
            _statisticsRepository = statisticsRepository;
            _userService = userService;
            _exerciseService = exerciseService;
        }

        public async Task AddWeightAsync(AddPersonWeightRequestModel model)
        {
            model.UserId = _userService.GetUserId();

            await _statisticsRepository.AddWeightAsync(model);
        }

        public async Task<ExerciseStatisticsResponseModel> GetExerciseStatisticsAsync(GetExerciseStatisticsRequestModel model)
        {
            var exerciseDetails = await _statisticsRepository.GetExerciseDetailsAsync(model);


            var result = SeparateTheExerciseStatistics(exerciseDetails);

            var exercise = await _exerciseService.GetExerciseByIdAsync(new GetExerciseRequestModel { ExerciseId = model.ExerciseId});

            result.ExerciseId = exercise.Id;

            result.Image = exercise.Image;

            result.ExerciseName = exercise.Name;

            result.CategoryName = exercise.Category.CategoryName;

            result.RepetitionsAverage = exerciseDetails.Average(ed => ed.Repetitions);
            
            return result;
        }

        private ExerciseStatisticsResponseModel SeparateTheExerciseStatistics(List<ExerciseSetDetail> exerciseDetails)
        {

            if(exerciseDetails is null || exerciseDetails.Count == 0)
            {
                return new ExerciseStatisticsResponseModel();
            }

            var orderedByWeight = exerciseDetails.OrderBy(ed => ed.Weight).ToList();
            var orderedByDate = exerciseDetails.OrderBy(ed => ed.DayDone).ToList();


            string firstDay = orderedByDate[0].DayDone.GetKevinDate(); 
            string lastDay = orderedByDate[orderedByDate.Count - 1].DayDone.GetKevinDate();

            return new ExerciseStatisticsResponseModel
            {
                FirstWeight = orderedByDate[0].Weight,
                CurrentWeight = orderedByDate[orderedByDate.Count - 1].Weight,
                HighestWeight = orderedByWeight[orderedByWeight.Count - 1].Weight,
                LowestWeight = orderedByWeight[0].Weight,
                FirstDay = firstDay,
                LastDay = lastDay
            };
        }

        public async Task<WeightStatisticsResponseModel> GetWeightStatisticsAsync()
        {

            var weights = await _statisticsRepository.GetWeightStatisticsAsync(new GetWeightStatisticsRequestModel { UserId = _userService.GetUserId()});

            WeightStatisticsResponseModel weightStatistics = SeparateTheWeightStatistics(weights);

            return weightStatistics;
        }

        private WeightStatisticsResponseModel SeparateTheWeightStatistics(List<UserWeight> weights)
        {
            if (weights is null || weights.Count == 0)
            {
                return new WeightStatisticsResponseModel
                {
                    FirstWeight = 0,
                    CurrentWeight = 0,
                    LowestWeight = 0,
                    HighestWeight = 0
                };
            }

            var weightsOrderedByCreation = weights.OrderBy(w => w.Date).ToList();
            var weightOrderedByKilos = weights.OrderBy(w => w.Kilos).ToList();

            return new WeightStatisticsResponseModel
            {
                FirstWeight = weightsOrderedByCreation[0].Kilos,
                CurrentWeight = weightsOrderedByCreation[weightsOrderedByCreation.Count - 1].Kilos,
                LowestWeight = weightOrderedByKilos[0].Kilos,
                HighestWeight = weightOrderedByKilos[weightOrderedByKilos.Count - 1].Kilos
            };                        
        }
    }
}
