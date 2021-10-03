using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ExerciseMS_Application.Mappers;
using ExerciseMS_Application.Queries;
using ExerciseMS_Core.Dtos;
using ExerciseMS_Core.Services;
using ExerciseMS_Core.UoW;
using MediatR;

namespace ExerciseMS_Application.Handlers.QueryHandlers
{
    public class GetExercisesNameAndIdByCategoryHandler : IRequestHandler<GetExercisesNameAndIdByCategoryQuery, IEnumerable<DtoExerciseSelect>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly IExerciseMapper _mapper;

        public GetExercisesNameAndIdByCategoryHandler(IUnitOfWork unitOfWork, IUserService userService, IExerciseMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<DtoExerciseSelect>> Handle(GetExercisesNameAndIdByCategoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Guid userId = new(_userService.GetUserId());

                var result =
                    await _unitOfWork.Exercises.GetExercisesNameAndIdByCategoryAsync(userId, request.CategoryId);

                return _mapper.MapEntityToDtoExerciseSelect(result);

            }
            catch (Exception)
            {
                throw;
            }
        }
 
    }
}