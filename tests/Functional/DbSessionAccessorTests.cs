using System;
using System.IO;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using Xunit;

namespace Byndyusoft.Data.Relational.Functional
{
    public class DbSessionAccessorTests : IAsyncLifetime
    {
        private readonly string _file = $"{Guid.NewGuid()}.db";

        private Service _service;
        private DbSessionFactory _sessionFactory;
        private readonly DbSessionStorage _sessionStorage = new();

        public async Task InitializeAsync()
        {
            var connectionString = $"Data Source={_file};Pooling=false";

            await using var connection = new SqliteConnection(connectionString);
            await connection.ExecuteAsync("CREATE TABLE test (id INT, name TEXT)");
            
            _sessionFactory =
                new DbSessionFactory(new DbSessionOptionsMonitor(SqliteFactory.Instance, connectionString), _sessionStorage);
            _service = new Service(new DbSessionAccessor(_sessionFactory, _sessionStorage));
        }

        public Task DisposeAsync()
        {
            File.Delete(_file);

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
            private readonly IDbSessionAccessor _sessionAccessor;

            public Service(IDbSessionAccessor sessionAccessor)
            {
                _sessionAccessor = sessionAccessor;
            }

            public async Task<dynamic> QueryAsync()
            {
                return await _sessionAccessor.DbSession!.QueryFirstOrDefaultAsync("SELECT * FROM test LIMIT 1");
            }
        }
    }
}