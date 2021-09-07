using ExerciseMS_Application.Mappers;
using ExerciseMS_Application.Queries;
using ExerciseMS_Core.Exceptions;
using ExerciseMS_Core.UoW;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExerciseMS_Application.Handlers.QueryHandlers
{
    public class GetExercisesCountByCategoryHandler : IRequestHandler<GetExercisesCountByCategoryQuery, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<GetExercisesCountByCategoryQuery> _validator;

        public GetExercisesCountByCategoryHandler(IUnitOfWork unitOfWork, IValidator<GetExercisesCountByCategoryQuery> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }
        public async Task<int> Handle(GetExercisesCountByCategoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                await _validator.ValidateAndThrowAsync(request);

                return _unitOfWork.Exercises.GetExerciseCountByCategory(request.CategoryId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
