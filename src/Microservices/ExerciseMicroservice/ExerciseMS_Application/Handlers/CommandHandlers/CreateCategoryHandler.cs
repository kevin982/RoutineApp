using ExerciseMS_Application.Commands;
using ExerciseMS_Application.Mappers;
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
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, DtoCategory>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryMapper _mapper;
        private readonly IValidator<CreateCategoryCommand> _validator;

        public CreateCategoryHandler(IUnitOfWork unitOfWork, ICategoryMapper mapper, IValidator<CreateCategoryCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<DtoCategory> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _validator.ValidateAndThrowAsync(request);

                Category category = _mapper.MapRequestToEntity(request.CreateCategoryRequest);

                Category result = await _unitOfWork.Categories.CreateAsync(category);
            
                await _unitOfWork.CompleteAsync();

                return _mapper.MapEntityToDto(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
