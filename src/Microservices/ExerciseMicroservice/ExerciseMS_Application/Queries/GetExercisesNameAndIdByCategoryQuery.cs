using System;
using System.Collections.Generic;
using ExerciseMS_Core.Dtos;
using MediatR;

namespace ExerciseMS_Application.Queries
{
    public class GetExercisesNameAndIdByCategoryQuery : IRequest<IEnumerable<DtoExerciseSelect>>
    {
        public Guid CategoryId { get; set; }

        public GetExercisesNameAndIdByCategoryQuery(Guid categoryId)
        {
            CategoryId = categoryId;
        }
    }
}