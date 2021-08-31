using ExerciseMS_Application.Commands;
using ExerciseMS_Application.Queries;
using ExerciseMS_Core.Dtos;
using ExerciseMS_Core.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_API.Controllers
{
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExerciseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Commands

        [HttpPost("/api/v1/Exercise")]
        public async Task<ActionResult> CreateExercise(CreateExerciseRequest model)
        {
            CreateExerciseCommand command = new(model);

            bool result = await _mediator.Send(command);

            return (result) ? Ok() : BadRequest();
        }


        [HttpDelete("/api/v1/Exercise/{id}")]
        public async Task<ActionResult> DeleteExercise(string id)
        {
            DeleteExerciseCommand command = new(new Guid(id));

            bool result = await _mediator.Send(command);

            return (result) ? Ok() : NotFound();
        }

        #endregion


        #region Queries

        [HttpGet("/api/v1/Exercise/{categoryId}/{index}/{size}")]
        public async Task<ActionResult<IEnumerable<DtoExercise>>> GetAllExercisesByCategory(string categoryId, int index, int size)
        {
            GetExercisesByCategoryQuery query = new(new Guid (categoryId), index, size);

            var result = _mediator.Send(query);

            return (result is not null) ? Ok(result) : NotFound();
        }

        [HttpGet("/api/v1/Exercise/Category/{categoryId}")]

        public async Task<ActionResult<int>> GetExercisesCountByCategory(string categoryId)
        {
            GetExercisesCountByCategoryQuery query = new(new Guid(categoryId));

            var result = await _mediator.Send(query);

            return (result is not null) ? Ok(result) : NotFound();
        }

        #endregion

    }
}
