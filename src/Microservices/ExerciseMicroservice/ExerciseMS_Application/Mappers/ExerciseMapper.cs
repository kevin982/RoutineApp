using ExerciseMS_Core.Dtos;
using ExerciseMS_Core.Models.Entities;
using ExerciseMS_Core.Models.Requests;
using System;
using System.Collections.Generic;

namespace ExerciseMS_Application.Mappers
{
    public class ExerciseMapper : IExerciseMapper
    {
 
        public Exercise MapRequestToEntity(CreateExerciseRequest request)
        {
            try
            {
                return new Exercise
                {
                    ExerciseName = request.Name,
                    CategoryId = request.CategoryId,
                    IsInTheRoutine = false,
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DtoExercise MapEntityToDto(Exercise exercise)
        {
            try
            {

                return new DtoExercise
                {
                    Id = exercise.ExerciseId,
                    ExerciseName = exercise.ExerciseName,
                    ImageUrl = exercise.ImageUrl,
                    IsInTheRoutine = exercise.IsInTheRoutine
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<DtoExercise> MapEntityToDto(IEnumerable<Exercise> exercises)
        {
            try
            {
                List<DtoExercise> dtos = new();

                foreach (var exercise in exercises) dtos.Add(MapEntityToDto(exercise));

                return dtos;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<DtoExerciseSelect> MapEntityToDtoExerciseSelect(IEnumerable<Exercise> exercises)
        {
            List<DtoExerciseSelect> result = new();

            foreach (var exercise in exercises)
            {
                result.Add(MapEntityToDtoExerciseSelect(exercise));
            }
            
            return result;
        }
        
        public DtoExerciseSelect MapEntityToDtoExerciseSelect(Exercise exercise)
        {
            return new DtoExerciseSelect()
            {
                ExerciseId = exercise.ExerciseId,
                ExerciseName = exercise.ExerciseName
            };
        }

    }
}
