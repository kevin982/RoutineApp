using ExerciseMS_Application.Commands;
using ExerciseMS_Application.Mappers;
using ExerciseMS_Core.Dtos;
using ExerciseMS_Core.Exceptions;
using ExerciseMS_Core.Models.Entities;
using ExerciseMS_Core.UoW;
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

        public CreateCategoryHandler(IUnitOfWork unitOfWork, ICategoryMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DtoCategory> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            Category category = _mapper.MapRequestToEntity(request.CreateCategoryRequest);

            if (category is null) throw new ExerciseMSException("The create category request model can not be null") { StatusCode = 500};

            Category result = await _unitOfWork.Categories.CreateAsync(category);
            
            await _unitOfWork.CompleteAsync();

            return _mapper.MapEntityToDto(result);
        }
    }
}
