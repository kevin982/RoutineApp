using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutineMS_Application.Commands
{
    public class RemoveExerciseFromRoutineCommand : IRequest<bool>
    {
        public Guid ExerciseId { get; set; }
        public RemoveExerciseFromRoutineCommand(Guid exerciseId)
        {
            ExerciseId = exerciseId;
        }
    }
}
