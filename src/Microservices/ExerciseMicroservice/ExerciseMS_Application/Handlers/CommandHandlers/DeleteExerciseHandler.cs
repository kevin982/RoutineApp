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
    public class DeleteExerciseHandler : IRequestHandler<DeleteExerciseCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteExerciseHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteExerciseCommand request, CancellationToken cancellationToken)
        {
            bool result = await _unitOfWork.Exercises.DeleteAsync(request.ExerciseId);

            if (result) await _unitOfWork.CompleteAsync();

            return result;
        }
    }
}
