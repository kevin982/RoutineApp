using ExerciseMS_Core.Dtos;
using ExerciseMS_Core.Exceptions;
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
            try
            {
                if (request is null) throw new ExerciseMSException("The create exercise request can not be null") { StatusCode = 500};

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
                if (exercise is null) throw new ExerciseMSException("The exercise can not be null") { StatusCode = 500 };

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
                if (exercises is null) throw new ExerciseMSException("The exercises can not be null") { StatusCode = 500 };

                if (exercises.Count() == 0) throw new ExerciseMSException("The exercises must contain at least one exercise") { StatusCode = 500 };

                List<DtoExercise> dtos = new();

                foreach (var exercise in exercises) dtos.Add(MapEntityToDto(exercise));

                return dtos;
            }
            catch (Exception)
            {
                throw;
            }
        }



    }
}
