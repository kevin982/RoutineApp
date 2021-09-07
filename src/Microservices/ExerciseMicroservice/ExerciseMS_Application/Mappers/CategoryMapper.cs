using ExerciseMS_Core.Dtos;
using ExerciseMS_Core.Models.Entities;
using ExerciseMS_Core.Models.Requests;
using System;
using System.Collections.Generic;

namespace ExerciseMS_Application.Mappers
{
    public class CategoryMapper : ICategoryMapper
    {
        public Category MapRequestToEntity(CreateCategoryRequest request)
        {
            try
            {
                return new Category
                {
                    CategoryName = request.CategoryName
                };
            }
            catch (Exception)
            {
                throw;
            }

        }

        public DtoCategory MapEntityToDto(Category category)
        {
            try
            {
                return new DtoCategory
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<DtoCategory> MapEntityToDto(IEnumerable<Category> categories)
        {
            try
            {
                List<DtoCategory> result = new();

                foreach (var category in categories) result.Add(MapEntityToDto(category));
                
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
