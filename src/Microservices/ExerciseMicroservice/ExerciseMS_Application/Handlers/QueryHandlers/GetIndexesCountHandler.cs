using ExerciseMS_Application.Queries;
using ExerciseMS_Core.UoW;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ExerciseMS_Application.Handlers.QueryHandlers
{
    public class GetIndexesCountHandler : IRequestHandler<GetIndexesCount, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<GetIndexesCount> _validator;

        public GetIndexesCountHandler(IUnitOfWork unitOfWork, IValidator<GetIndexesCount> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }
        public async Task<int> Handle(GetIndexesCount request, CancellationToken cancellationToken)
        {
            try
            {
                await _validator.ValidateAndThrowAsync(request);

                return _unitOfWork.Exercises.GetIndexesCount(request.CategoryId, request.Size);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
