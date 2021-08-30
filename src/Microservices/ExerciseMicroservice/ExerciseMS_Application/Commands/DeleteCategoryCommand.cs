using ExerciseMS_Core.Models.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_Application.Commands
{
    public class DeleteCategoryCommand : IRequest<bool>
    {
        public Guid CategoryId { get; init; }

        public DeleteCategoryCommand(Guid categoryId)
        {
            CategoryId = categoryId;
        }
    }
}
