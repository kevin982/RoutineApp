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
        public async Task<string> AddExerciseToRoutine([FromBody]AddExerciseToRoutineRequest request)
        {
            try
            {

                if (request is null || (!request.IsValid()))
                {
                    var error = new { succeeded = false, title = "Bad request", statusCode = 400 };
                    return JsonSerializer.Serialize(error);
                }

                string accessToken = await HttpContext.GetTokenAsync("access_token");

                return await _routineService.AddExerciseToRoutineAsync(accessToken, request);
            }
            catch (Exception ex)
            {
                return SendError(ex);
            }
        }

        [HttpDelete("/v1/Routine/{id}")]
        public async Task<string> RemoveExerciseFromRoutine(Guid id)
        {
            try
            {
                string accessToken = await HttpContext.GetTokenAsync("access_token");

                return await _routineService.RemoveExerciseFromRoutineAsync(accessToken, id);

            }
            catch (Exception ex)
            {
                return SendError(ex);
            }
        }

        [HttpGet("/v1/Routine/Workout")]
        public IActionResult WorkOut()
        {
            return View();
        }

        [HttpGet("/v1/Routine/ExerciseToDo")]
        public async Task<string> GetNextExerciseToDo()
        {
            try
            {
                string accessToken = await HttpContext.GetTokenAsync("access_token");

                return await _routineService.GetExerciseToDoAsync(accessToken);
            }
            catch (Exception ex)
            {
                return SendError(ex);
            }

        }

        [HttpPost("/v1/Routine/ExerciseDone")]
        public async Task<string> SetDone([FromBody]ExerciseDoneRequestModel model)
        {
            try
            {
                string accessToken = await HttpContext.GetTokenAsync("access_token");

                return await _routineService.PostSetDoneAsync(model, accessToken);

            }
            catch (Exception ex)
            {
                return SendError(ex);
            }
        }

        private string SendError(Exception ex)
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
