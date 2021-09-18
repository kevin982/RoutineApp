using CategoryMS_Core.Repositories;
using System.Threading.Tasks;

namespace CategoryMS_Core.UoW
{
    public interface IUnitOfWork
    {
        ICategoryRepository Categories { get; }
        Task CompleteAsync();
    }
}
