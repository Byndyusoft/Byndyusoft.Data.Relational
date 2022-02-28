using System;
using System.Data.Common;
using System.IO;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using Xunit;

namespace Byndyusoft.Data.Relational.Functional
{
    public class DbSessionTransactionsTests : IAsyncLifetime
    {
        private readonly string _file = $"{Guid.NewGuid()}.db";
        private DbConnection _connection;
        private DbSession _session;

        public async Task InitializeAsync()
        {
            _connection = new SqliteConnection($"Data Source={_file};Pooling=false");
            await _connection.OpenAsync();
            await _connection.ExecuteAsync("CREATE TABLE test (id INT, name TEXT)");

            var transaction = await _connection.BeginTransactionAsync();

            _session = new DbSession(_connection, transaction);
        }

        public async Task DisposeAsync()
        {
            await _session.DisposeAsync();
            await _connection.DisposeAsync();

            File.Delete(_file);
        }

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
        public async Task DisposeA_Do_Rollback()
        {
            // Arrange
            await _session.ExecuteAsync("INSERT INTO test (id, name) VALUES (1, 'test')");

            // Act
            _session.Dispose();

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
        public async Task RollbackAsync_NoChanges()
        {
            // Act
            await _session.RollbackAsync();

            // Arrange
            Assert.True(true);
        }
    }
}