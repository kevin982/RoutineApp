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

            foreach (var image in model.ImagesUrl) exercise.Images.Add(new Image { Img = image });

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

                foreach (var image in exercise.Images) response.Images.Add(image.Img);

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
                Images = exercise.Images
            };
        }
    }
}
