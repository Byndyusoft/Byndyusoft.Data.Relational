namespace Byndyusoft.Data.Relational
{
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Threading.Tasks;

    public class DbSessionFactory : IDbSessionFactory
    {
        public string ConnectionString { get; }

        public DbProviderFactory DbProviderFactory { get; }

        public DbSessionFactory(DbProviderFactory dbProviderFactory, string connectionString)
        {
            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            DbProviderFactory = dbProviderFactory ?? throw new ArgumentNullException(nameof(dbProviderFactory));
        }

        public Task<IDbSession> CreateSessionAsync()
        {
            var connection = DbProviderFactory.CreateConnection();
            if (connection == null) throw new InvalidOperationException();
            connection.ConnectionString = ConnectionString;
            var session = new DbSession(connection, null);
            DbSessionAccessor.DbSession = session;
            return Task.FromResult<IDbSession>(session);
        }

        public Task<ICommittableDbSession> CreateSessionAsync(IsolationLevel isolationLevel)
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