using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RoutineMS_Core.Exceptions;
using RoutineMS_Core.Models.Entities;
using RoutineMS_Infraestructure.Data;
using RoutineMS_Infraestructure.UoW;
using RoutineMS_Tests.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RoutineMS_Tests.Repositories
{
    public class DayRepositoryShould
    {
        private UnitOfWork unitOfWork;

        private readonly List<Day> Days = new() { new Day() { Id = 1, Name = "Monday"}, new Day() { Id = 2, Name = "Tuesday" }, new Day() { Id = 3, Name = "Wednesday" }, new Day() { Id = 4, Name = "Thursday" }, new Day() { Id = 5, Name = "Friday" }, new Day() { Id = 6, Name = "Saturday" }, new Day() { Id = 7, Name = "Sunday" } };
 

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        public async Task GetDayByDayNumberCorrectly(int number)
        {
            //Arrange

            var expectedDay = Days[number-1];
            var context = await FakeData.InitializeContextAsync(false);
            unitOfWork = new UnitOfWork(context);

            //Act

            var day = await unitOfWork.Days.GetDayByDayNumberAsync(number);

            //Assert

            Assert.Equal(expectedDay.Id, day.Id);
            Assert.Equal(expectedDay.Name, day.Name);
        }

        [Fact]
        public async Task ThrowExceptionIfDayIsNotFound()
        {
            //Arrange

            var context = await FakeData.InitializeContextAsync(false);
            unitOfWork = new UnitOfWork(context);

            //Act


            //Assert

            await Assert.ThrowsAsync<RoutineMSException>(async () => await unitOfWork.Days.GetDayByDayNumberAsync(9));
        }
    }
}
