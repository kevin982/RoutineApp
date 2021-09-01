using ExerciseMS_Core.Dtos;
using ExerciseMS_Core.Models.Requests;
using MediatR;


namespace ExerciseMS_Application.Commands
{
    public class CreateExerciseCommand : IRequest<DtoExercise>
    {
        public CreateExerciseRequest CreateExerciseRequest { get; init; }

        public CreateExerciseCommand(CreateExerciseRequest createExerciseRequest)
        {
            CreateExerciseRequest = createExerciseRequest;
        }
    }
}
