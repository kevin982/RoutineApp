using ExerciseMS_Core.Dtos;
using ExerciseMS_Core.Models.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_Application.Commands
{
    public class CreateCategoryCommand : IRequest<DtoCategory>
    {
        public CreateCategoryRequest CreateCategoryRequest { get; set; }

        public CreateCategoryCommand(CreateCategoryRequest createCategoryRequest)
        {
            CreateCategoryRequest = createCategoryRequest;
        }
    }
}
