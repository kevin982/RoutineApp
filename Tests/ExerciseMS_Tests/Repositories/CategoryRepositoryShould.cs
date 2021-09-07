using ExerciseMS_Core.Exceptions;
using ExerciseMS_Core.Models.Entities;
using ExerciseMS_Core.Services;
using ExerciseMS_Infraestructure.Data;
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
    public class CategoryRepositoryShould
    {
        private UnitOfWork unitOfWork;

        private Mock<IUserService> _userService = new();
        private async Task<ExerciseMsDbContext> InitializeContextAsync(bool seed)
        {
            var options = new DbContextOptionsBuilder<ExerciseMsDbContext>()
                .UseInMemoryDatabase(databaseName: $"ExercisePrueba{Guid.NewGuid()}")
                .Options;

            var context = new ExerciseMsDbContext(options);

            if (seed) await FakeData.SeedFakeData(context);

            return context;
        }

        #region CreateCategory

        [Fact]
        public async Task ThrowExceptionIfCategoryIsNullInCreateCategory()
        {
            //Arrange

            var context = await InitializeContextAsync(false);

            unitOfWork = new(context, _userService.Object);

            Category category = null;

            //Act

            //Assert

            await Assert.ThrowsAsync<ExerciseMSException>(async () => await unitOfWork.Categories.CreateAsync(category));
        }


        [Fact]
        public async Task CreateCategoryCorrectly()
        {
            //Arrange

            var context = await InitializeContextAsync(true);

            unitOfWork = new(context, _userService.Object);

            Category category = new Category() { CategoryId = new Guid("0130d449-4d41-4790-bf2b-17316527217b"), CategoryName = "Category test"};

            //Act

            await unitOfWork.Categories.CreateAsync(category);
            await unitOfWork.CompleteAsync();
            var result = await unitOfWork.Categories.GetByIdAsync(category.CategoryId);

            //Assert

            Assert.Equal(category, result);
        }

        #endregion

        #region DeleteCategory

        [Fact]
        public async Task ThrowExceptionIfCategoryIdIsEmptyInDeleteCategory()
        {
            //Arrange

            var context = await InitializeContextAsync(false);

            unitOfWork = new(context, _userService.Object);

            Guid categoryId = Guid.NewGuid();

            //Act

            //Assert

            await Assert.ThrowsAsync<ExerciseMSException>(async () => await unitOfWork.Categories.DeleteAsync(categoryId));
        }

        [Fact]
        public async Task DeleteTheActualCategoryInDeleteCategory()
        {
            //Arrange

            var context = await InitializeContextAsync(true);

            unitOfWork = new(context, _userService.Object);

            Guid categoryId = new("b0de268a-543f-447c-ba5a-21fb35e19146");

            //Act

            await unitOfWork.Categories.DeleteAsync(categoryId);
            await unitOfWork.CompleteAsync();

            //Assert

            await Assert.ThrowsAsync<ExerciseMSException>(async () => await unitOfWork.Categories.GetByIdAsync(categoryId));
        }


        #endregion

        #region GetAllCategories

        [Fact]
        public async Task ThrowExceptionIfThereAreNotCategoriesInGetAllCategories()
        {
            //Arrange

            var context = await InitializeContextAsync(false);

            unitOfWork = new(context, _userService.Object);

            //Act

            //Assert

            await Assert.ThrowsAsync<ExerciseMSException>(async () => await unitOfWork.Categories.GetAllAsync());
        }

        [Fact]
        public async Task GetAllCategoriesCorrectlyInGetAllCategories()
        {
            //Arrange

            var context = await InitializeContextAsync(true);

            unitOfWork = new(context, _userService.Object);

            var categoriesExpected = FakeData.FakeCategories().ToList();

            //Act

            var result = (List<Category>)await unitOfWork.Categories.GetAllAsync();

            //Assert

            for (int i = 0; i < result.Count; i++)
            {
                Assert.Equal(categoriesExpected[i].CategoryId, result[i].CategoryId);
                Assert.Equal(categoriesExpected[i].CategoryName, result[i].CategoryName);
            }
        }


        #endregion
    }
}
