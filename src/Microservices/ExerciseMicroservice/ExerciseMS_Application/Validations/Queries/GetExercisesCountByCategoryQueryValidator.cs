using ExerciseMS_Application.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_Application.Validations.Queries
{
    public class GetExercisesCountByCategoryQueryValidator : AbstractValidator<GetExercisesCountByCategoryQuery>
    {
        public GetExercisesCountByCategoryQueryValidator()
        {
            RuleFor(g => g.CategoryId).NotNull().NotEmpty();
        }
    }
}
