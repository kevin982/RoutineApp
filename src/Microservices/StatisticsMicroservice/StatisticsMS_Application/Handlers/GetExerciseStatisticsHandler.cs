using MediatR;
using StatisticsMS_Application.Mappers;
using StatisticsMS_Application.Queries;
using StatisticsMS_Core.Dtos;
using StatisticsMS_Core.Services;
using StatisticsMS_Core.UoW;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StatisticsMS_Application.Handlers
{
    public class GetExerciseStatisticsHandler : IRequestHandler<GetExerciseStatisticsQuery, IEnumerable<DtoStatistic>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly IStatisticMapper _mapper;

        public GetExerciseStatisticsHandler(IUserService userService, IUnitOfWork unitOfWork, IStatisticMapper mapper)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DtoStatistic>> Handle(GetExerciseStatisticsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Guid userId = new(_userService.GetUserId());

                var statistics = await _unitOfWork.Statistics.GetExerciseStatisticsAsync(userId, request.ExerciseId, request.Year, request.Month);

                return _mapper.MapEntityToDto(statistics);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
