using DomainRoutineApp.Models.Requests.Statics;
using DomainRoutineApp.Models.Responses.Statics;
using DomainRoutineApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutineCoreApp.Controllers
{
    [Route("[controller]/[action]")]
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class StatisticsController:Controller
    {
        [ViewData]
        public ExerciseStatisticsResponseModel exerciseStatistics { get; set; } = null;

        private readonly IStatisticsService _statisticsService = null;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        public IActionResult Weight()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Weight(AddPersonWeightRequestModel model)
        {
            if (ModelState.IsValid)
            {
                await _statisticsService.AddWeightAsync(model);
            }

            return View();
        }

        public IActionResult ExerciseStatics()
        {
            exerciseStatistics = null;


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ExerciseStatics(GetExerciseStatisticsRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _statisticsService.GetExerciseStatisticsAsync(model);
                
                exerciseStatistics = result;
            }

            return View();
        }
    }
}
