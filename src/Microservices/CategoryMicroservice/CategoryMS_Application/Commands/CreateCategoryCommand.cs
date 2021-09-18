using CategoryMS_Core.Dtos;
using CategoryMS_Core.Models.Requests;
using MediatR;

namespace CategoryMS_Application.Commands
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
