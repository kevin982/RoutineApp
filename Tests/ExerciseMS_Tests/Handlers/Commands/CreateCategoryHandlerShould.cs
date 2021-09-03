using ExerciseMS_Application.Commands;
using ExerciseMS_Application.Handlers.CommandHandlers;
using ExerciseMS_Application.Mappers;
using ExerciseMS_Core.Dtos;
using ExerciseMS_Core.Exceptions;
using ExerciseMS_Core.Models.Entities;
using ExerciseMS_Core.Models.Requests;
using ExerciseMS_Core.UoW;
using FluentValidation;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ExerciseMS_Tests.Handlers.Commands
{
    public class CreateCategoryHandlerShould
    {
        private CreateCategoryHandler handler;

        private Mock<IUnitOfWork> _unitOfWork = new();
        private Mock<ICategoryMapper> _mapper = new();
        private Mock<IValidator<CreateCategoryCommand>> _validator = new();

 
        [Fact]
        public async Task CallTheCorrectMethods()
        {
            //Arrange
            CreateCategoryCommand command = new(new CreateCategoryRequest() { CategoryName = "legs" });
            CancellationToken cancellationToken = new();
            Category category = new() { CategoryName = "Chest", CategoryId = Guid.NewGuid() };

            _mapper.Setup(m => m.MapRequestToEntity(It.IsAny<CreateCategoryRequest>())).Returns(new Category());
            _unitOfWork.Setup(u => u.Categories.CreateAsync(It.IsAny<Category>())).ReturnsAsync(new Category());
            _mapper.Setup(m => m.MapEntityToDto(It.IsAny<Category>())).Returns(new DtoCategory());

            //Act

            handler = new(_unitOfWork.Object, _mapper.Object, _validator.Object);
            await handler.Handle(command, cancellationToken);

            //Assert

            _mapper.Verify(m => m.MapRequestToEntity(It.IsAny<CreateCategoryRequest>()), Times.Once());
            _unitOfWork.Verify(u => u.Categories.CreateAsync(It.IsAny<Category>()), Times.Once());
            _unitOfWork.Verify(u => u.CompleteAsync(), Times.Once());
            _mapper.Verify(m => m.MapEntityToDto(It.IsAny<Category>()), Times.Once());
        }



    }
}
