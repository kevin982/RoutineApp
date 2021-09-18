using CategoryMS_Application.Commands;
using CategoryMS_Core.Dtos;
using CategoryMS_Core.Models.Entities;
using CategoryMS_Core.UoW;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CategoryMS_Application.Handlers.CommandHandlers
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, DtoCategory>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateCategoryCommand> _validator;

        public CreateCategoryHandler(IUnitOfWork unitOfWork, IValidator<CreateCategoryCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<DtoCategory> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _validator.ValidateAndThrowAsync(request);

                Category category = new() { CategoryName = request.CreateCategoryRequest.CategoryName};

                Category result = await _unitOfWork.Categories.CreateAsync(category);

                await _unitOfWork.CompleteAsync();

                return new DtoCategory() { CategoryId = result.CategoryId, CategoryName = result.CategoryName };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
