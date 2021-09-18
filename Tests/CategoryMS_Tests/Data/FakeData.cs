using CategoryMS_Core.Models.Entities;
using CategoryMS_Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CategoryMS_Tests.Data
{
    public class FakeData
    {
        public static async Task SeedFakeData(CategoryMsDbContext context)
        {
            await context.Categories.AddRangeAsync(FakeCategories());

            await context.SaveChangesAsync();
        }

        public static IEnumerable<Category> FakeCategories()
        {
            return new List<Category>()
            {
                new Category(){CategoryId = new Guid("b0de268a-543f-447c-ba5a-21fb35e19146"), CategoryName = "Back"},
                new Category(){CategoryId = new Guid("5f5b42b2-e14d-4824-8d93-3977e3355f01"), CategoryName = "Chest"},
                new Category(){CategoryId = new Guid("786f4325-63e7-4f3d-9aa9-4355538a3ba3"), CategoryName = "Abs"},
                new Category(){CategoryId = new Guid("0caffb7d-defe-46c4-8082-5ab3a7ad3f89"), CategoryName = "Biceps"},
                new Category(){CategoryId = new Guid("cea33567-29de-41d7-8689-79d2a0bdb67e"), CategoryName = "Triceps"},
                new Category(){CategoryId = new Guid("d5e61185-b9ce-4acc-a801-896ed3737f65"), CategoryName = "Shoulders"},
                new Category(){CategoryId = new Guid("92ee6e17-569e-4e39-9f23-af028206431a"), CategoryName = "Cardio"},
                new Category(){CategoryId = new Guid("6e09dc32-23d1-4a82-aaeb-ba47c852ebee"), CategoryName = "Forearms"},
                new Category(){CategoryId = new Guid("04b1e0cd-6f9f-4bf3-bf28-f9487fd588ed"), CategoryName = "Legs"}
            };
        }
    }
}
