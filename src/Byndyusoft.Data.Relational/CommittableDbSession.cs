namespace Byndyusoft.Data.Relational
{
    using System.Data;
    using System.Data.Common;
    using System.Threading;
    using System.Threading.Tasks;

    public class CommittableDbSession : DbSession, ICommittableDbSession
    {
        private bool _completed;

        public CommittableDbSession(DbConnection connection, IsolationLevel isolationLevel)
            : base(connection, isolationLevel)
        {
            IsolationLevel = isolationLevel;
        }

        public IsolationLevel IsolationLevel { get; }

        public void Commit()
        {
            ThrowIfDisposed();

            if (Transaction == null || _completed)
                return;

            Transaction.Commit();
            _completed = true;
        }

        public void Rollback()
        {
            ThrowIfDisposed();

            if (Transaction == null || _completed)
                return;

            Transaction.Rollback();
            _completed = true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing == false || _completed)
                return;

            Transaction?.Rollback();
            _completed = true;

            base.Dispose(true);
        }

#if NETSTANDARD2_1
        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            if (Transaction == null || _completed)
                return;

            await Transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
            _completed = true;
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            if (Transaction == null || _completed)
                return;

            await Transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
            _completed = true;
        }

        protected override async ValueTask DisposeAsync(bool disposing)
        {
            if (disposing == false || _completed)
                return;

            if (Transaction != null)
            {
                await Transaction.RollbackAsync();
                _completed = true;
            }

            await base.DisposeAsync(true);
        }
#endif
    }
}