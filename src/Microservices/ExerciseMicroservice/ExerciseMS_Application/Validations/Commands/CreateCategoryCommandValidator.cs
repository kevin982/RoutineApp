﻿using ExerciseMS_Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_Application.Validations.Commands
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(c => c.CreateCategoryRequest).NotNull();
            RuleFor(c => c.CreateCategoryRequest.CategoryName).NotNull().NotEmpty().Length(3,20);
        }
    }
}
