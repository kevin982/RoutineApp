using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCRoutineAppClient.Filters;
using MVCRoutineAppClient.Models;
using MVCRoutineAppClient.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MVCRoutineAppClient.Controllers
{
    [Controller]
    [Authorize]
    public class ExerciseController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IExerciseService _exerciseService;


        public ExerciseController(IHttpClientFactory httpClientFactory, IExerciseService exerciseService)
        {
            _httpClientFactory = httpClientFactory;
            _exerciseService = exerciseService;
        }

        [UserAuthorizationFilter]
        [HttpGet("/v1/Exercise")]
        public async Task<IActionResult> CreateExercise()
        {
            ViewBag.AccessToken = await HttpContext.GetTokenAsync("access_token");

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

                ViewBag.AccessToken = accessToken;
                
                var result = await _exerciseService.CreateExerciseAsync(model, accessToken);

                ViewBag.Succeeded = result.Item1;

                ViewBag.Message = result.Item2;

            }
            catch (Exception ex)
            {
                ViewBag.Succeeded = false;

                ViewBag.Message = ex.Message;
            }
            
            return View();
        }
    }
}
