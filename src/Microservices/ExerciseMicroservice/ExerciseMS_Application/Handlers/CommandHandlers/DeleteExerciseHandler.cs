﻿using ExerciseMS_Application.Commands;
using ExerciseMS_Core.Exceptions;
using ExerciseMS_Core.Models.Entities;
using ExerciseMS_Core.UoW;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExerciseMS_Application.Handlers.CommandHandlers
{
    public class DeleteExerciseHandler : IRequestHandler<DeleteExerciseCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<DeleteExerciseCommand> _validator;

        public DeleteExerciseHandler(IUnitOfWork unitOfWork, IValidator<DeleteExerciseCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Guid> Handle(DeleteExerciseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _validator.ValidateAndThrowAsync(request);

                if (request is null) throw new ExerciseMSException("The command to delete the exercise can not be null") { StatusCode = 500};
            
                if (request.ExerciseId == Guid.Empty) throw new ExerciseMSException("The command to delete the exercise can not contain an empty id") { StatusCode = 500};

                Exercise result = await _unitOfWork.Exercises.DeleteAsync(request.ExerciseId);

                await _unitOfWork.CompleteAsync();

                return result.ExerciseId;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
