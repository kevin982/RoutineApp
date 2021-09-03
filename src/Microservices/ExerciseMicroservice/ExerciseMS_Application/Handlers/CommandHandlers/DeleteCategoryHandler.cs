using ExerciseMS_Application.Commands;
using ExerciseMS_Core.Dtos;
using ExerciseMS_Core.Exceptions;
using ExerciseMS_Core.Models.Entities;
using ExerciseMS_Core.UoW;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExerciseMS_Application.Handlers.CommandHandlers
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<DeleteCategoryCommand> _validator;

        public DeleteCategoryHandler(IUnitOfWork unitOfWork, IValidator<DeleteCategoryCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Guid> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _validator.ValidateAndThrowAsync(request);

                Category result = await _unitOfWork.Categories.DeleteAsync(request.CategoryId);

                await _unitOfWork.CompleteAsync();

                return result.CategoryId;
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
