using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StatisticsMS_Application.Queries;
using StatisticsMS_Core.Models.Response;
using StatisticsMS_Core.Services;
using System;
using System.Threading.Tasks;

namespace StatisticsMS_API.Controllers
{
    [ApiController]
    [Authorize(Roles = "user")]
    [Authorize(Policy = "StatisticsScope")]
 
    public class StatisticController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICustomSender _sender;
        public StatisticController(ICustomSender sender, IMediator mediator)
        {
            _sender = sender;
            _mediator = mediator;
        }

        [HttpGet("/api/v1/Statistics/{exerciseId}/{month:int}/{year:int}")]
        public async Task<ActionResult<HateoasResponse>> GetExerciseStatistics(Guid exerciseId, int month, int year)
        {
            try
            {
                GetExerciseStatisticsQuery query = new(exerciseId : exerciseId, month : month, year: year);

                var result = await _mediator.Send(query);

                return Ok(_sender.SendResult(result, GenericLinks.GetStatisticsLinks(),"The exercise statistics have been reached!"));
            }
            catch (Exception ex)
            {
                var errorResponse = _sender.SendError(ex, GenericLinks.GetStatisticsLinks());

                Response.StatusCode = errorResponse.StatusCode;

                return errorResponse;
            }
        }

    }
}
