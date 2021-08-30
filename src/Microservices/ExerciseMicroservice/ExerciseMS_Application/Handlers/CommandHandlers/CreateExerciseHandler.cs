using ExerciseMS_Application.Commands;
using ExerciseMS_Application.Mappers;
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
    public class CreateExerciseHandler : IRequestHandler<CreateExerciseCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExerciseMapper _mapper;

        public CreateExerciseHandler(IUnitOfWork unitOfWork, IExerciseMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
        {
            Exercise exercise = _mapper.MapRequestToEntity(request.CreateExerciseRequest);

            if (exercise == null) return false;

            bool result = await _unitOfWork.Exercises.CreateAsync(exercise);

            if (result) await _unitOfWork.CompleteAsync();

            return result;
        }
    }
}
