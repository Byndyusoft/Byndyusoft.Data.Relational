using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.Diagnostics;

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
            return CreateSessionAsyncCore(session, cancellationToken);
        }

        public Task<ICommittableDbSession> CreateCommittableSessionAsync(CancellationToken cancellationToken = default)
        {
            return CreateCommittableSessionAsync(IsolationLevel.Unspecified, cancellationToken);
        }

        public virtual Task<ICommittableDbSession> CreateCommittableSessionAsync(
            IsolationLevel isolationLevel, CancellationToken cancellationToken = default)
        {
            var session = new DbSession();
            return CreateCommittableSessionAsync(session, isolationLevel, cancellationToken);
        }
        
        private async Task<IDbSession> CreateSessionAsyncCore(DbSession session, CancellationToken cancellationToken)
        {
            try
            {
                var connection = session.Connection = ProviderFactory.CreateConnection()?.AddDiagnosting();
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

        private async Task<ICommittableDbSession> CreateCommittableSessionAsync(DbSession session,
            IsolationLevel isolationLevel, CancellationToken cancellationToken)
        {
            try
            {
                await CreateSessionAsyncCore(session, cancellationToken)
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