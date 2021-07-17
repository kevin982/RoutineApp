 
using DomainRoutineApp.Models.Requests.Exercise;
using DomainRoutineApp.Models.Responses.Exercise;
using DomainRoutineLibrary.Entities;
using System.Collections.Generic;

namespace RoutineApp.Mappers.Interfaces
{
    public interface IExerciseMapper
    {
        ExerciseWorkOutResponseModel MapDomainToExerciseWorkOutResponse(Exercise exercise);

        Exercise MapCreateExerciseToDomain(CreateExerciseRequestModel model);

        List<CreateRoutineExerciseResponseModel> MapDomainToCreateRoutineExerciseResponse(IEnumerable<Exercise> list);

        List<SelectExerciseModel> MapDomainToSelectExerciseModel(List<Exercise> list);
    }
}
