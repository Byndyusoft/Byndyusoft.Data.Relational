using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Byndyusoft.Data.Relational
{
    public class DbSessionFactory : IDbSessionFactory
    {
        public DbSessionFactory(DbProviderFactory dbProviderFactory, string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentNullException(nameof(connectionString));

            ConnectionString = connectionString;
            ProviderFactory = dbProviderFactory ?? throw new ArgumentNullException(nameof(dbProviderFactory));
        }

        public DbProviderFactory ProviderFactory { get; }

        public string ConnectionString { get; }

        public virtual Task<IDbSession> CreateSessionAsync(CancellationToken cancellationToken = default)
        {
            var session = new DbSession();
            try
            {
                return CreateSessionAsyncCore(session, cancellationToken);
            }
            catch
            {
                session.Dispose();
                throw;
            }
        }

        public Task<ICommittableDbSession> CreateCommittableSessionAsync(CancellationToken cancellationToken = default)
        {
            return CreateCommittableSessionAsync(IsolationLevel.Unspecified, cancellationToken);
        }

        public virtual Task<ICommittableDbSession> CreateCommittableSessionAsync(
            IsolationLevel isolationLevel, CancellationToken cancellationToken = default)
        {
            var session = new DbSession();
            try
            {
                return CreateCommittableSessionAsync(session, isolationLevel, cancellationToken);
            }
            catch
            {
                session.Dispose();
                throw;
            }
        }

        private async Task<IDbSession> CreateSessionAsyncCore(DbSession session, CancellationToken cancellationToken)
        {
            var connection = session.Connection = ProviderFactory.CreateConnection();
            if (connection == null) throw new InvalidOperationException();
            connection.ConnectionString = ConnectionString;
            await connection.OpenAsync(cancellationToken).ConfigureAwait(false);
            return session;
        }

        private async Task<ICommittableDbSession> CreateCommittableSessionAsync(DbSession session,
            IsolationLevel isolationLevel,
            CancellationToken cancellationToken)
        {
            await CreateSessionAsyncCore(session, cancellationToken)
                .ConfigureAwait(false);
            session.Transaction = await session.Connection.BeginTransactionAsync(isolationLevel, cancellationToken)
                .ConfigureAwait(false);
            return session;
        }
    }
}