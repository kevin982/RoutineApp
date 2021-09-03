using ExerciseMS_Application.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_Application.Validations.Queries
{
    public class GetExercisesByCategoryQueryValidator : AbstractValidator<GetExercisesByCategoryQuery>
    {
        public GetExercisesByCategoryQueryValidator()
        {
            RuleFor(g => g.CategoryId).NotNull().NotEmpty();
            RuleFor(g => g.Index).GreaterThanOrEqualTo(0);  
            RuleFor(g => g.Size).GreaterThan(4).LessThan(20);
        }
    }
}
