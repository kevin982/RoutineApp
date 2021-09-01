using ExerciseMS_Core.Dtos;
using MediatR;
using System;

namespace ExerciseMS_Application.Commands
{
    public class DeleteExerciseCommand : IRequest<Guid>
    {
        public Guid ExerciseId { get; init; }

        public DeleteExerciseCommand(Guid exerciseId)
        {
            ExerciseId = exerciseId;
        }
    }
}
