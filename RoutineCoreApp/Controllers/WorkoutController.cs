using DomainRoutineApp.Models.Requests.Exercise;
using DomainRoutineApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoutineCoreApp.Controllers
{
    [Route("[controller]/[action]")]
    [Authorize]
    public class WorkoutController : Controller
    {
        private readonly IWorkoutService _workOutService = null;
        private readonly IExerciseService _exerciseService = null;

        public WorkoutController(IWorkoutService workOutService, IExerciseService exerciseService)
        {
            _workOutService = workOutService;
            _exerciseService = exerciseService;
        }

        public async Task<IActionResult> WorkOut()
        {
            var result = await _workOutService.GetNextExerciseAsync();

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> ExerciseDone(ExerciseDoneRequestModel model)
        {
            if (ModelState.IsValid)
            {
                await _workOutService.CreateAndAddExerciseDetailAsync(model);
            }

            return RedirectToAction("WorkOut");
        }
    }
}