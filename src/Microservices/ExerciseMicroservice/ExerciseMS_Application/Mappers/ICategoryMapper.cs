using ExerciseMS_Core.Dtos;
using ExerciseMS_Core.Models.Entities;
using ExerciseMS_Core.Models.Requests;
using System.Collections.Generic;

namespace ExerciseMS_Application.Mappers
{
    public interface ICategoryMapper
    {
        DtoCategory MapEntityToDto(Category category);
        IEnumerable<DtoCategory> MapEntityToDto(IEnumerable<Category> categories);
        Category MapRequestToEntity(CreateCategoryRequest request);
    }
}