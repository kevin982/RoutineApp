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
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Commands
        
        [HttpPost("/api/v1/Category")]
        public async Task<ActionResult> CreateCategory(CreateCategoryRequest model)
        {
            CreateCategoryCommand command = new(model);

            bool result = await _mediator.Send(command);

            return (result) ? Ok() : BadRequest();
        }
        
        [HttpDelete("api/v1/Category/{id}")]
        public async Task<ActionResult> DeleteCategory(Guid id)
        {
            DeleteCategoryCommand command = new(id);

            bool result = await _mediator.Send(command);

            return (result) ? Ok() : NotFound();
        }

        #endregion

        #region Queries

        [HttpGet("api/v1/Category")]
        public async Task<ActionResult<IEnumerable<DtoExercise>>> GetAllCategories()
        {
            GetAllCategoriesQuery query = new();

            var result = await _mediator.Send(query);

            return (result is not null)? Ok(result) : NotFound();
        }

        #endregion


    }
}
