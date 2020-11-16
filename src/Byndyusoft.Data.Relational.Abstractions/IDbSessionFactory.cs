using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Byndyusoft.Data.Relational
{
    public interface IDbSessionFactory
    {
        Task<IDbSession> CreateSessionAsync();

        Task<IDbSession> CreateSessionAsync(CancellationToken cancellationToken);

        Task<ICommittableDbSession> CreateCommittableSessionAsync();

        Task<ICommittableDbSession> CreateCommittableSessionAsync(IsolationLevel isolationLevel,
            CancellationToken cancellationToken);
    }
}
