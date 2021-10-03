using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using StatisticsMS_Application.Handlers;
using StatisticsMS_Application.Mappers;
using StatisticsMS_Application.Queries;
using StatisticsMS_Core.Dtos;
using StatisticsMS_Core.Models.Entities;
using StatisticsMS_Core.Services;
using StatisticsMS_Core.UoW;
using Xunit;

namespace StatisticsMS_Tests.Handlers
{
    public class GetExerciseStatisticsHandlerShould
    {
        private GetExerciseStatisticsHandler handler { get; set; }

        private Mock<IUnitOfWork> _unitOfWork = new();
        private Mock<IUserService> _userService = new();
        private Mock<IStatisticMapper> _statisticMapper = new();

        [Fact]
        public async Task CallTheCorrectMethods()
        {
            //Arrange

            GetExerciseStatisticsQuery query = new(Guid.NewGuid(), 2, 2020);
            CancellationToken token = new();
            
            //Act


            _userService.Setup(u => u.GetUserId()).Returns(Guid.NewGuid().ToString());
            _unitOfWork.Setup(u =>
                u.Statistics.GetExerciseStatisticsAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<int>(),
                    It.IsAny<int>())).ReturnsAsync(new List<Statistic>());

            _statisticMapper.Setup(s => s.MapEntityToDto(It.IsAny <List<Statistic>>()))
                .Returns(It.IsAny<List<DtoStatistic>>());

            handler = new(_userService.Object, _unitOfWork.Object, _statisticMapper.Object);

            await handler.Handle(query, token);
            
            //Assert
            
            
            _userService.Verify(u => u.GetUserId(), Times.Once());
            _unitOfWork.Verify(u =>
                u.Statistics.GetExerciseStatisticsAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<int>(),
                    It.IsAny<int>()), Times.Once());

            _statisticMapper.Verify(s => s.MapEntityToDto(It.IsAny <List<Statistic>>()), Times.Once());
        }
    }
}