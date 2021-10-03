using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StatisticsMS_Core.Models.Entities;
using StatisticsMS_Infraestructure.Data;

namespace StatisticsMS_Tests.Data
{
    public static class FakeData
    {
        public static async Task<StatisticsMSContext> InitializeContextAsync(bool seed)
        {
            try
            {
                StatisticsMSContext context;

                var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlServer()
                .BuildServiceProvider();

                var builder = new DbContextOptionsBuilder<StatisticsMSContext>();

                builder.UseSqlServer($"Server=tcp:127.0.0.1, 1433;Initial Catalog=StatisticsMS_tests_{Guid.NewGuid()};User Id=sa;Password=Statistics.1", b => b.MigrationsAssembly("StatisticsMS_API"))
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

        public static async Task<StatisticsMSContext> SeedFakeData(StatisticsMSContext context)
        {
            try
            {
                Statistic statistic1 = new(){Id = Guid.NewGuid(), UserId = new Guid("72cd9667-8bcd-45c6-bc57-befa1de5422f"), ExerciseId = new Guid("6d333d08-ca22-4865-a998-1f4fd8477970"), Weight = 60, Repetitions = 10, DayDone = new DateTime(2021, 8, 29)};
                Statistic statistic2 = new(){Id = Guid.NewGuid(), UserId = new Guid("72cd9667-8bcd-45c6-bc57-befa1de5422f"), ExerciseId = new Guid("6d333d08-ca22-4865-a998-1f4fd8477970"), Weight = 70, Repetitions = 12, DayDone = new DateTime(2021, 8, 29)};
                Statistic statistic3 = new(){Id = Guid.NewGuid(), UserId = new Guid("72cd9667-8bcd-45c6-bc57-befa1de5422f"), ExerciseId = new Guid("6d333d08-ca22-4865-a998-1f4fd8477970"), Weight = 60, Repetitions = 8, DayDone = new DateTime(2021, 12, 29)};
                Statistic statistic4 = new(){Id = Guid.NewGuid(), UserId = new Guid("72cd9667-8bcd-45c6-bc57-befa1de5422f"), ExerciseId = new Guid("6d333d08-ca22-4865-a998-1f4fd8477970"), Weight = 55, Repetitions = 8, DayDone = new DateTime(2020, 7, 29)};
                Statistic statistic5 = new(){Id = Guid.NewGuid(), UserId = new Guid("72cd9667-8bcd-45c6-bc57-befa1de5422f"), ExerciseId = new Guid("fb4b2d04-f1d9-4d8f-92b1-f7755a7d7899"), Weight = 15, Repetitions = 15, DayDone = new DateTime(2019, 1, 29)};
                Statistic statistic6 = new(){Id = Guid.NewGuid(), UserId = new Guid("72cd9667-8bcd-45c6-bc57-befa1de5422f"), ExerciseId = new Guid("fb4b2d04-f1d9-4d8f-92b1-f7755a7d7899"), Weight = 20, Repetitions = 10, DayDone = new DateTime(2018, 8, 29)};
                Statistic statistic7 = new(){Id = Guid.NewGuid(), UserId = new Guid("a46dc694-dac2-4bb0-9b7b-588e3ff11b41"), ExerciseId = new Guid("471c6e24-ece1-4fcd-9eab-e6ca0e8bd33c"), Weight = 30, Repetitions = 8, DayDone = new DateTime(2021, 7, 29)};
                Statistic statistic8 = new(){Id = Guid.NewGuid(), UserId = new Guid("a46dc694-dac2-4bb0-9b7b-588e3ff11b41"), ExerciseId = new Guid("471c6e24-ece1-4fcd-9eab-e6ca0e8bd33c"), Weight = 35, Repetitions = 12, DayDone = new DateTime(2021, 2, 25)};
                Statistic statistic9 = new(){Id = Guid.NewGuid(), UserId = new Guid("c4112d02-a10e-4e7b-afe1-7f9f0e07f781"), ExerciseId = new Guid("10aa12b2-c2e7-472c-8775-0a159071b7df"), Weight = 45, Repetitions = 12, DayDone = new DateTime(2020, 1, 29)};
                Statistic statistic10 = new(){Id = Guid.NewGuid(), UserId = new Guid("c4112d02-a10e-4e7b-afe1-7f9f0e07f781"), ExerciseId = new Guid("10aa12b2-c2e7-472c-8775-0a159071b7df"), Weight = 60, Repetitions = 8, DayDone = new DateTime(2021, 10, 29)};
                
                await context.Statistics.AddRangeAsync(statistic1, statistic2, statistic3, statistic4, statistic5, statistic6, statistic7, statistic8, statistic9, statistic10);

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