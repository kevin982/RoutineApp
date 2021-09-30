using MediatR;
using RoutineMS_Application.Extensions;
using RoutineMS_Application.Queries;
using RoutineMS_Core.Dtos;
using RoutineMS_Core.Services;
using RoutineMS_Core.UoW;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RoutineMS_Application.Handlers
{
    public class GetExerciseToDoHandler : IRequestHandler<GetExerciseToDoQuery, ExerciseToDoDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        public GetExerciseToDoHandler(IUnitOfWork unitOfWork, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        public async Task<ExerciseToDoDto> Handle(GetExerciseToDoQuery request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.SetsDetails.DeleteOldDetailsAsync();
                await _unitOfWork.CompleteAsync();  
                Guid userId = new(_userService.GetUserId());
                var result = await _unitOfWork.Routines.GetExerciseToDoFromRoutineAsync(userId, DateTime.UtcNow.GetDayOfWeek());

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
