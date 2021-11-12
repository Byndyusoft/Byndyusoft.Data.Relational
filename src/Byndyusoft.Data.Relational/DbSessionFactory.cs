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
            return StartSessionAsyncCore(session, cancellationToken);
        }

        public Task<ICommittableDbSession> CreateCommittableSessionAsync(CancellationToken cancellationToken = default)
        {
            return CreateCommittableSessionAsync(IsolationLevel.Unspecified, cancellationToken);
        }

        public virtual Task<ICommittableDbSession> CreateCommittableSessionAsync(
            IsolationLevel isolationLevel, CancellationToken cancellationToken = default)
        {
            var session = new DbSession();
            return StartCommittableSessionAsyncCore(session, isolationLevel, cancellationToken);
        }

        private async Task<IDbSession> StartSessionAsyncCore(DbSession session, CancellationToken cancellationToken)
        {
            session.Start();

            try
            {
                var connection = session.Connection = ProviderFactory.CreateConnection()!;
                if (connection == null) throw new InvalidOperationException();
                connection.ConnectionString = ConnectionString;
                await connection.OpenAsync(cancellationToken).ConfigureAwait(false);
                return session;
            }
            catch
            {
                await session.DisposeAsync();
                throw;
            }
        }

        private async Task<ICommittableDbSession> StartCommittableSessionAsyncCore(DbSession session,
            IsolationLevel isolationLevel, CancellationToken cancellationToken)
        {
            try
            {
                await StartSessionAsyncCore(session, cancellationToken)
                    .ConfigureAwait(false);
                session.Transaction = await session.Connection.BeginTransactionAsync(isolationLevel, cancellationToken)
                    .ConfigureAwait(false);
                return session;
            }
            catch
            {
                await session.DisposeAsync();
                throw;
            }
        }
    }
}