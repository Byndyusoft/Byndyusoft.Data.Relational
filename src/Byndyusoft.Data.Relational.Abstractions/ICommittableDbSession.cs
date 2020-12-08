using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Byndyusoft.Data.Relational
{
    public interface ICommittableDbSession : IDbSession
    {
        IsolationLevel IsolationLevel { get; }

        Task CommitAsync(CancellationToken cancellationToken = default);

        Task RollbackAsync(CancellationToken cancellationToken = default);
    }
}