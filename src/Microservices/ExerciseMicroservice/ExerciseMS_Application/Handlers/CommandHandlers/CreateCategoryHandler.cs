using ExerciseMS_Application.Commands;
using ExerciseMS_Application.Mappers;
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
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryMapper _mapper;

        public CreateCategoryHandler(IUnitOfWork unitOfWork, ICategoryMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            Category category = _mapper.MapRequestToEntity(request.CreateCategoryRequest);

            bool result = await _unitOfWork.Categories.CreateAsync(category);
            
            if(result) await _unitOfWork.CompleteAsync();

            return result;
        }
    }
}
