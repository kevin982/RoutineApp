using RoutineApp.Data.Entities;
using RoutineApp.Mappers.Interfaces;
using RoutineApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoutineApp.Mappers.Classes
{
    public class ExerciseMapper:IExerciseMapper
    {
        private readonly IImageMapper _imageMapper = null;
        public ExerciseMapper(IImageMapper imageMapper)
        {
            _imageMapper = imageMapper;
        }

        public ExerciseWorkOutResponseModel MapDomainToExerciseWorkOutResponse(Exercise exercise)
        {
            return new ExerciseWorkOutResponseModel
            {
                ExerciseId = exercise.Id,
                ExerciseName = exercise.Name,
                Images = _imageMapper.MapBitsToImages(exercise.Images)
            };
        }
    }
}
