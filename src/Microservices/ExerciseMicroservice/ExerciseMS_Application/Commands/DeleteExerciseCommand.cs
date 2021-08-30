using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_Application.Commands
{
    public class DeleteExerciseCommand : IRequest<bool>
    {
        public Guid ExerciseId { get; init; }
        public DeleteExerciseCommand(Guid exerciseId)
        {
            ExerciseId = exerciseId;
        }
    }
}
