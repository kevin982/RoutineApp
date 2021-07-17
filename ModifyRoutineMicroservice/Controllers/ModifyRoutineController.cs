using DomainRoutineLibrary.Exceptions;
using DomainRoutineLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ModifyRoutineMicroservice.Models;
using ModifyRoutineMicroservice.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModifyRoutineMicroservice.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [EnableCors("MVCRoutineClient")]
    //[Authorize]
    public class ModifyRoutineController : ControllerBase
    {

        private readonly IModifyRoutineService _modifyRoutineService;

        public ModifyRoutineController(IModifyRoutineService modifyRoutineService)
        {
            _modifyRoutineService = modifyRoutineService;
        }


        [HttpPatch("/Exercise")]
        public async Task<ActionResult<ApiResponseModel>> AddExercise(AddExerciseToRoutineRequestModel model)
        {
            try
            {
                await _modifyRoutineService.AddExerciseToRoutineAsync(model);

                var response = new ApiResponseModel { Message = $"The exercise with the id {model.ExerciseId} has been added to the user routine successfully!" };

                response.Links.Add(new LinkModel { href = $"https://localhost:5001/api/v1/{nameof(ModifyRoutineController)}/Exercise", method = "Patch", rel = "To add a new exercise to the routine" });
                response.Links.Add(new LinkModel { href = $"https://localhost:5001/api/v1/{nameof(ModifyRoutineController)}/Exercise", method = "Delete", rel = "To remove an exercise from the routine" });

                return Ok(response);

            }
            catch (ApiException ex)
            {
                var response = new ApiResponseModel { Message = $"The exercise could not be added because of {ex.Error}" };

                response.Links.Add(new LinkModel { href = $"https://localhost:5001/api/v1/{nameof(ModifyRoutineController)}/Exercise", method = "Patch", rel = "To add a new exercise to the routine" });
                response.Links.Add(new LinkModel { href = $"https://localhost:5001/api/v1/{nameof(ModifyRoutineController)}/Exercise", method = "Delete", rel = "To remove an exercise from the routine" });
                
                Response.StatusCode = ex.HttpCode;

                return response;
            }
        }

        [HttpDelete("/Exercise/{id}")]
        public async Task<ActionResult<ApiResponseModel>> DeleteExercise(int id)
        {
            try
            {
                await _modifyRoutineService.RemoveExerciseFromRoutineAsync(id);

                var response = new ApiResponseModel { Message = $"The exercise with the id {id} has been removed to the user routine successfully!" };

                response.Links.Add(new LinkModel { href = $"https://localhost:5001/api/v1/{nameof(ModifyRoutineController)}/Exercise", method = "Patch", rel = "To add a new exercise to the routine" });
                response.Links.Add(new LinkModel { href = $"https://localhost:5001/api/v1/{nameof(ModifyRoutineController)}/Exercise", method = "Delete", rel = "To remove an exercise from the routine" });

                return Ok(response);
            }
            catch (ApiException ex) 
            {
                var response = new ApiResponseModel { Message = $"The exercise could not be removed because of {ex.Error}" };

                response.Links.Add(new LinkModel { href = $"https://localhost:5001/api/v1/{nameof(ModifyRoutineController)}/Exercise", method = "Patch", rel = "To add a new exercise to the routine" });
                response.Links.Add(new LinkModel { href = $"https://localhost:5001/api/v1/{nameof(ModifyRoutineController)}/Exercise", method = "Delete", rel = "To remove an exercise from the routine" });

                Response.StatusCode = ex.HttpCode;

                return response;
            }
        }
 
    }
}
