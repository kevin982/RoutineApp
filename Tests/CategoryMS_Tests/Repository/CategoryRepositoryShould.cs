using CategoryMS_Core.Exceptions;
using CategoryMS_Core.Models.Entities;
using CategoryMS_Infraestructure.Data;
using CategoryMS_Infraestructure.UoW;
using CategoryMS_Tests.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CategoryMS_Tests.Repository
{
    public class CategoryRepositoryShould
    {
        private UnitOfWork unitOfWork;
        private async Task<CategoryMsDbContext> InitializeContextAsync(bool seed)
        {
            var options = new DbContextOptionsBuilder<CategoryMsDbContext>()
                .UseInMemoryDatabase(databaseName: $"CategoryPrueba{Guid.NewGuid()}")
                .Options;

            var context = new CategoryMsDbContext(options);

            if (seed) await FakeData.SeedFakeData(context);

            return context;
        }

        #region CreateCategory

        [Fact]
        public async Task ThrowExceptionIfCategoryIsNullInCreateCategory()
        {
            //Arrange

            var context = await InitializeContextAsync(false);

            unitOfWork = new(context);

            Category category = null;

            //Act

            //Assert

            await Assert.ThrowsAsync<CategoryMSException>(async () => await unitOfWork.Categories.CreateAsync(category));
        }


        [Fact]
        public async Task CreateCategoryCorrectly()
        {
            //Arrange

            var context = await InitializeContextAsync(true);

            unitOfWork = new(context);

            Category category = new Category() { CategoryId = new Guid("0130d449-4d41-4790-bf2b-17316527217b"), CategoryName = "Category test" };

            //Act

            await unitOfWork.Categories.CreateAsync(category);
            await unitOfWork.CompleteAsync();
            var result = await unitOfWork.Categories.GetByIdAsync(category.CategoryId);

            //Assert

            Assert.Equal(category, result);
        }

        #endregion

        #region GetAllCategories

        [Fact]
        public async Task ThrowExceptionIfThereAreNotCategoriesInGetAllCategories()
        {
            //Arrange

            var context = await InitializeContextAsync(false);

            unitOfWork = new(context);

            //Act

            //Assert

            await Assert.ThrowsAsync<CategoryMSException>(async () => await unitOfWork.Categories.GetAllAsync());
        }

        [Fact]
        public async Task GetAllCategoriesCorrectlyInGetAllCategories()
        {
            //Arrange

            var context = await InitializeContextAsync(true);

            unitOfWork = new(context);

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
