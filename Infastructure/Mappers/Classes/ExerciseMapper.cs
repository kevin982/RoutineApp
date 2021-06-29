using DomainRoutineApp.Models.Entities;
using DomainRoutineApp.Models.Requests.Exercise;
using DomainRoutineApp.Models.Responses.Exercise;
using RoutineApp.Mappers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureRoutineApp.Mappers.Classes
{
    public class ExerciseMapper : IExerciseMapper
    {
        public Exercise MapCreateExerciseToDomain(CreateExerciseRequestModel model)
        {
            Exercise exercise = new()
            {
                Name = model.Name,
                CategoryId = model.Category
            };

            exercise.Image = model.ImageUrl;

            return exercise;
        }

        public List<CreateRoutineExerciseResponseModel> MapDomainToCreateRoutineExerciseResponse(IEnumerable<Exercise> list)
        {
            List<CreateRoutineExerciseResponseModel> responses = new();

            

            foreach (var exercise in list)
            {
                var response = new CreateRoutineExerciseResponseModel
                {
                    ExerciseId = exercise.Id,
                    ExerciseName = exercise.Name,
                    IsInTheRoutine = exercise.IsInTheRoutine,
                    Category = exercise.Category.CategoryName
                };

                response.Image = exercise.Image;

                responses.Add(response);
            }

            return responses;
        }

        public ExerciseWorkOutResponseModel MapDomainToExerciseWorkOutResponse(Exercise exercise)
        {
            return new ExerciseWorkOutResponseModel
            {
                ExerciseId = exercise.Id,
                ExerciseName = exercise.Name,
                Image = exercise.Image
            };
        }

        public List<SelectExerciseModel> MapDomainToSelectExerciseModel(List<Exercise> list)
        {
            List<SelectExerciseModel> result = new();

            foreach (var exercise in list)
            {
                result.Add(new SelectExerciseModel
                {
                    CategoryName = exercise.Category.CategoryName,
                    ExerciseName = exercise.Name,
                    ExerciseId = exercise.Id
                });
            }

            return result;
        }
    }
}
