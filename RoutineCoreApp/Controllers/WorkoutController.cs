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
    [Authorize]
    [Route("[controller]/[action]")]
    public class WorkOutController : Controller
    {
        private readonly IWorkoutService _workOutService = null;

        public WorkOutController(IWorkoutService workOutService)
        {
            _workOutService = workOutService;
        }

        public async Task<IActionResult> WorkOut()
        {
            var result = await _workOutService.GetNextExerciseAsync();

            return View(result);
        }

     }
}
