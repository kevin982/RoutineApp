using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StatisticsMS_Application.Mappers;
using System.Reflection;

namespace StatisticsMS_Application
{
    public static class ApplicationDI
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<IStatisticMapper, StatisticMapper>();
            return services;
        }
    }
}
