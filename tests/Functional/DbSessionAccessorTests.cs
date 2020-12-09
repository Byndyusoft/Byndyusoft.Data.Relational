using System.IO;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using Xunit;

namespace Byndyusoft.Data.Relational.Functional
{
    public class DbSessionAccessorTests : IAsyncLifetime
    {
        private Service _service;
        private DbSessionFactory _sessionFactory;

        public async Task InitializeAsync()
        {
            File.Delete("queries.db");

            var connectionString = "Data Source=queries.db";

            using var connection = new SqliteConnection(connectionString);
            await connection.ExecuteAsync("CREATE TABLE test (id INT, name TEXT)");

            _sessionFactory = new DbSessionFactory(SqliteFactory.Instance, connectionString);
            _service = new Service();
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        [Fact]
        public async Task DirectSessionAccess()
        {
            // Arrange
            await using var session = await _sessionFactory.CreateSessionAsync();

            // Act
            var row = await session.QueryFirstOrDefaultAsync("SELECT * FROM test LIMIT 1");

            // Asser
            Assert.Null(row);
        }

        [Fact]
        public async Task SessionAccessViaSessionAccessor()
        {
            // Arrange
            await using var session = await _sessionFactory.CreateSessionAsync();

            // Act
            var row = await _service.QueryAsync();

            // Asser
            Assert.Null(row);
        }

        [Fact]
        public async Task MultipleUsage()
        {
            // ReSharper disable UseAwaitUsing
            using (await _sessionFactory.CreateSessionAsync())
            {
                var row = await _service.QueryAsync();
                Assert.Null(row);
            }

            using (await _sessionFactory.CreateSessionAsync())
            {
                var row = await _service.QueryAsync();
                Assert.Null(row);
            }

            // ReSharper enable UseAwaitUsing
        }

        [Fact]
        public async Task MultipleUsage_Async()
        {
            await using (await _sessionFactory.CreateSessionAsync())
            {
                var row = await _service.QueryAsync();
                Assert.Null(row);
            }

            await using (await _sessionFactory.CreateSessionAsync())
            {
                var row = await _service.QueryAsync();
                Assert.Null(row);
            }
        }

        private class Service
        {
            private readonly IDbSessionAccessor _sessionAccessor = new DbSessionAccessor();

            public async Task<dynamic> QueryAsync()
            {
                return await _sessionAccessor.DbSession.QueryFirstOrDefaultAsync("SELECT * FROM test LIMIT 1");
            }
        }
    }
}