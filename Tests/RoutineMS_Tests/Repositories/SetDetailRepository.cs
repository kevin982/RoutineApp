using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
    public class SetDetailRepository
    {
        private UnitOfWork unitOfWork;

        [Fact]
        public async Task DeleteOldDetailsCorrectly() 
        {
            //Arrange
            var resultExpected =  new SetDetail() { Id = new Guid("915e2e48-196a-47d1-8f9f-a17857a96893"), Date = DateTime.UtcNow, SetsCompleted = 2 };
            var context = await FakeData.InitializeContextAsync(true);
            unitOfWork= new UnitOfWork(context);

            //Act

            await unitOfWork.SetsDetails.DeleteOldDetailsAsync();
            await unitOfWork.CompleteAsync();
            var result = await unitOfWork.SetsDetails.GetAllAsync();

            //Assert

            Assert.True(result.Count() == 1);
            Assert.Equal(resultExpected.Id, result.First().Id);
            Assert.Equal(resultExpected.SetsCompleted, result.First().SetsCompleted);
            Assert.Equal(resultExpected.Date.Year, result.First().Date.Year);
            Assert.Equal(resultExpected.Date.Month, result.First().Date.Month);
            Assert.Equal(resultExpected.Date.Day, result.First().Date.Day);
        }
    }
}
