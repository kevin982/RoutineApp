using ExerciseMS_Core.Dtos;
using ExerciseMS_Core.Exceptions;
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
            try
            {
                if (request is null) throw new ExerciseMSException("The create category request can not be null"){ StatusCode = 500};

                if (request.CategoryName.Length < 3) throw new ExerciseMSException("The category name length must be greater or equal to 3.") { StatusCode = 400};

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
                if (category is null) throw new ExerciseMSException("The category must not be null") { StatusCode = 500 };

                if (category.CategoryId == Guid.Empty || category.CategoryName.Length < 3) throw new ExerciseMSException("The category id must not be empty and the name length must be greater or equal to 3.") { StatusCode = 400 };

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
                if (categories is null) throw new ExerciseMSException("The categories can not be null") { StatusCode = 500};

                if (categories.Count() == 0) throw new ExerciseMSException("The categories list must contain at least one category") { StatusCode = 500};

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
