using System.Threading;
using System.Threading.Tasks;

namespace Byndyusoft.Data.Relational.Sessions
{
    internal class DependentDbSession : IDependentDbSession
    {
        public DependentDbSession(IDbSession dbSession)
        {
            DbSession = dbSession;
        }

        public IDbSession DbSession { get; }

        public ValueTask CommitAsync(CancellationToken cancellationToken) 
            => new();

        public ValueTask RollbackAsync(CancellationToken cancellationToken) 
            => new();

        public void Dispose() =>
            DbSession.Dispose();
    }
}