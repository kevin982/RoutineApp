using ExerciseMS_Core.Exceptions;
using ExerciseMS_Core.Models.Entities;
using ExerciseMS_Core.Repositories;
using ExerciseMS_Core.Services;
using ExerciseMS_Infraestructure.Data;
using ExerciseMS_Infraestructure.Repositories;
using ExerciseMS_Infraestructure.UoW;
using ExerciseMS_Tests.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ExerciseMS_Tests.Repositories
{
    
    public class ExerciseRepositoryShould
    {

        private UnitOfWork unitOfWork;

        private Mock<IUserService> _userService = new();
 

        #region AddExerciseMethod

        private async Task<ExerciseMsDbContext> InitializeContextAsync(bool seed)
        {
            var options = new DbContextOptionsBuilder<ExerciseMsDbContext>()
                .UseInMemoryDatabase(databaseName: $"ExercisePrueba{Guid.NewGuid()}")
                .Options;

            var context = new ExerciseMsDbContext(options);

            if(seed) await FakeData.SeedFakeData(context);

            return context;
        }

        [Fact]
        public async Task ThrowExceptionIfExerciseIsNullInAddExerciseMethod()
        {
            //Arrange
 
            var context = await InitializeContextAsync(false);

            Exercise exercise = null;

            unitOfWork = new(context, _userService.Object);

            //Act

            //Assert

            await Assert.ThrowsAsync<ExerciseMSException>(async () => await unitOfWork.Exercises.CreateAsync(exercise));
        }
        
        [Fact]
        public async Task CreateExerciseCorrectlyInAddExerciseMethod()
        {
            //Arrange
             
            var context = await InitializeContextAsync(false);

            Exercise exercise = new Exercise() { ExerciseId = new Guid("16084a62-683a-469e-bfef-03bc2a7ab933"), UserId = new Guid("14a64295-658e-4279-8669-e1a2f801fe72") { }, CategoryId = new Guid("0704c4b1-0481-409f-a053-0088a7b6d0c6"), ExerciseName = "Exercise test", IsInTheRoutine = false, ImageUrl = "https://someimage.jpg"};

            unitOfWork = new(context, _userService.Object);

            //Act

            await unitOfWork.Exercises.CreateAsync(exercise);
            await unitOfWork.CompleteAsync();

            var result = await unitOfWork.Exercises.GetByIdAsync(exercise.ExerciseId);

            //Assert

            Assert.Equal(exercise, result);
        }
        #endregion

        #region GetExerciseById

        [Fact]
        public async Task ThrowExceptionIfExerciseIdIsEmptyInGetExerciseById()
        {
            //Arrange

            var context = await InitializeContextAsync(true);

            Guid exerciseId = Guid.Empty;

            unitOfWork = new(context, _userService.Object);

            //Act

            //Assert

            await Assert.ThrowsAsync<ExerciseMSException>(async () => await unitOfWork.Exercises.GetByIdAsync(exerciseId));
        }

        [Fact]
        public async Task GetTheCorrectExerciseByIdInGetExerciseById()
        {
            //Arrange
 
            var context = await InitializeContextAsync(true);

            Guid exerciseId = Guid.Empty;

            unitOfWork = new(context, _userService.Object);

            Exercise exerciseToExpect = FakeData.FakeExercises().ToList()[0];

            //Act

            var result = await unitOfWork.Exercises.GetByIdAsync(new Guid("aa12e9f1-32d9-4914-8824-908a87482387"));

            //Assert

            Assert.Equal(exerciseToExpect.ExerciseId, result.ExerciseId);
            Assert.Equal(exerciseToExpect.CategoryId, result.CategoryId);
            Assert.Equal(exerciseToExpect.UserId, result.UserId);
            Assert.Equal(exerciseToExpect.ExerciseName, result.ExerciseName);
            Assert.Equal(exerciseToExpect.IsInTheRoutine, result.IsInTheRoutine);
            Assert.Equal(exerciseToExpect.ImageUrl, result.ImageUrl);
        }


        #endregion

        #region GetExerciseByCategory

        [Theory]
        [InlineData("", 5)]
        [InlineData("85f9fac1-e9ea-43ae-b0ef-64759aed4e02", 0)]
        public async Task ThrowExceptionIfCategoryIdIsEmptyOrSizeIsZeroInGetExerciseByCategory(string id, int size)
        {
            //Arrange

            var context = await InitializeContextAsync(false);

            unitOfWork = new(context, _userService.Object);

            var categoryId = (string.IsNullOrEmpty(id)) ?Guid.Empty:new Guid(id);

            //Act

            //Assert

            await Assert.ThrowsAsync<ExerciseMSException>(async () => await unitOfWork.Exercises.GetAllExercisesByCategoryAsync(categoryId, 0, size));
        }

        [Fact]
        public async Task ThrowExceptionIfThereAreNotExercisesInGetExerciseByCategory()
        {
            //Arrange

            var context = await InitializeContextAsync(false);

            Guid categoryId = new("5472fa2e-cd04-41e2-8b50-25b9120be7fc");
            int index = 1, size = 5;

            //Act

            _userService.Setup(u => u.GetUserId()).Returns("5ec4913e-2c1c-430a-a566-91a3334fdaec");

            unitOfWork = new(context, _userService.Object);
            //Assert

            await Assert.ThrowsAsync<ExerciseMSException>(async () => await unitOfWork.Exercises.GetAllExercisesByCategoryAsync(categoryId, index, size));
        }

        [Fact]
        public async Task GetTheCorrectExercisesByCategoryInGetExerciseByCategory()
        {
            //Arrange
 
            var context = await InitializeContextAsync(true);

            List<Exercise> exercisesExpected = new() { FakeData.FakeExercises().ToList()[0], FakeData.FakeExercises().ToList()[1] };

            Guid categoryId = new("5f5b42b2-e14d-4824-8d93-3977e3355f01");
            int index = 0, size = 5;

            //Act

            _userService.Setup(u => u.GetUserId()).Returns("4de0dd76-08b1-4f81-85e6-33693ca836e3");

            unitOfWork = new(context, _userService.Object);

            var result = await unitOfWork.Exercises.GetAllExercisesByCategoryAsync(categoryId, index, size);

            var resultExercises = result.ToList();

            //Assert

            for (int i = 0; i < result.Count(); i++)
            {
                Assert.Equal(exercisesExpected[i].ExerciseId, resultExercises[i].ExerciseId);
                Assert.Equal(exercisesExpected[i].CategoryId, resultExercises[i].CategoryId);
                Assert.Equal(exercisesExpected[i].UserId, resultExercises[i].UserId);
                Assert.Equal(exercisesExpected[i].ExerciseName, resultExercises[i].ExerciseName);
                Assert.Equal(exercisesExpected[i].IsInTheRoutine, resultExercises[i].IsInTheRoutine);
                Assert.Equal(exercisesExpected[i].ImageUrl, resultExercises[i].ImageUrl);
            }
            
        }

        #endregion

        #region GetExerciseCountByCategory

        [Fact]
        public async Task ThrowExceptionIfCategoryIsEmptyInGetExerciseCountByCategory()
        {
            //Arrange
 
            var context = await InitializeContextAsync(false);

            unitOfWork = new(context, _userService.Object);

            var categoryId = Guid.Empty;

            //Act

            //Assert

            Assert.Throws<ExerciseMSException>(() => unitOfWork.Exercises.GetExerciseCountByCategory(categoryId));
        }

        [Fact]
        public async Task ThrowExceptionIfCountIsZeroInGetExerciseCountByCategory()
        {
            //Arrange
 
            var context = await InitializeContextAsync(false);


            var categoryId = Guid.NewGuid();

            //Act
            _userService.Setup(u => u.GetUserId()).Returns(Guid.NewGuid().ToString());

            unitOfWork = new(context, _userService.Object);

            //Assert

            Assert.Throws<ExerciseMSException>(() => unitOfWork.Exercises.GetExerciseCountByCategory(categoryId));
        }

        [Fact]
        public async Task GetTheCorrectExercisesCountInGetExerciseCountByCategory()
        {
            //Arrange
 
            var context = await InitializeContextAsync(true);

            List<Exercise> exercisesExpected = new() { FakeData.FakeExercises().ToList()[0], FakeData.FakeExercises().ToList()[1] };

            Guid categoryId = new("5f5b42b2-e14d-4824-8d93-3977e3355f01");
            int index = 0, size = 5;

            //Act

            _userService.Setup(u => u.GetUserId()).Returns("4de0dd76-08b1-4f81-85e6-33693ca836e3");

            unitOfWork = new(context, _userService.Object);

            var result = unitOfWork.Exercises.GetExerciseCountByCategory(categoryId);

            //Assert

            Assert.Equal(2, result);
        }

        #endregion

        #region DeleteExercise

        [Fact]
        public async Task ThrowExceptionIfExerciseIdIsEmtpyInDeleteExercise()
        {
            //Arrange
            
            var context = await InitializeContextAsync(false);

            unitOfWork = new(context, _userService.Object);

            var exerciseId = Guid.Empty;

            //Act

            //Assert

            await Assert.ThrowsAsync<ExerciseMSException>(async () => await unitOfWork.Exercises.DeleteAsync(exerciseId));
        }

        [Fact]
        public async Task ThrowExceptionIfUserDoesNotOwnThatExerciseDeleteExercise()
        {
            //Arrange
            var context = await InitializeContextAsync(true);

            _userService.Setup(u => u.GetUserId()).Returns("4de0bd76-08b1-4f81-85e6-33693ca836e3");

            unitOfWork = new(context, _userService.Object);

            var exerciseId = new Guid("aa12e9f1-32d9-4914-8824-908a87482387");

            //Act

            //Assert

            await Assert.ThrowsAsync<ExerciseMSException>(async () => await unitOfWork.Exercises.DeleteAsync(exerciseId));
        }

        [Fact]
        public async Task DeleteTheExerciseCorrectlyInDeleteExercise()
        {
            //Arrange
           
            var context = await InitializeContextAsync(true);

            _userService.Setup(u => u.GetUserId()).Returns("4de0dd76-08b1-4f81-85e6-33693ca836e3");

            unitOfWork = new(context, _userService.Object);

            var exerciseId = new Guid("aa12e9f1-32d9-4914-8824-908a87482387");

            //Act

            await unitOfWork.Exercises.DeleteAsync(exerciseId);
            await unitOfWork.CompleteAsync();
            //Assert

            await Assert.ThrowsAsync<ExerciseMSException>(async () => await unitOfWork.Exercises.GetByIdAsync(exerciseId));
        }

        #endregion

        #region UpdateIsInTheRoutine

        [Theory]
        [InlineData("465f44ac-1728-44d8-b229-c040a52664c2", "")]
        [InlineData("", "5e794ee9-8392-4c9a-94a1-5a87eb8caec5")]
        [InlineData("","")]
        public async Task ThrowExceptionIfExerciseIdOrUserIdAreEmptyInUpdateIsInTheRoutine(string exerciseIdentification, string userIdentification)
        {
            //Arrange
          
            var context = await InitializeContextAsync(false);

            unitOfWork = new(context, _userService.Object);

            Guid exerciseId = (string.IsNullOrEmpty(exerciseIdentification)) ? Guid.Empty : new Guid(exerciseIdentification);
            Guid userId = (string.IsNullOrEmpty(userIdentification)) ? Guid.Empty : new Guid(userIdentification);

            //Act

            //Assert

            await Assert.ThrowsAsync<ExerciseMSException>(async () => await unitOfWork.Exercises.UpdateIsInTheRoutine(true, exerciseId, userId));
        }

        [Fact]
        public async Task ThrowExceptionIfUserDoesNotOwnTheExercise()
        {
            //Arrange
          
            var context = await InitializeContextAsync(true);

            Guid exerciseId = new ("aa12e9f1-32d9-4914-8824-908a87482387");
            Guid userId = new ("aeb70bb5-f127-4976-8445-9c26cf6cb80b");

            //Act
            
            unitOfWork = new(context, _userService.Object);
            
            //Assert

            await Assert.ThrowsAsync<ExerciseMSException>(async () => await unitOfWork.Exercises.UpdateIsInTheRoutine(true, exerciseId, userId));
        }


        #endregion
    }
}
