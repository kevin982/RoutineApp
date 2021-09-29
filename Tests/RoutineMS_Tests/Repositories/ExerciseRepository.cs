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
    public class ExerciseRepository
    {
        private UnitOfWork unitOfWork;
 
        [Fact]
        public async Task GetCorrectExerciseById()
        {
            //Arrange
             
            var context = await FakeData.InitializeContextAsync(true);
            var exerciseExpected = new Exercise() { Id = new Guid("fa37f4cb-54e9-44ed-9555-1400c93de5ae"), Name = "Barbell Bench Press", CategoryName = "Chest", ImageUrl = "https://www.mensjournal.com/wp-content/uploads/mf/_main2_benchpress.jpg?w=700&quality=86&strip=all", SetDetail = new SetDetail() { Id = new Guid("915e2e48-196a-47d1-8f9f-a17857a96893"), Date = DateTime.UtcNow, SetsCompleted = 2 } };
            unitOfWork = new UnitOfWork(context);

            //Act

            var exercise = await unitOfWork.Exercises.GetByIdAsync(new Guid("fa37f4cb-54e9-44ed-9555-1400c93de5ae"));

            //Assert

            Assert.Equal(exerciseExpected.Id, exercise.Id);
            Assert.Equal(exerciseExpected.CategoryName, exercise.CategoryName);
            Assert.Equal(exerciseExpected.Name, exercise.Name);
            Assert.Equal(exerciseExpected.ImageUrl, exercise.ImageUrl);
            Assert.Equal(exerciseExpected.SetDetail.Id, exercise.SetDetail.Id);
            Assert.Equal(exerciseExpected.SetDetail.Date.Year, exercise.SetDetail.Date.Year);
            Assert.Equal(exerciseExpected.SetDetail.Date.Month, exercise.SetDetail.Date.Month);
            Assert.Equal(exerciseExpected.SetDetail.Date.Day, exercise.SetDetail.Date.Day);
            Assert.Equal(exerciseExpected.SetDetail.SetsCompleted, exercise.SetDetail.SetsCompleted);

        }

        [Fact]
        public async Task ThrowExceptionIfExercisesDoesNotExist()
        {
            //Arrange

            var context = await FakeData.InitializeContextAsync(true);
            unitOfWork = new UnitOfWork(context);

            //Act

            //Assert

            await Assert.ThrowsAsync<RoutineMSException>(async () => await unitOfWork.Exercises.GetByIdAsync(Guid.NewGuid()));

        }
    }
}
