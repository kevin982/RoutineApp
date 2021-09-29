using MediatR;
using Microsoft.Extensions.Configuration;
using RoutineMS_Application.Commands;
using RoutineMS_Core.Exceptions;
using RoutineMS_Core.Services;
using RoutineMS_Core.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoutineMS_Application.Handlers
{
    public class RemoveExerciseFromRoutineHandler : IRequestHandler<RemoveExerciseFromRoutineCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPublisherService _publisherService;
        private readonly IUserService _userService;
        private readonly IConfiguration Configuration;

        public RemoveExerciseFromRoutineHandler(IUnitOfWork unitOfWork, IPublisherService publisherService, IUserService userService, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _publisherService = publisherService;
            _userService = userService;
            Configuration = configuration;
        }

        public async Task<bool> Handle(RemoveExerciseFromRoutineCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Guid userId = new(_userService.GetUserId());

                var routine = await _unitOfWork.Routines.TheExerciseIsInRoutineAsync(request.ExerciseId, userId);

                if (!routine) throw new RoutineMSException("The exercise is not in the routine!") { StatusCode = 400 };

                await _unitOfWork.Routines.RemoveRoutineByExerciseAsync(request.ExerciseId, userId);

                await _unitOfWork.CompleteAsync();

                _publisherService.PublishEvent(new { UserId = userId, ExerciseId = request.ExerciseId, NewValue = false }, Configuration["Events:UpdateExercise:Exchange"], Configuration["Events:UpdateExercise:RoutingKey"]);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
