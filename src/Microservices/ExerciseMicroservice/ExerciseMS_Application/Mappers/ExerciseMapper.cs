using ExerciseMS_Core.Dtos;
using ExerciseMS_Core.Models.Entities;
using ExerciseMS_Core.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_Application.Mappers
{
    public class ExerciseMapper : IExerciseMapper
    {
        public Exercise MapRequestToEntity(CreateExerciseRequest request)
        {
            if (request is null) return null;

            return new Exercise
            {
                ExerciseName = request.Name,
                CategoryId = request.CategoryId,
                IsInTheRoutine = false,
            };
        }

        public DtoExercise MapEntityToDto(Exercise exercise)
        {
            if (exercise is null) return null;

            return new DtoExercise
            {
                Id = exercise.ExerciseId,
                ExerciseName = exercise.ExerciseName,
                ImageUrl = exercise.ImageUrl,
                IsInTheRoutine = exercise.IsInTheRoutine
            };
        }

        public IEnumerable<DtoExercise> MapEntityToDto(IEnumerable<Exercise> exercises)
        {
            if (exercises is null) return null;

            if (exercises.Count() == 0) return null;

            List<DtoExercise> dtos = new();

            foreach (var exercise in exercises) dtos.Add(MapEntityToDto(exercise));

            return dtos;
        }



    }
}
