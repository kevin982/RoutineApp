using RoutineApp.Data.Entities;
using RoutineApp.Models;

namespace RoutineApp.Mappers.Interfaces
{
    public interface IExerciseMapper
    {
        ExerciseWorkOutResponseModel MapDomainToExerciseWorkOutResponse(Exercise exercise);
    }
}
