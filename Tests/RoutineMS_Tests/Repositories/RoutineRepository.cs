using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RoutineMS_Core.Dtos;
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
    public class RoutineRepository
    {
        private UnitOfWork UnitOfWork { get; set; }


        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        public async Task GetAllTheRoutineCorrectly(int day)
        {
            //Arrange
            var context = await FakeData.InitializeContextAsync(true);

            Guid userId = new("72cd9667-8bcd-45c6-bc57-befa1de5422f");

            UnitOfWork = new(context);
 

            List<ExerciseToDoDto> exercisesExpected = new()
            {
                new ExerciseToDoDto() { Id = new Guid("6d333d08-ca22-4865-a998-1f4fd8477970"), Name = "Cable Curl", ImageUrl = "https://www.mensjournal.com/wp-content/uploads/mf/three-way-finisher-chest-main.jpg?w=700&quality=86&strip=all" },
                new ExerciseToDoDto() { Id = new Guid("152ba755-91ee-4dce-a30b-dc33550ca5ee"), Name = "Cable Hammer Curl", ImageUrl = "https://i3.ytimg.com/vi/Xm05VEYT09Y/hqdefault.jpg" },
                new ExerciseToDoDto() { Id = new Guid("471c6e24-ece1-4fcd-9eab-e6ca0e8bd33c"), Name = "3-Way Finisher", ImageUrl = "https://www.mensjournal.com/wp-content/uploads/mf/three-way-finisher-chest-main.jpg?w=700&quality=86&strip=all" },
                new ExerciseToDoDto() { Id = new Guid("fa37f4cb-54e9-44ed-9555-1400c93de5ae"), Name = "Barbell Bench Press", ImageUrl = "https://www.mensjournal.com/wp-content/uploads/mf/_main2_benchpress.jpg?w=700&quality=86&strip=all" }

            };

            //Act and Assert

            await UnitOfWork.SetsDetails.DeleteOldDetailsAsync();
            await UnitOfWork.CompleteAsync();

            int exercise1RepetitionsLeft = 0;
            Guid lastExercise = Guid.Empty;
            int index = -1;

            for (int i = 0; i < 15; i++)
            {
                var result = await UnitOfWork.Routines.GetExerciseToDoFromRoutineAsync(userId, day);

                if ((lastExercise == Guid.Empty) || (lastExercise != result.Id))
                {
                    lastExercise = result.Id;
                    exercise1RepetitionsLeft = result.SetsLeft;
                    index++;
                }

                Assert.Equal(exercisesExpected[index].Id, result.Id);
                Assert.Equal(exercisesExpected[index].Name, result.Name);
                Assert.Equal(exercisesExpected[index].ImageUrl, result.ImageUrl);
                Assert.Equal(exercise1RepetitionsLeft, result.SetsLeft);

                var e = await UnitOfWork.Routines.GetByIdAsync(result.Id);

                if (e.Exercise.SetDetail is not null) e.Exercise.SetDetail.SetsCompleted++;

                if (e.Exercise.SetDetail is null)
                {
                    SetDetail sd = new() { Date = DateTime.UtcNow, SetsCompleted = 1 };
                    await UnitOfWork.SetsDetails.CreateAsync(sd);
                    e.Exercise.SetDetail = sd;
                }

                await UnitOfWork.CompleteAsync();

                exercise1RepetitionsLeft--;

            }

        }

        [Fact]
        public async Task ReturnNullWhereThereIsNoExercisesToDo()
        {

            //Arrange
            var context = await FakeData.InitializeContextAsync(true);

            Guid userId = new("72cd9667-8bcd-45c6-bc57-befa1de5422f");

            UnitOfWork = new(context);
 

            List<ExerciseToDoDto> exercisesExpected = new()
            {
                new ExerciseToDoDto() { Id = new Guid("6d333d08-ca22-4865-a998-1f4fd8477970"), Name = "Cable Curl", ImageUrl = "https://www.mensjournal.com/wp-content/uploads/mf/three-way-finisher-chest-main.jpg?w=700&quality=86&strip=all" },
                new ExerciseToDoDto() { Id = new Guid("152ba755-91ee-4dce-a30b-dc33550ca5ee"), Name = "Cable Hammer Curl", ImageUrl = "https://i3.ytimg.com/vi/Xm05VEYT09Y/hqdefault.jpg" },
                new ExerciseToDoDto() { Id = new Guid("471c6e24-ece1-4fcd-9eab-e6ca0e8bd33c"), Name = "3-Way Finisher", ImageUrl = "https://www.mensjournal.com/wp-content/uploads/mf/three-way-finisher-chest-main.jpg?w=700&quality=86&strip=all" },
                new ExerciseToDoDto() { Id = new Guid("fa37f4cb-54e9-44ed-9555-1400c93de5ae"), Name = "Barbell Bench Press", ImageUrl = "https://www.mensjournal.com/wp-content/uploads/mf/_main2_benchpress.jpg?w=700&quality=86&strip=all" }

            };

            //Act and Assert

            await UnitOfWork.SetsDetails.DeleteOldDetailsAsync();
            await UnitOfWork.CompleteAsync();

            var result = await UnitOfWork.Routines.GetExerciseToDoFromRoutineAsync(userId, 7);

            Assert.Null(result);

        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        public async Task ReturnNullWhereUserDidAllExercises(int day)
        {
            //Arrange
            var context = await FakeData.InitializeContextAsync(true);

            Guid userId = new("72cd9667-8bcd-45c6-bc57-befa1de5422f");

            UnitOfWork = new(context);

            var alldays = await UnitOfWork.Days.GetAllAsync();
            var allExercises = await UnitOfWork.Exercises.GetAllAsync();
            var allSetsDetails = await UnitOfWork.SetsDetails.GetAllAsync();
            var allRoutines = await UnitOfWork.Routines.GetAllAsync();

            List<ExerciseToDoDto> exercisesExpected = new()
            {
                new ExerciseToDoDto() { Id = new Guid("6d333d08-ca22-4865-a998-1f4fd8477970"), Name = "Cable Curl", ImageUrl = "https://www.mensjournal.com/wp-content/uploads/mf/three-way-finisher-chest-main.jpg?w=700&quality=86&strip=all" },
                new ExerciseToDoDto() { Id = new Guid("152ba755-91ee-4dce-a30b-dc33550ca5ee"), Name = "Cable Hammer Curl", ImageUrl = "https://i3.ytimg.com/vi/Xm05VEYT09Y/hqdefault.jpg" },
                new ExerciseToDoDto() { Id = new Guid("471c6e24-ece1-4fcd-9eab-e6ca0e8bd33c"), Name = "3-Way Finisher", ImageUrl = "https://www.mensjournal.com/wp-content/uploads/mf/three-way-finisher-chest-main.jpg?w=700&quality=86&strip=all" },
                new ExerciseToDoDto() { Id = new Guid("fa37f4cb-54e9-44ed-9555-1400c93de5ae"), Name = "Barbell Bench Press", ImageUrl = "https://www.mensjournal.com/wp-content/uploads/mf/_main2_benchpress.jpg?w=700&quality=86&strip=all" }

            };

            //Act

            await UnitOfWork.SetsDetails.DeleteOldDetailsAsync();
            await UnitOfWork.CompleteAsync();

            int exercise1RepetitionsLeft = 0;
            Guid lastExercise = Guid.Empty;
            int index = -1;

            for (int i = 0; i < 15; i++)
            {
                var result = await UnitOfWork.Routines.GetExerciseToDoFromRoutineAsync(userId, day);

                if ((lastExercise == Guid.Empty) || (lastExercise != result.Id))
                {
                    lastExercise = result.Id;
                    exercise1RepetitionsLeft = result.SetsLeft;
                    index++;
                }
                var e = await UnitOfWork.Routines.GetByIdAsync(result.Id);

                if (e.Exercise.SetDetail is not null) e.Exercise.SetDetail.SetsCompleted++;

                if (e.Exercise.SetDetail is null)
                {
                    SetDetail sd = new() { Date = DateTime.UtcNow, SetsCompleted = 1 };
                    await UnitOfWork.SetsDetails.CreateAsync(sd);
                    e.Exercise.SetDetail = sd;
                }

                await UnitOfWork.CompleteAsync();

                exercise1RepetitionsLeft--;

            }

            var nextExercise = await UnitOfWork.Routines.GetExerciseToDoFromRoutineAsync(userId, day);

            //Assert

            Assert.Null(nextExercise);
        }

        [Theory]
        [InlineData("fa37f4cb-54e9-44ed-9555-1400c93de5ae")]
        [InlineData("6d333d08-ca22-4865-a998-1f4fd8477970")]
        public async Task RemoveRoutineCorrectly(string id)
        {
            //Arrange
            Guid exerciseId = new(id);

            Guid userId = new("72cd9667-8bcd-45c6-bc57-befa1de5422f");

            var context = await FakeData.InitializeContextAsync(true);

            UnitOfWork = new(context);

            //Act

            var beforeRemoving = await UnitOfWork.Routines.GetByIdAsync(exerciseId);

            await UnitOfWork.Routines.RemoveRoutineByExerciseAsync(exerciseId, userId);
            await UnitOfWork.CompleteAsync();
 
            Assert.NotNull(beforeRemoving);

            await Assert.ThrowsAsync<RoutineMSException>(async () => await UnitOfWork.Routines.GetByIdAsync(exerciseId));
        }


        [Fact]
        public async Task ThrowExceptionIfRoutineIsNotFoundInRemoveRoutineByExerciseAsync()
        {
            //Arrange
            Guid exerciseId = Guid.NewGuid();

            Guid userId = Guid.NewGuid();

            var context = await FakeData.InitializeContextAsync(true);

            UnitOfWork = new(context);

            //Act

            await Assert.ThrowsAsync<RoutineMSException>(async () => await UnitOfWork.Routines.RemoveRoutineByExerciseAsync(exerciseId, userId));
        }


        [Theory]
        [InlineData("fa37f4cb-54e9-44ed-9555-1400c93de5ae", true)]
        [InlineData("", false)]
        public async Task TheExerciseIsInRoutineWorkCorrectly(string id, bool resultExpected)
        {
            //Arrange
            Guid exerciseId = (string.IsNullOrEmpty(id)) ? Guid.NewGuid() : new Guid(id);

            Guid userId = new("72cd9667-8bcd-45c6-bc57-befa1de5422f");

            var context = await FakeData.InitializeContextAsync(true);

            UnitOfWork = new(context);

            //Act

            bool result = await UnitOfWork.Routines.TheExerciseIsInRoutineAsync(exerciseId, userId);
        
            //Assert

            Assert.Equal(resultExpected, result);
        }
    }

}
