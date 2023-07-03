using System.Threading;
using System.Threading.Tasks;

namespace Byndyusoft.Data.Relational.Sessions
{
    internal class DependentDbSession : IDependentDbSession
    {
        private readonly IDbSession _dbSession;

        public DependentDbSession(IDbSession dbSession)
        {
            _dbSession = dbSession;
        }

        public IDbSession DbSession => _dbSession;

        public ValueTask CommitAsync(CancellationToken cancellationToken) => new();

        public ValueTask RollbackAsync(CancellationToken cancellationToken) => new();

        public void Dispose()
        {
            _dbSession.Dispose();
        }
    }
}