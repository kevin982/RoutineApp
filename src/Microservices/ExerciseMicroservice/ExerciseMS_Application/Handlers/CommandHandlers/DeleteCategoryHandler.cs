﻿using ExerciseMS_Application.Commands;
using ExerciseMS_Core.Dtos;
using ExerciseMS_Core.Models.Entities;
using ExerciseMS_Core.UoW;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExerciseMS_Application.Handlers.CommandHandlers
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        { 
            Category result =  await _unitOfWork.Categories.DeleteAsync(request.CategoryId);

            await _unitOfWork.CompleteAsync();

            return result.CategoryId;
        }
    }
}
