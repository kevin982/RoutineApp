using ExerciseMS_Core.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_Application.Queries
{
    public class GetExercisesByCategoryQuery : IRequest<IEnumerable<DtoExercise>>
    {
        public Guid CategoryId { get; init; }
        public Guid UserId { get; init; }
        public int Index { get; init; }
        public int Size { get; init; }

        public GetExercisesByCategoryQuery(Guid categoryId, Guid userId, int index, int size)
        {
            CategoryId = categoryId;
            UserId = userId;
            Index = index;
            Size = size;
        }

    }
}
