using ClientRoutineApp.Models;
using ClientRoutineApp.Services.Interfaces;
using DomainRoutineApp.Models.Requests.Exercise;
using DomainRoutineApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutineCoreApp.Controllers
{
    [Route("[controller]/[action]")]
    [Authorize]
    public class ExerciseController : Controller
    {

        private readonly IExerciseService _exerciseService = null;
        private readonly IConfiguration _configuration = null;
        private readonly IImageService _imageService = null;

        public ExerciseController(IExerciseService exerciseService, IConfiguration configuration, IImageService imageService)
        {
            _exerciseService = exerciseService;
            _configuration = configuration;
            _imageService = imageService;
        }

        public IActionResult CreateExercise()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateExercise(CreateExerciseRequestModel model)
        {
 
            if (ModelState.IsValid)
            {

                string cloud =_configuration.GetValue<string>("Cloudinary:cloud");
                string apikey = _configuration.GetValue<string>("Cloudinary:apikey");
                string secret = _configuration.GetValue<string>("Cloudinary:secret");

                foreach (var image in model.Images)
                {
  
                    PostImageRequestModel postImage = new()
                    {
                        Image = image.OpenReadStream(),
                        ImageName = image.FileName,
                        Cloud = cloud,
                        ApiKey = apikey,
                        Secret = secret
                    };

                    string imageUrlCloudinary = await _imageService.PostImageAsync(postImage);

                    model.ImagesUrl.Add(imageUrlCloudinary);
                }

                await _exerciseService.CreateExerciseAsync(model);
            }

            return View();
        }

        public IActionResult CreateRoutine()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddExercise(AddExerciseToRoutineRequestModel model)
        {

            await _exerciseService.AddExerciseToRoutineAsync(model);

            return RedirectToAction("CreateRoutine");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RemoveExercise(int id)
        {
            var model = new RemoveExerciseFromRoutineRequestModel() { ExerciseId = id };

            await _exerciseService.RemoveExerciseFromRoutineAsync(model);
            return RedirectToAction("CreateRoutine");
        }


    }

}
