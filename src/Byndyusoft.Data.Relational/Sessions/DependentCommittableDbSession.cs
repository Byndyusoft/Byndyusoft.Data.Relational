using System.Threading;
using System.Threading.Tasks;
using Byndyusoft.Data.Relational;

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
        await _committableSession.CommitAsync(cancellationToken);
    }

    public async ValueTask RollbackAsync(CancellationToken cancellationToken)
    {
        await _committableSession.CommitAsync(cancellationToken);
    }

    public void Dispose()
    {
        _committableSession.Dispose();
    }
}