using ExerciseMS_Application.Commands;
using ExerciseMS_Application.Queries;
using ExerciseMS_Core.Dtos;
using ExerciseMS_Core.Exceptions;
using ExerciseMS_Core.Models;
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
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICustomSender _sender;

        public CategoryController(IMediator mediator, ICustomSender sender)
        {
            _mediator = mediator;
            _sender = sender;
        }

        #region Commands

        [Authorize(Policy = "WriteScope")]
        [Authorize(Policy = "Admin")]
        [HttpPost("/api/v1/Category")]
        public async Task<ActionResult<HateoasResponse>> CreateCategory(CreateCategoryRequest model)
        {
            try
            {
                CreateCategoryCommand command = new(model);

                var categoryDto = await _mediator.Send(command);

                return Ok(_sender.SendResult(categoryDto, GenericLinks.GetCategoryLinks(), $"The category whose name is {categoryDto.CategoryName} has been created!"));
            }
            catch (Exception ex)
            {
                var result = _sender.SendError(ex, GenericLinks.GetCategoryLinks());

                Response.StatusCode = result.StatusCode;

                return result;
            }


        }


        [Authorize(Policy = "WriteScope")]
        [Authorize(Policy = "Admin")]
        [HttpDelete("api/v1/Category/{id}")]
        public async Task<ActionResult<HateoasResponse>> DeleteCategory(Guid id)
        {
            try
            {
                DeleteCategoryCommand command = new(id);

                var categoryDeleted = new { CategoryDeletedId = await _mediator.Send(command) };

                return Ok(_sender.SendResult(categoryDeleted, GenericLinks.GetCategoryLinks(),$"The category with the id {categoryDeleted.CategoryDeletedId} has been deleted!"));

            }
            catch (Exception ex)
            {
                var result = _sender.SendError(ex, GenericLinks.GetCategoryLinks());

                Response.StatusCode = result.StatusCode;

                return result;
            }
        }

        #endregion

        #region Queries

        [Authorize(Policy = "ReadScope")]
        [Authorize(Policy = "User,Admin")]
        [HttpGet("api/v1/Category")]
        public async Task<ActionResult<HateoasResponse>> GetAllCategories()
        {
            try
            {
                GetAllCategoriesQuery query = new();

                var categories = await _mediator.Send(query);

                return Ok(_sender.SendResult(categories, GenericLinks.GetCategoryLinks(), "The categories have been achieved!"));
            }
            catch (Exception ex)
            {
                var result = _sender.SendError(ex, GenericLinks.GetCategoryLinks());

                Response.StatusCode = result.StatusCode;

                return result;
            }
        }
        #endregion
    
    }
}
