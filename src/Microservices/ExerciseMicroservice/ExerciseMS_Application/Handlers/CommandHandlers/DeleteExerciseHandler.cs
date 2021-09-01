using ExerciseMS_Application.Commands;
using ExerciseMS_Core.Models.Entities;
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
    public class DeleteExerciseHandler : IRequestHandler<DeleteExerciseCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteExerciseHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(DeleteExerciseCommand request, CancellationToken cancellationToken)
        {
            Exercise result = await _unitOfWork.Exercises.DeleteAsync(request.ExerciseId);

            await _unitOfWork.CompleteAsync();

            return result.ExerciseId;
        }
    }
}
