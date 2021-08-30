using ExerciseMS_Application.Mappers;
using ExerciseMS_Application.Queries;
using ExerciseMS_Core.Dtos;
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
    public class GetExercisesByCategoryHandler : IRequestHandler<GetExercisesByCategoryQuery, IEnumerable<DtoExercise>>
    {
        private readonly IExerciseMapper _exerciseMapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetExercisesByCategoryHandler(IExerciseMapper exerciseMapper, IUnitOfWork unitOfWork)
        {
            _exerciseMapper = exerciseMapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DtoExercise>> Handle(GetExercisesByCategoryQuery request, CancellationToken cancellationToken)
        {
            var exercises = await _unitOfWork.Exercises.GetAllExercisesByCategoryAsync(request.CategoryId, request.Index, request.Size);

            return _exerciseMapper.MapEntityToDto(exercises);
        }
    }
}
