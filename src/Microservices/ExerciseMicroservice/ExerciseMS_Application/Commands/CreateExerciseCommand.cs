using ExerciseMS_Core.Models.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_Application.Commands
{
    public class CreateExerciseCommand : IRequest<bool>
    {
        public CreateExerciseRequest CreateExerciseRequest { get; init; }

        public CreateExerciseCommand(CreateExerciseRequest createExerciseRequest)
        {
            CreateExerciseRequest = createExerciseRequest;
        }
    }
}
