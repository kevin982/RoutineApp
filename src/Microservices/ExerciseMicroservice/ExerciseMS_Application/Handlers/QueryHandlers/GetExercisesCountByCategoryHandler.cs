using ExerciseMS_Application.Mappers;
using ExerciseMS_Application.Queries;
using ExerciseMS_Core.UoW;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExerciseMS_Application.Handlers.QueryHandlers
{
    public class GetExercisesCountByCategoryHandler : IRequestHandler<GetExercisesCountByCategoryQuery, int?>
    {
        private readonly IExerciseMapper _exerciseMapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetExercisesCountByCategoryHandler(IExerciseMapper exerciseMapper, IUnitOfWork unitOfWork)
        {
            _exerciseMapper = exerciseMapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<int?> Handle(GetExercisesCountByCategoryQuery request, CancellationToken cancellationToken)
        {
            int? count = _unitOfWork.Exercises.GetExerciseCountByCategory(request.CategoryId);

            return count;
        }
    }
}
