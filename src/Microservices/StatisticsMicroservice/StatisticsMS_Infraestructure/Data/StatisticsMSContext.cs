using Microsoft.EntityFrameworkCore;
using StatisticsMS_Core.Models.Entities;

namespace StatisticsMS_Infraestructure.Data
{
    public class StatisticsMSContext : DbContext
    {
        public DbSet<Statistic> Statistics { get; set; }
        public StatisticsMSContext(DbContextOptions<StatisticsMSContext> options) : base(options) { }
    }
}
