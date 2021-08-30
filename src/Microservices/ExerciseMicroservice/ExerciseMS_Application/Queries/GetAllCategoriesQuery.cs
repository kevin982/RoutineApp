using ExerciseMS_Core.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_Application.Queries
{
    public class GetAllCategoriesQuery : IRequest<IEnumerable<DtoCategory>>
    {   
    }
}
