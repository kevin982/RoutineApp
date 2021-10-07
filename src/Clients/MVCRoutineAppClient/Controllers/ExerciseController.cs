using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCRoutineAppClient.Filters;
using MVCRoutineAppClient.Models;
using MVCRoutineAppClient.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace MVCRoutineAppClient.Controllers
{
    [Controller]
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class ExerciseController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IExerciseService _exerciseService;
        private readonly ILogger<ExerciseController> _logger;

        public ExerciseController(IHttpClientFactory httpClientFactory, IExerciseService exerciseService, ILogger<ExerciseController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _exerciseService = exerciseService;
            _logger = logger;
        }

        [UserAuthorizationFilter]
        [HttpGet("/v1/Exercise")]
        public IActionResult CreateExercise()
        {
            return View();
        }

        [UserAuthorizationFilter]
        [HttpPost("/v1/Exercise")]
        public async Task<IActionResult> CreateExercise(CreateExerciseRequestModel model)
        {
            try
            {
 
                model.FileContentType = model.Image.ContentType;

                string accessToken = await HttpContext.GetTokenAsync("access_token");
                
                ViewBag.Result = await _exerciseService.CreateExerciseAsync(model, accessToken);
            }
            catch (Exception)
            {
                JObject error = new();

                error.Add("succeeded", false);
                error.Add("title", "Internal Error");
            }
            
            return View();
        }

        [UserAuthorizationFilter]
        [HttpGet("/v1/Exercise/Category/{categoryId}/{index}/{size}")]
        public async Task<string> GetExercisesByCategory(Guid categoryId, int index, int size)
        {
            try
            {
                index -= 1;

                string accessToken = await HttpContext.GetTokenAsync("access_token");

                return await _exerciseService.GetExercisesByCategory(accessToken, categoryId, index, size);

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

        [UserAuthorizationFilter]
        [HttpGet("/v1/Exercise/IndexesCount/{categoryId}/{size}")]
        public async Task<string> GetIndexesCount(Guid categoryId, int size)
        {
            try
            {
                string accessToken = await HttpContext.GetTokenAsync("access_token");

                return  await _exerciseService.GetIndexesCount(accessToken, categoryId, size);

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
        
        [UserAuthorizationFilter]
        [HttpGet("/v1/Exercise/Category/NameAndId/{categoryId}")]
        public async Task<string> GetExercisesNameAndIdByCategory(Guid categoryId)
        {
            try
            {
                string accessToken = await HttpContext.GetTokenAsync("access_token");

                return await _exerciseService.GetExercisesNameAndIdByCategory(accessToken, categoryId);

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
