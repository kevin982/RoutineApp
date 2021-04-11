using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoutineApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoutineApp.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class WorkOutController : Controller
    {
        private readonly IWorkOutService _workOutService = null;

        public WorkOutController(IWorkOutService workOutService)
        {
            _workOutService = workOutService;
        }
        
        public async Task<IActionResult> WorkOut()
        {
            var result = await _workOutService.GetNextExerciseAsync();

            return View(result);
        }

        //public async Task<IActionResult> ExerciseDone()
        //{

        //}





    }
}
