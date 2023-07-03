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

        public async ValueTask CommitAsync(CancellationToken cancellationToken)
        {
            await _committableSession.CommitAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public async ValueTask RollbackAsync(CancellationToken cancellationToken)
        {
            await _committableSession.CommitAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public void Dispose()
        {
            _committableSession.Dispose();
        }
    }
}