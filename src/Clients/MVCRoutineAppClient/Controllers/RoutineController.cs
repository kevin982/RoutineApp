using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCRoutineAppClient.Models;
using MVCRoutineAppClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MVCRoutineAppClient.Controllers
{
    [Authorize]
    [Controller]
    public class RoutineController : Controller
    {
        private readonly IRoutineService _routineService;
        private readonly ILogger<RoutineController> _logger;

        public RoutineController(IRoutineService routineService, ILogger<RoutineController> logger)
        {
            _routineService = routineService;
            _logger = logger;
        }

        [HttpGet("/v1/Routine")]
        public IActionResult CreateRoutine()
        {
            return View();
        }

        [HttpPost("/v1/Routine")]
        public async Task<string> AddExerciseToRoutine(AddExerciseToRoutineRequest request)
        {
            try
            {
                if(request is null)
                {
                    var error = new {succeeded = false, title = "Bad request", statusCode = 400};
                    return JsonSerializer.Serialize(error);
                }
                else if (!request.IsValid())
                {   
                    var error = new{succeeded = false, title = "Bad Request",statusCode = 400};
                    return JsonSerializer.Serialize(error);
                }

                string accessToken = await HttpContext.GetTokenAsync("access_token");

                return await _routineService.AddExerciseToRoutineAsync(accessToken, request);
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

        [HttpDelete("/v1/Routine/{id}")]
        public async Task<string> RemoveExerciseFromRoutine(Guid exerciseId)
        {
            try
            {
                string accessToken = await HttpContext.GetTokenAsync("access_token");

                return await _routineService.RemoveExerciseFromRoutineAsync(accessToken, exerciseId);

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
