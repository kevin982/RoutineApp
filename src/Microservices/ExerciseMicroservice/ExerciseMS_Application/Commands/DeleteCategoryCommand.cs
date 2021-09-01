using ExerciseMS_Core.Dtos;
using MediatR;
using System;

namespace ExerciseMS_Application.Commands
{
    public class DeleteCategoryCommand : IRequest<Guid>
    {
        public Guid CategoryId { get; init; }

        public DeleteCategoryCommand(Guid categoryId)
        {
            CategoryId = categoryId;
        }
    }
}
