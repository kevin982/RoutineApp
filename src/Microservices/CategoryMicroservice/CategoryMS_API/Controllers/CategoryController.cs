
using CategoryMS_Application.Commands;
using CategoryMS_Application.Queries;
using CategoryMS_Core.Models.Requests;
using CategoryMS_Core.Models.Responses;
using CategoryMS_Core.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CategoryMS_API.Controllers
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

        [Authorize(Roles = "admin")]
        [Authorize(Policy = "CategoryScope")]
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
 

        #endregion

        #region Queries

        [Authorize(Roles = "user,admin")]  
        [Authorize(Policy = "CategoryScope")]
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
