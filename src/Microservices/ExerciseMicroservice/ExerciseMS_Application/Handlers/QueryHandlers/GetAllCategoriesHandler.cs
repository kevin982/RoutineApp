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
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<DtoCategory>>
    {
        private readonly ICategoryMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCategoriesHandler(ICategoryMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<DtoCategory>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(request.Index, request.Size);

            return _mapper.MapEntityToDto(categories);
        }
    }
}
