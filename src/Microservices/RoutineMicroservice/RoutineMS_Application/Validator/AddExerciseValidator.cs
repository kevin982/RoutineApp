using FluentValidation;
using RoutineMS_Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutineMS_Application.Validator
{
    public class AddExerciseValidator : AbstractValidator<AddExerciseToRoutineCommand>
    {
        public AddExerciseValidator()
        {
            RuleFor(r => r.Request).NotNull();
            RuleFor(r => r.Request.ExerciseId).NotNull().NotEmpty();
            RuleFor(r => r.Request.Days).NotNull().NotEmpty().ForEach(d => d.GreaterThanOrEqualTo(1)).ForEach(d => d.LessThanOrEqualTo(7));
            RuleFor(r => r.Request.Sets).GreaterThanOrEqualTo(1).LessThanOrEqualTo(10);
            RuleFor(r => r.Request.ExerciseName).NotNull().NotEmpty();
            RuleFor(r => r.Request.CategoryName).NotNull().NotEmpty();
            RuleFor(r => r.Request.ImageUrl).NotNull().NotEmpty();
        }
    }
}
