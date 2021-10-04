using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCRoutineAppClient.Services;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MVCRoutineAppClient.Controllers
{
    [Controller]
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class StatisticsController : Controller
    {
        private readonly IStatisticsService _statisticsService;
        private readonly ILogger<StatisticsController> _logger;

        public StatisticsController(IStatisticsService statisticsService, ILogger<StatisticsController> logger)
        {
            _statisticsService = statisticsService;
            _logger = logger;
        }

        // GET
        [HttpGet("/v1/Statistics")]
        public IActionResult ExerciseStatistics()
        {
            return View();
        }

        [HttpGet("/v1/Statistics/{exerciseId}/{month:int}/{year:int}")]
        public async Task<string> GetExerciseStatistics(Guid exerciseId, int month, int year)
        {
            try
            {
                string accessToken = await HttpContext.GetTokenAsync("access_token");

                return await _statisticsService.GetExerciseStatisticsAsync(accessToken, exerciseId, month, year);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An exception happend at {DateTime.UtcNow}. Error message {ex.Message}");

                var error = new
                {
                    succeeded = false,
                    title = "Internal error",
                    statusCode = 500
                };

                return JsonSerializer.Serialize(error);
            }
        }
        
    }
}