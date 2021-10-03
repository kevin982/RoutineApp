using ExerciseMS_Core.Dtos;
using ExerciseMS_Core.Models.Entities;
using ExerciseMS_Core.Models.Requests;
using System.Collections.Generic;

namespace ExerciseMS_Application.Mappers
{
    public interface IExerciseMapper
    {
        DtoExercise MapEntityToDto(Exercise exercise);
        IEnumerable<DtoExercise> MapEntityToDto(IEnumerable<Exercise> exercises);
        Exercise MapRequestToEntity(CreateExerciseRequest request);
        IEnumerable<DtoExerciseSelect> MapEntityToDtoExerciseSelect(IEnumerable<Exercise> exercises);
        DtoExerciseSelect MapEntityToDtoExerciseSelect(Exercise exercise);
    }
}