using System.Threading;
using System.Threading.Tasks;

namespace Byndyusoft.Data.Relational.Sessions
{
    internal class DependentCommittableDbSession : IDependentDbSession
    {
        private readonly ICommittableDbSession _committableSession;

        public DependentCommittableDbSession(ICommittableDbSession committableSession)
        {
            _committableSession = committableSession;
        }

        public IDbSession DbSession => _committableSession;

        public ValueTask CommitAsync(CancellationToken cancellationToken) =>
            new(_committableSession.CommitAsync(cancellationToken));

        public ValueTask RollbackAsync(CancellationToken cancellationToken) =>
            new(_committableSession.CommitAsync(cancellationToken));

        public void Dispose() =>
            _committableSession.Dispose();
    }
}