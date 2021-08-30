using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_Application.Commands
{
    public class UpdateIsInTheRoutineCommand : IRequest<bool>
    {
        public bool NewValue { get; set; }

        public Guid ExerciseId { get; set; }

        public Guid UserId { get; set; }

        public UpdateIsInTheRoutineCommand(bool newValue, Guid exerciseId, Guid userId)
        {
            NewValue = newValue;
            ExerciseId = exerciseId;
            UserId = userId;
        }
    }
}
