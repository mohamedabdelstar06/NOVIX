using System.Threading;
using System.Threading.Tasks;

namespace NOVIX.Shared.Core.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
