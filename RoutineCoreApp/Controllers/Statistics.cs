using DomainRoutineApp.Models.Requests.Statics;
using DomainRoutineApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutineCoreApp.Controllers
{
    [Route("[controller]/[action]")]
    public class Statistics:Controller
    {

        private readonly IStatisticsService _statisticsService = null;

        public Statistics(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [HttpGet]
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

    }
}
