using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoutineMS_Application.Commands;
using RoutineMS_Application.Queries;
using RoutineMS_Core.Models.Requests;
using RoutineMS_Core.Models.Responses;
using RoutineMS_Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutineMS_API.Controllers
{
    [ApiController]
    [AutoValidateAntiforgeryToken]
    public class RoutineController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICustomSender _sender;
        public RoutineController(IMediator mediator, ICustomSender sender)
        {
            _mediator = mediator;
            _sender = sender;
        }


        [HttpPost("/api/v1/Routine")]
        public async Task<ActionResult<HateoasResponse>> AddExerciseToRoutine(AddExerciseToRoutineRequest request)
        {
            try
            {
                AddExerciseToRoutineCommand command = new(request);

                bool result = await _mediator.Send(command);

                return Ok(_sender.SendResult(new {ExerciseCreated = result}, GenericLinks.GetRoutineLinks(), "The exercise has been added to your Routine!"));
            }
            catch (Exception ex)
            {
                var errorResponse = _sender.SendError(ex, GenericLinks.GetRoutineLinks());

                Response.StatusCode = errorResponse.StatusCode;

                return errorResponse;
            }
        }

        [HttpDelete("/api/v1/Routine/{id}")]
        public async Task<ActionResult<HateoasResponse>> RemoveExerciseFromRoutine(Guid id)
        {
            try
            {
                RemoveExerciseFromRoutineCommand command = new(id);

                bool result = await _mediator.Send(command);

                return Ok(_sender.SendResult(new { ExerciseRemoved = result }, GenericLinks.GetRoutineLinks(), "The exercise has been removed!"));
            }
            catch (Exception ex)
            {
                var errorResponse = _sender.SendError(ex, GenericLinks.GetRoutineLinks());

                Response.StatusCode = errorResponse.StatusCode;

                return errorResponse;
            }
        }

        [HttpPost("/api/v1/Routine/SetDone")]
        public async Task<ActionResult<HateoasResponse>> SetDone(SetDoneRequest request)
        {
            try
            {
                SetDoneCommand command = new(request);

                bool result = await _mediator.Send(command);

                return Ok(_sender.SendResult(new { SetDoneCreated = result }, GenericLinks.GetRoutineLinks(), "The set done has been registered!"));
            }
            catch (Exception ex)
            {
                var errorResponse = _sender.SendError(ex, GenericLinks.GetRoutineLinks());

                Response.StatusCode = errorResponse.StatusCode;

                return errorResponse;
            }
        }

        [HttpGet("/api/v1/Routine")]
        public async Task<ActionResult<HateoasResponse>> GetExerciseToDo()
        {
            try
            {
                GetExerciseToDoQuery query = new ();

                var result = await _mediator.Send(query);

                string message = (result is not null) ? $"You have to do the {result.Name}" : "You are done for today, go and get some rest!";

                return Ok(_sender.SendResult(result, GenericLinks.GetRoutineLinks(), message));
            }
            catch (Exception ex)
            {
                var errorResponse = _sender.SendError(ex, GenericLinks.GetRoutineLinks());

                Response.StatusCode = errorResponse.StatusCode;

                return errorResponse;
            }
        }
    }
}
