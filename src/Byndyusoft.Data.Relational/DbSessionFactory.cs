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
            var session = new DbSession(ProviderFactory, ConnectionString);
            return StartAsyncCore<IDbSession>(session, cancellationToken);
        }

        public Task<ICommitableDbSession> CreateCommitableSessionAsync(CancellationToken cancellationToken = default)
        {
            return CreateCommitableSessionAsync(IsolationLevel.Unspecified, cancellationToken);
        }

        public virtual Task<ICommitableDbSession> CreateCommitableSessionAsync(
            IsolationLevel isolationLevel, CancellationToken cancellationToken = default)
        {
            var session = new DbSession(ProviderFactory, ConnectionString, isolationLevel);
            return StartAsyncCore<ICommitableDbSession>(session, cancellationToken);
        }

        private static async Task<T> StartAsyncCore<T>(DbSession session, CancellationToken cancellationToken)
            where T : IDbSession
        {
            try
            {
                await session.StartAsync(cancellationToken).ConfigureAwait(false);
                return (T)(IDbSession)session;
            }
            catch
            {
                await session.DisposeAsync().ConfigureAwait(false); ;
                throw;
            }
        }
    }
}