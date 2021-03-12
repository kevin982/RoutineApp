using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoutineApp.Data.Entities;
using RoutineApp.Models;
using RoutineApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RoutineApp.Controllers
{

    [Authorize]
    [Route("[controller]/[action]")]
    public class RoutineController : Controller
    {

        private readonly IRoutineService _routineService = null;

        public RoutineController(IRoutineService routineService)
        {
            _routineService = routineService;
        }

        public IActionResult CreateExercise()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateExercise(CreateExerciseModel model)
        {

            if (ModelState.IsValid)
            {
                foreach (var image in model.Images)
                {
                    Image img = new();

                    MemoryStream ms = new();

                    await image.CopyToAsync(ms);

                    img.Img = ms.ToArray();

                    model.ImageToStore.Add(img);

                    ms.Close();
                    ms.Dispose();
                }

                ViewBag.Result = await _routineService.CreateExerciseAsync(model);
            }

            return View();
        }

    }
}
