using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using Xunit;

namespace Byndyusoft.Data.Relational
{
    public class CommittableDbSessionTransactionsTests : IAsyncLifetime
    {
        private CommittableDbSession _session;
        private DbConnection _connection;
        private readonly string _file = $"{Guid.NewGuid()}.db";

        public async Task InitializeAsync()
        {
            _connection = new SqliteConnection($"Data Source={_file}");
            await _connection.ExecuteAsync("CREATE TABLE test (id INT, name TEXT)");

            _session = new CommittableDbSession(_connection, IsolationLevel.Unspecified);
        }

        public Task DisposeAsync()
        {
            using (_session)
            {
            }

            using (_connection)
            {
            }

            File.Delete(_file);

            return Task.CompletedTask;
        }

        [Fact]
        public async Task Commit()
        {
            // Arrange
            await _session.ExecuteAsync("INSERT INTO test (id, name) VALUES (1, 'test')");

            // Act
            _session.Commit();

            // Arrange
            var row = await _connection.QueryFirstOrDefaultAsync("SELECT id, name FROM test");
            Assert.NotNull(row);
            Assert.Equal(1, row.id);
            Assert.Equal("test", row.name);
        }

        [Fact]
        public async Task Commit_AllowsDoubleCall()
        {
            // Arrange
            await _session.ExecuteAsync("INSERT INTO test (id, name) VALUES (1, 'test')");

            // Act
            _session.Commit();
            _session.Commit();

            // Arrange
            var row = await _connection.QueryFirstOrDefaultAsync("SELECT id, name FROM test");
            Assert.NotNull(row);
            Assert.Equal(1, row.id);
            Assert.Equal("test", row.name);
        }

        [Fact]
        public void Commit_NoChanges()
        {
            // Act
            _session.Commit();

            // Arrange
            Assert.True(true);
        }

        [Fact]
        public async Task Rollback()
        {
            // Arrange
            await _session.ExecuteAsync("INSERT INTO test (id, name) VALUES (1, 'test')");

            // Act
            _session.Rollback();

            // Arrange
            var row = await _connection.QueryFirstOrDefaultAsync("SELECT id, name FROM test");
            Assert.Null(row);
        }

        [Fact]
        public async Task Dispose_Do_Rollback()
        {
            // Arrange
            await _session.ExecuteAsync("INSERT INTO test (id, name) VALUES (1, 'test')");

            // Act
            using (_session)
            {
            }

            // Arrange
            var row = await _connection.QueryFirstOrDefaultAsync("SELECT id, name FROM test");
            Assert.Null(row);
        }

        [Fact]
        public async Task Rollback_AllowDoubleCall()
        {
            // Arrange
            await _session.ExecuteAsync("INSERT INTO test (id, name) VALUES (1, 'test')");

            // Act
            _session.Rollback();
            _session.Rollback();

            // Arrange
            var row = await _connection.QueryFirstOrDefaultAsync("SELECT id, name FROM test");
            Assert.Null(row);
        }

        [Fact]
        public void Rollback_NoChanges()
        {
            // Act
            _session.Rollback();

            // Arrange
            Assert.True(true);
        }

#if NETCOREAPP3_1
        [Fact]
        public async Task CommitAsync()
        {
            // Arrange
            await _session.ExecuteAsync("INSERT INTO test (id, name) VALUES (1, 'test')");

            // Act
            await _session.CommitAsync();

            // Arrange
            var row = await _connection.QueryFirstOrDefaultAsync("SELECT id, name FROM test");
            Assert.NotNull(row);
            Assert.Equal(1, row.id);
            Assert.Equal("test", row.name);
        }

        [Fact]
        public async Task CommitAsync_AllowDoubleCall()
        {
            // Arrange
            await _session.ExecuteAsync("INSERT INTO test (id, name) VALUES (1, 'test')");

            // Act
            await _session.CommitAsync();
            await _session.CommitAsync();

            // Arrange
            var row = await _connection.QueryFirstOrDefaultAsync("SELECT id, name FROM test");
            Assert.NotNull(row);
            Assert.Equal(1, row.id);
            Assert.Equal("test", row.name);
        }

        [Fact]
        public async Task CommitAsync_NoChanges()
        {
            // Act
            await _session.CommitAsync();

            // Arrange
            Assert.True(true);
        }

        [Fact]
        public async Task RollbackAsync()
        {
            // Arrange
            await _session.ExecuteAsync("INSERT INTO test (id, name) VALUES (1, 'test')");

            // Act
            await _session.RollbackAsync();

            // Arrange
            var row = await _connection.QueryFirstOrDefaultAsync("SELECT id, name FROM test");
            Assert.Null(row);
        }

        [Fact]
        public async Task DisposeAsync_Do_Rollback()
        {
            // Arrange
            await _session.ExecuteAsync("INSERT INTO test (id, name) VALUES (1, 'test')");

            // Act
            await using (_session)
            {
            }

            // Arrange
            var row = await _connection.QueryFirstOrDefaultAsync("SELECT id, name FROM test");
            Assert.Null(row);
        }

        [Fact]
        public async Task RollbackAsync_AllowDoubleCall()
        {
            // Arrange
            await _session.ExecuteAsync("INSERT INTO test (id, name) VALUES (1, 'test')");

            // Act
            await _session.RollbackAsync();
            await _session.RollbackAsync();

            // Arrange
            var row = await _connection.QueryFirstOrDefaultAsync("SELECT id, name FROM test");
            Assert.Null(row);
        }

        [Fact]
        public async ValueTask RollbackAsync_NoChanges()
        {
            // Act
            await _session.RollbackAsync();

            // Arrange
            Assert.True(true);
        }

#endif
    }
}