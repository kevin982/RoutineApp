using ExerciseMS_Application.Commands;
using ExerciseMS_Application.Extensions;
using FluentValidation;

namespace ExerciseMS_Application.Validations.Commands
{
    public class CreateExerciseCommandValidator : AbstractValidator<CreateExerciseCommand>
    {
        public CreateExerciseCommandValidator()
        {
            RuleFor(c => c.CreateExerciseRequest).NotNull();
            RuleFor(c => c.CreateExerciseRequest.CategoryId).NotEmpty();
            RuleFor(c => c.CreateExerciseRequest.Name).NotNull().NotEmpty().Length(5,30);
            RuleFor(c => c.CreateExerciseRequest.Image).NotNull();
            RuleFor(c => c.CreateExerciseRequest.Image.FileName).NotNull().NotEmpty();
            RuleFor(c => c.CreateExerciseRequest.Image.IsImage()).NotEqual(false);
        }
    }

}
