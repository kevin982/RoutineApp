using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StatisticsMS_Core.Exceptions;
using StatisticsMS_Core.Models.Entities;
using StatisticsMS_Core.UoW;
using StatisticsMS_Infraestructure.UoW;
using StatisticsMS_Tests.Data;
using Xunit;

namespace StatisticsMS_Tests.Repository
{
    public class StatisticsRepository
    {
        private UnitOfWork unitOfWork{ get; set; }

        [Fact]
        public async Task GetTheAllTheExerciseStatisticsCorrectly()
        {
            //Arrange

            List<Statistic> statisticsExpected = new()
            {
                new()
                {
                    Id = Guid.NewGuid(), UserId = new Guid("72cd9667-8bcd-45c6-bc57-befa1de5422f"),
                    ExerciseId = new Guid("6d333d08-ca22-4865-a998-1f4fd8477970"), Weight = 60, Repetitions = 10,
                    DayDone = new DateTime(2021, 8, 29)
                },
                new()
                {
                    Id = Guid.NewGuid(), UserId = new Guid("72cd9667-8bcd-45c6-bc57-befa1de5422f"),
                    ExerciseId = new Guid("6d333d08-ca22-4865-a998-1f4fd8477970"), Weight = 70, Repetitions = 12,
                    DayDone = new DateTime(2021, 8, 29)
                },
                new()
                {
                    Id = Guid.NewGuid(), UserId = new Guid("72cd9667-8bcd-45c6-bc57-befa1de5422f"),
                    ExerciseId = new Guid("6d333d08-ca22-4865-a998-1f4fd8477970"), Weight = 60, Repetitions = 8,
                    DayDone = new DateTime(2021, 12, 29)
                },
                new()
                {
                    Id = Guid.NewGuid(), UserId = new Guid("72cd9667-8bcd-45c6-bc57-befa1de5422f"),
                    ExerciseId = new Guid("6d333d08-ca22-4865-a998-1f4fd8477970"), Weight = 55, Repetitions = 8,
                    DayDone = new DateTime(2020, 7, 29)
                }
            };

            statisticsExpected = statisticsExpected.OrderBy(s => s.DayDone).ToList();
            
            Guid userId = new("72cd9667-8bcd-45c6-bc57-befa1de5422f");
            Guid exerciseId = new("6d333d08-ca22-4865-a998-1f4fd8477970");
            int year = 0;
            int month = 0;
            
            var context = await FakeData.InitializeContextAsync(true);

            unitOfWork = new(context);

            //Act

            var result = await unitOfWork.Statistics.GetExerciseStatisticsAsync(userId, exerciseId, year, month);

            var statistics = result.ToList();
            
            //Assert

            for (int i = 0; i < statistics.Count; i++)
            {
                
                Assert.Equal(statisticsExpected[i].UserId, statistics[i].UserId);
                Assert.Equal(statisticsExpected[i].ExerciseId, statistics[i].ExerciseId);
                Assert.Equal(statisticsExpected[i].Repetitions, statistics[i].Repetitions);
                Assert.Equal(statisticsExpected[i].Weight, statistics[i].Weight);
                Assert.Equal(statisticsExpected[i].DayDone.Year, statistics[i].DayDone.Year);
                Assert.Equal(statisticsExpected[i].DayDone.Month, statistics[i].DayDone.Month);
                Assert.Equal(statisticsExpected[i].DayDone.Day, statistics[i].DayDone.Day);
            }
        }
        
        [Fact]
        public async Task GetTheAllTheExerciseStatisticsForAPeriodOfTimeCorrectly()
        {
            //Arrange

            List<Statistic> statisticsExpected = new()
            {
                new()
                {
                    Id = Guid.NewGuid(), UserId = new Guid("72cd9667-8bcd-45c6-bc57-befa1de5422f"),
                    ExerciseId = new Guid("6d333d08-ca22-4865-a998-1f4fd8477970"), Weight = 60, Repetitions = 10,
                    DayDone = new DateTime(2021, 8, 29)
                },
                new()
                {
                    Id = Guid.NewGuid(), UserId = new Guid("72cd9667-8bcd-45c6-bc57-befa1de5422f"),
                    ExerciseId = new Guid("6d333d08-ca22-4865-a998-1f4fd8477970"), Weight = 70, Repetitions = 12,
                    DayDone = new DateTime(2021, 8, 29)
                }
            };

            statisticsExpected = statisticsExpected.OrderBy(s => s.DayDone).ToList();
            
            Guid userId = new("72cd9667-8bcd-45c6-bc57-befa1de5422f");
            Guid exerciseId = new("6d333d08-ca22-4865-a998-1f4fd8477970");
            int year = 2021;
            int month = 8;
            
            var context = await FakeData.InitializeContextAsync(true);

            unitOfWork = new(context);

            //Act

            var result = await unitOfWork.Statistics.GetExerciseStatisticsAsync(userId, exerciseId, year, month);

            var statistics = result.ToList();
            
            //Assert

            for (int i = 0; i < statistics.Count; i++)
            {
                
                Assert.Equal(statisticsExpected[i].UserId, statistics[i].UserId);
                Assert.Equal(statisticsExpected[i].ExerciseId, statistics[i].ExerciseId);
                Assert.Equal(statisticsExpected[i].Repetitions, statistics[i].Repetitions);
                Assert.Equal(statisticsExpected[i].Weight, statistics[i].Weight);
                Assert.Equal(statisticsExpected[i].DayDone.Year, statistics[i].DayDone.Year);
                Assert.Equal(statisticsExpected[i].DayDone.Month, statistics[i].DayDone.Month);
                Assert.Equal(statisticsExpected[i].DayDone.Day, statistics[i].DayDone.Day);
            }
        }
        
        
        [Fact]
        public async Task ThrowExceptionIfStatisticsIsEmpty()
        {
            //Arrange

            Guid userId = new("288285c8-30c8-4c90-958e-127fe2104caa");
            Guid exerciseId = new("6d333d08-ca22-4865-a998-1f4fd8477970");
            int year = 0;
            int month = 0;
            
            var context = await FakeData.InitializeContextAsync(true);

            unitOfWork = new(context);

            //Act
            
            //Assert

            await Assert.ThrowsAsync<StatisticsMSException>(async () =>
                await unitOfWork.Statistics.GetExerciseStatisticsAsync(userId, exerciseId, year, month));
        }

        [Fact]
        public async Task ThrowExceptionIfThereAreNoStatisticsForASpecificPeriodOfTime()
        {
            //Arrange

            Guid userId = new("72cd9667-8bcd-45c6-bc57-befa1de5422f");
            Guid exerciseId = new("6d333d08-ca22-4865-a998-1f4fd8477970");
            int year = 2015;
            int month = 5;
            
            var context = await FakeData.InitializeContextAsync(true);

            unitOfWork = new(context);

            //Act
            
            //Assert

            await Assert.ThrowsAsync<StatisticsMSException>(async () =>
                await unitOfWork.Statistics.GetExerciseStatisticsAsync(userId, exerciseId, year, month));
        }
    }
}