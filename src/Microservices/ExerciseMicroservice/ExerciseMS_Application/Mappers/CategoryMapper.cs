using ExerciseMS_Core.Dtos;
using ExerciseMS_Core.Models.Entities;
using ExerciseMS_Core.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_Application.Mappers
{
    public class CategoryMapper : ICategoryMapper
    {
        public Category MapRequestToEntity(CreateCategoryRequest request)
        {
            if (request is null) return null;

            return new Category
            {
                CategoryName = request.CategoryName
            };
        }

        public DtoCategory MapEntityToDto(Category category)
        {
            if (category is null) return null;

            return new DtoCategory
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName
            };
        }

        public IEnumerable<DtoCategory> MapEntityToDto(IEnumerable<Category> categories)
        {
            if (categories is null) return null;

            if (categories.Count() == 0) return null;

            List<DtoCategory> result = new();

            foreach (var category in categories) result.Add(MapEntityToDto(category));

            return result;
        }
    }
}
