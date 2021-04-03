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

    [Route("[controller]/[action]")]
    public class ExerciseController : Controller
    {

        private readonly IExerciseService _exerciseService = null;

        public ExerciseController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
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

                ViewBag.Result = await _exerciseService.CreateExerciseAsync(model);
            }

            return View();
        }

        public IActionResult CreateRoutine()
        {
            return View();
        }


        [HttpGet("~/GetAllExercisesAsync")]
        public async Task<List<Exercise>> GetAllExercisesAsync()
        {
            return await _exerciseService.GetAllExercisesAsync();
        }
 
        [HttpPost]
        public async Task<IActionResult> AddExercise(AddExerciseModel model)
        {

            await _exerciseService.AddExerciseToRoutineAsync(model);

            return RedirectToAction("CreateRoutine");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RemoveExercise(int id)
        {
            await _exerciseService.RemoveExerciseFromRoutineAsync(id);
            return RedirectToAction("CreateRoutine");
        }


    }

    }

