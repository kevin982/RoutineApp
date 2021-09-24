using ExerciseMS_Application.Commands;
using ExerciseMS_Application.Queries;
using ExerciseMS_Core.Dtos;
using ExerciseMS_Core.Models.Requests;
using ExerciseMS_Core.Models.Responses;
using ExerciseMS_Core.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_API.Controllers
{
    [ApiController]
    [Authorize(Roles = "user")]
    [Authorize(Policy = "ExerciseScope")]
    public class ExerciseController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICustomSender _sender;


        public ExerciseController(IMediator mediator, ICustomSender sender)
        {
            _mediator = mediator;
            _sender = sender;
        }

        #region Commands

        [Authorize(Roles = "user")]
        [Authorize(Policy = "ExerciseScope")]
        [HttpPost("/api/v1/Exercise")]
        public async Task<ActionResult<HateoasResponse>> CreateExercise([FromForm]CreateExerciseRequest model)
        {
            try
            {
                CreateExerciseCommand command = new(model);

                var exercise = await _mediator.Send(command);

                return Ok(_sender.SendResult(exercise, GenericLinks.GetExerciseLinks(), $"The exercise whose name is {exercise.ExerciseName} has been created!"));
            }
            catch (Exception ex)
            {
                var errorResponse = _sender.SendError(ex, GenericLinks.GetExerciseLinks());

                Response.StatusCode = errorResponse.StatusCode;

                return errorResponse;
            }
        }

        [Authorize(Roles = "user")]
        [Authorize(Policy = "ExerciseScope")]
        [HttpDelete("/api/v1/Exercise/{id}")]
        public async Task<ActionResult<HateoasResponse>> DeleteExercise(Guid id)
        {
            try
            {
                DeleteExerciseCommand command = new(id);

                var exerciseDeleted = new { DeletedExerciseId = await _mediator.Send(command) };

                return Ok(_sender.SendResult(exerciseDeleted, GenericLinks.GetExerciseLinks(), $"The exercise whose id is {exerciseDeleted.DeletedExerciseId} has been deleted!"));
            }
            catch (Exception ex)
            {
                var errorResponse = _sender.SendError(ex, GenericLinks.GetExerciseLinks());

                Response.StatusCode = errorResponse.StatusCode;

                return errorResponse;
            }

        }

        #endregion


        #region Queries

 
        [HttpGet("/api/v1/Exercise/Category/{categoryId}/{index:int:min(0)}/{size:int:min(5):max(20)}")]
        public async Task<ActionResult<HateoasResponse>> GetAllExercisesByCategory(Guid categoryId, int index, int size)
        {

            try
            {
                GetExercisesByCategoryQuery query = new(categoryId, index, size);

                var exercises = await _mediator.Send(query);

                return Ok(_sender.SendResult(exercises, GenericLinks.GetExerciseLinks(), $"The exercises whose category id is {categoryId} have been reached!"));

            }
            catch (Exception ex)
            {
                var errorResponse = _sender.SendError(ex, GenericLinks.GetExerciseLinks());

                Response.StatusCode = errorResponse.StatusCode;

                return errorResponse;
            }


        }

        
        [HttpGet("/api/v1/Exercise/IndexesCount/{categoryId}/{size:int:min(5):max(20)}")]
        public async Task<ActionResult<HateoasResponse>> GetIndexesCount(Guid categoryId, int size)
        {
            try
            {
                GetIndexesCount query = new(categoryId, size);

                var count = new { Count = await _mediator.Send(query) };

                return Ok(_sender.SendResult(count, GenericLinks.GetExerciseLinks(), "The exercise count with that specific category has been reached!"));
            }
            catch (Exception ex)
            {
                var errorResponse = _sender.SendError(ex, GenericLinks.GetExerciseLinks());

                Response.StatusCode = errorResponse.StatusCode;

                return errorResponse;
            }

        }

        #endregion

    }
}
