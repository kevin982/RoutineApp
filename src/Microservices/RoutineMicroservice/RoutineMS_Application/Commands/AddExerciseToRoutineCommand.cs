using MediatR;
using RoutineMS_Core.Models.Requests;

namespace RoutineMS_Application.Commands
{
    public class AddExerciseToRoutineCommand : IRequest<bool>
    {
        public AddExerciseToRoutineRequest Request { get; init; }

        public AddExerciseToRoutineCommand(AddExerciseToRoutineRequest request)
        {
            Request = request;
        }
    }
}
