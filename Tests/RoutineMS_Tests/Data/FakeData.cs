using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RoutineMS_Core.Models.Entities;
using RoutineMS_Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutineMS_Tests.Data
{
    public static class FakeData
    {
        public static async Task<RoutineMsDbContext> InitializeContextAsync(bool seed)
        {
            try
            {
                RoutineMsDbContext context;

                var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlServer()
                .BuildServiceProvider();

                var builder = new DbContextOptionsBuilder<RoutineMsDbContext>();

                builder.UseSqlServer($"Server=tcp:127.0.0.1, 1433;Initial Catalog=routineMS_tests_{Guid.NewGuid()};User Id=sa;Password=RoutineMS.1", b => b.MigrationsAssembly("RoutineMS_API"))
                        .UseInternalServiceProvider(serviceProvider);

                context = new(builder.Options);
                await context.Database.MigrateAsync();

                if (seed) context = await SeedFakeData(context);

                return context;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static async Task<RoutineMsDbContext> SeedFakeData(RoutineMsDbContext context)
        {
            try
            {
  
                SetDetail setDetail = new () { Id = new Guid("915e2e48-196a-47d1-8f9f-a17857a96893"), Date = DateTime.UtcNow, SetsCompleted = 2};
                SetDetail setDetail2 = new () { Id = new Guid("093f616d-fb76-4027-be65-dc88fe7162cc"), Date = new DateTime(2019, 12, 20), SetsCompleted = 2};
                SetDetail setDetail3 = new () { Id = new Guid("d229fead-6b44-47fc-b9ca-617f8864dbd2"), Date = new DateTime(2018, 5, 3), SetsCompleted = 2};

                await context.SetsDetails.AddRangeAsync(setDetail, setDetail2, setDetail3);

                Exercise exercise1 = new () { Id = new Guid("6d333d08-ca22-4865-a998-1f4fd8477970"), Name = "Cable Curl", CategoryName = "Biceps", ImageUrl = "https://www.mensjournal.com/wp-content/uploads/mf/three-way-finisher-chest-main.jpg?w=700&quality=86&strip=all" };
                Exercise exercise2 = new () { Id = new Guid("152ba755-91ee-4dce-a30b-dc33550ca5ee"), Name = "Cable Hammer Curl", CategoryName = "Biceps", ImageUrl = "https://i3.ytimg.com/vi/Xm05VEYT09Y/hqdefault.jpg", SetDetail = setDetail3 };
                Exercise exercise3 = new () { Id = new Guid("471c6e24-ece1-4fcd-9eab-e6ca0e8bd33c"), Name = "3-Way Finisher", CategoryName = "Chest", ImageUrl = "https://www.mensjournal.com/wp-content/uploads/mf/three-way-finisher-chest-main.jpg?w=700&quality=86&strip=all", SetDetail = setDetail2 };
                Exercise exercise4 = new () { Id = new Guid("fa37f4cb-54e9-44ed-9555-1400c93de5ae"), Name = "Barbell Bench Press", CategoryName = "Chest", ImageUrl = "https://www.mensjournal.com/wp-content/uploads/mf/_main2_benchpress.jpg?w=700&quality=86&strip=all", SetDetail = setDetail };

                await context.Exercises.AddRangeAsync(exercise1, exercise2, exercise3, exercise4);

                Routine routine1 = new () { Id = new Guid("08fe5653-02c1-4991-851d-209cda8b4bbd"), UserId = new Guid("72cd9667-8bcd-45c6-bc57-befa1de5422f"), Exercise = exercise3, Sets = 3, Days = new List<Day>() {await context.Days.Where(d => d.Id == 1).FirstOrDefaultAsync(), await context.Days.Where(d => d.Id == 3).FirstOrDefaultAsync() }};
                Routine routine2 = new () { Id = new Guid("8bd18297-9a6f-4493-8b91-43137b395659"), UserId = new Guid("72cd9667-8bcd-45c6-bc57-befa1de5422f"), Exercise = exercise2, Sets = 5, Days = new List<Day>() {await context.Days.Where(d => d.Id == 1).FirstOrDefaultAsync(), await context.Days.Where(d => d.Id == 3).FirstOrDefaultAsync() }};
                Routine routine3 = new () { Id = new Guid("d5c44169-af2a-4b44-84ab-9d201c9d5946"), UserId = new Guid("72cd9667-8bcd-45c6-bc57-befa1de5422f"), Exercise = exercise4, Sets = 4, Days = new List<Day>() {await context.Days.Where(d => d.Id == 1).FirstOrDefaultAsync(), await context.Days.Where(d => d.Id == 3).FirstOrDefaultAsync() }};
                Routine routine4 = new()  { Id = new Guid("21a9168e-b790-42a0-a221-d43a6bd24354"), UserId = new Guid("72cd9667-8bcd-45c6-bc57-befa1de5422f"), Exercise = exercise1, Sets = 5, Days = new List<Day>() { await context.Days.Where(d => d.Id == 1).FirstOrDefaultAsync(), await context.Days.Where(d => d.Id == 3).FirstOrDefaultAsync() }};
                
                await context.Routines.AddRangeAsync(routine1, routine2, routine3, routine4);

                await context.SaveChangesAsync();

                return context;
            }
            catch (Exception)
            {
                throw;
            }
        }
 
    }
}
