using StatisticsMS_Core.Repositories;
using System.Threading.Tasks;

namespace StatisticsMS_Core.UoW
{
    public interface IUnitOfWork
    {
        IStatisticsRepository Statistics { get; }
        Task CompleteAsync();
    }
}
