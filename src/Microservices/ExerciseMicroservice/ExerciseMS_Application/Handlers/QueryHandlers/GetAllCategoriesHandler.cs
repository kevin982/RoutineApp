using ExerciseMS_Application.Mappers;
using ExerciseMS_Application.Queries;
using ExerciseMS_Core.Dtos;
using ExerciseMS_Core.Exceptions;
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
            try
            {
                if (request is null) throw new ExerciseMSException("The get all categories query can not be null") {StatusCode = 500};

                var categories = await _unitOfWork.Categories.GetAllAsync();

                return _mapper.MapEntityToDto(categories);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
