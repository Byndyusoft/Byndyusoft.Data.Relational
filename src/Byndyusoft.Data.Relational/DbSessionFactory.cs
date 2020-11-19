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
            DbProviderFactory = dbProviderFactory ?? throw new ArgumentNullException(nameof(dbProviderFactory));
        }

        public string ConnectionString { get; }

        public DbProviderFactory DbProviderFactory { get; }

        public Task<IDbSession> CreateSessionAsync(CancellationToken cancellationToken = default)
        {
            var connection = DbProviderFactory.CreateConnection();
            if (connection == null) throw new InvalidOperationException();
            connection.ConnectionString = ConnectionString;
            var session = new DbSession(connection, null);
            DbSessionAccessor.DbSession = session;
            return Task.FromResult<IDbSession>(session);
        }

        public virtual Task<ICommittableDbSession> CreateCommittableSessionAsync(
            CancellationToken cancellationToken = default)
        {
            return CreateCommittableSessionAsync(IsolationLevel.Unspecified, cancellationToken);
        }

        public virtual Task<ICommittableDbSession> CreateCommittableSessionAsync(
            IsolationLevel isolationLevel, CancellationToken cancellationToken = default)
        {
            var connection = DbProviderFactory.CreateConnection();
            if (connection == null) throw new InvalidOperationException();
            connection.ConnectionString = ConnectionString;
            var session = new CommittableDbSession(connection, isolationLevel);
            DbSessionAccessor.DbSession = session;
            return Task.FromResult<ICommittableDbSession>(session);
        }
    }
}