namespace Byndyusoft.Data.Relational
{
    using System.Data;
    using System.Data.Common;
    using System.Threading;
    using System.Threading.Tasks;

    public class CommittableDbSession : DbSession, ICommittableDbSession
    {
        public CommittableDbSession(DbConnection connection, IsolationLevel isolationLevel)
            : base(connection, isolationLevel)
        {
            IsolationLevel = isolationLevel;
        }

        public IsolationLevel IsolationLevel { get; }

        public void Commit()
        {
            ThrowIfDisposed();

            Transaction?.Commit();
        }

#if NETSTANDARD2_1
        public async ValueTask CommitAsync(CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            if (Transaction == null)
                return;

            await Transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
#endif
    }
}