using System.Threading;
using System.Threading.Tasks;
using Byndyusoft.Data.Relational;

internal class DependentCommitableDbSession : IDependentDbSession
{
    private readonly ICommittableDbSession _commitableSession;

    public DependentCommitableDbSession(ICommittableDbSession commitableSession)
    {
        _commitableSession = commitableSession;
    }

    public IDbSession DbSession => _commitableSession;

    public async ValueTask CommitAsync(CancellationToken cancellationToken)
    {
        await _commitableSession.CommitAsync(cancellationToken);
    }

    public async ValueTask RollbackAsync(CancellationToken cancellationToken)
    {
        await _commitableSession.CommitAsync(cancellationToken);
    }

    public void Dispose()
    {
        _commitableSession.Dispose();
    }
}