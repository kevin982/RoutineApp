using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_Application.Queries
{
    public class GetExercisesCountByCategoryQuery : IRequest<int?>
    {
        public Guid UserId { get; init; }
        public Guid CategoryId { get; init; }

        public GetExercisesCountByCategoryQuery(Guid userId, Guid categoryId)
        {
            UserId = userId;
            CategoryId = categoryId;
        }
    }
}
