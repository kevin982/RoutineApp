using CategoryMS_Application.Queries;
using CategoryMS_Core.Dtos;
using CategoryMS_Core.Exceptions;
using CategoryMS_Core.UoW;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CategoryMS_Application.Handlers.QueryHandlers
{
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<DtoCategory>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCategoriesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<DtoCategory>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null) throw new CategoryMSException("The get all categories query can not be null") { StatusCode = 500 };

                var categories = await _unitOfWork.Categories.GetAllAsync();

                List<DtoCategory> result = new();

                foreach (var category in categories) result.Add(new DtoCategory(){CategoryId = category.CategoryId, CategoryName = category.CategoryName });

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
