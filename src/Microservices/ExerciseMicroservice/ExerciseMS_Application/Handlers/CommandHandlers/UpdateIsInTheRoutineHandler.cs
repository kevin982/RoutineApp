using ExerciseMS_Application.Commands;
using ExerciseMS_Core.UoW;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExerciseMS_Application.Handlers.CommandHandlers
{
    public class UpdateIsInTheRoutineHandler : IRequestHandler<UpdateIsInTheRoutineCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateIsInTheRoutineHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateIsInTheRoutineCommand request, CancellationToken cancellationToken)
        {
            bool result = await _unitOfWork.Exercises.UpdateIsInTheRoutine(request.NewValue, request.ExerciseId, request.UserId);

            if (result) await _unitOfWork.CompleteAsync();

            return result;
        }
    }
}
