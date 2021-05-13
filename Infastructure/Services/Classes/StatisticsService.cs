using DomainRoutineApp.Models.Entities;
using DomainRoutineApp.Models.Requests.Statics;
using DomainRoutineApp.Models.Responses.Statics;
using DomainRoutineApp.Repositores.Interfaces;
using DomainRoutineApp.Services.Interfaces;
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


        public StatisticsService(IStatisticsRepository statisticsRepository, IUserService userService)
        {
            _statisticsRepository = statisticsRepository;
            _userService = userService;
        }

        public async Task AddWeightAsync(AddPersonWeightRequestModel model)
        {
            model.UserId = _userService.GetUserId();

            await _statisticsRepository.AddWeightAsync(model);
        }

        public async Task<WeightStatisticsResponseModel> GetWeightStatisticsAsync()
        {

            var weights = await _statisticsRepository.GetWeightStatisticsAsync(new GetWeightStatisticsRequestModel { UserId = _userService.GetUserId()});

            WeightStatisticsResponseModel weightStatistics = SeparateTheStatistics(weights);

            return weightStatistics;
        }

        private WeightStatisticsResponseModel SeparateTheStatistics(List<Weight> weights)
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
