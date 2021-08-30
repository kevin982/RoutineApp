using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_Application.Queries
{
    public class GetExerciseCountByCategoryQuery : IRequest<int>
    {
        public Guid UserId { get; init; }
        public Guid CategoryId { get; init; }

        public GetExerciseCountByCategoryQuery(Guid userId, Guid categoryId)
        {
            UserId = userId;
            CategoryId = categoryId;
        }
    }
}
