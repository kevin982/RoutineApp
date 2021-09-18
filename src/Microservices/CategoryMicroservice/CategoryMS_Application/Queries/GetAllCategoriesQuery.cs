using CategoryMS_Core.Dtos;
using MediatR;
using System.Collections.Generic;

namespace CategoryMS_Application.Queries
{
    public class GetAllCategoriesQuery : IRequest<IEnumerable<DtoCategory>>
    {
    }
}
