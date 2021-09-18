using CategoryMS_Application.Commands;
using CategoryMS_Application.Handlers.CommandHandlers;
using CategoryMS_Core.Models.Entities;
using CategoryMS_Core.Models.Requests;
using CategoryMS_Core.UoW;
using FluentValidation;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CategoryMS_Tests.Handlers
{
    public class CreateCategoryHandlerShould
    {
        private CreateCategoryHandler handler;

        private Mock<IUnitOfWork> _unitOfWork = new();
        private Mock<IValidator<CreateCategoryCommand>> _validator = new();


        [Fact]
        public async Task CallTheCorrectMethods()
        {
            //Arrange
            CreateCategoryCommand command = new(new CreateCategoryRequest() { CategoryName = "legs" });
            CancellationToken cancellationToken = new();
            Category category = new() { CategoryName = "Chest", CategoryId = Guid.NewGuid() };

            _unitOfWork.Setup(u => u.Categories.CreateAsync(It.IsAny<Category>())).ReturnsAsync(new Category());

            //Act

            handler = new(_unitOfWork.Object,_validator.Object);
            await handler.Handle(command, cancellationToken);

            //Assert

            _unitOfWork.Verify(u => u.Categories.CreateAsync(It.IsAny<Category>()), Times.Once());
            _unitOfWork.Verify(u => u.CompleteAsync(), Times.Once());
        }



    }
}
