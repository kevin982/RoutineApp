using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCRoutineAppClient.Filters;
using MVCRoutineAppClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCRoutineAppClient.Controllers
{
    [ApiController]
    [Authorize]
    public class ExerciseController : Controller
    {
        [UserAuthorizationFilter]
        [HttpGet("/v1/Exercise")]
        public IActionResult CreateExercise()
        {
            return View();
        }

        [UserAuthorizationFilter]
        [HttpPost("/v1/Exercise")]
        public IActionResult CreateExercise(CreateExerciseRequestModel model)
        {
            //I must implement the logic to add the exercise here.

            return View();
        }

        [AdminAuthorizationFilter]
        [HttpGet("/v1/Category")]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [AdminAuthorizationFilter]
        [HttpPost("/v1/Category")]
        public IActionResult CreateCategory([FromForm]CreateCategoryRequestModel model)
        {
            //I must implement the logic to add the category here.

            return View();
        }

        [UserAuthorizationFilter]
        [HttpGet("/v1/Categories")]
        public async Task<ActionResult<string>> GetAllCategories()
        {
            //I must implement the logic to get all the categories here

            return "";
        }

    }
}
