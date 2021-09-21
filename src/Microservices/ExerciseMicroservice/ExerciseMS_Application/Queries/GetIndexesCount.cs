using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_Application.Queries
{
    public class GetIndexesCount : IRequest<int>
    {

        public Guid CategoryId { get; init; }
        public int Size { get; init; }

        public GetIndexesCount(Guid categoryId, int size)
        {
            CategoryId = categoryId;
            Size = size;
        }
    }
}
