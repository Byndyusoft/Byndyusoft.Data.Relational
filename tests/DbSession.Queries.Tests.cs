using System;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using Xunit;

namespace Byndyusoft.Data.Relational
{
    public class DbSessionQueriesTests : IAsyncLifetime
    {
        private class Row
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        private DbSession _session;
        private DbConnection _connection;

        public async Task InitializeAsync()
        {
            File.Delete("queries.db");

            _connection = new SqliteConnection("Data Source=queries.db");
            await _connection.ExecuteAsync("CREATE TABLE test (id INT, name TEXT)");

            _session = new DbSession(_connection);
        }

        public Task DisposeAsync()
        {
            using (_session)
            {
            }

            using (_connection)
            {
            }

            return Task.CompletedTask;
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task QueryAsync_Generic_NullSql_ThrowsException(string sql)
        {
            // Act
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _session.QueryAsync<Row>(sql));

            // Assert
            Assert.Equal("sql", exception.ParamName);
        }

        [Fact]
        public async Task QueryAsync_Generic()
        {
            // Arrange
            await _connection.ExecuteAsync("INSERT INTO test (id, name) VALUES (1, 'test1');");

            // Act
            var rows = (await _session.QueryAsync<Row>("SELECT id, name FROM test")).ToArray();

            // Assert
            var row = Assert.Single(rows);
            Assert.NotNull(row);
            Assert.Equal(1, row.Id);
            Assert.Equal("test1", row.Name);
        }

        [Fact]
        public async Task QueryAsync_Generic_WithParams()
        {
            // Arrange
            await _connection.ExecuteAsync("INSERT INTO test (id, name) VALUES (1, 'test1');");

            // Act
            var rows =
                (await _session.QueryAsync<Row>("SELECT id, name FROM test WHERE id=@id", new {id = 1})).ToArray();

            // Assert
            var row = Assert.Single(rows);
            Assert.NotNull(row);
            Assert.Equal(1, row.Id);
            Assert.Equal("test1", row.Name);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task QueryAsync_NullSql_ThrowsException(string sql)
        {
            // Act
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _session.QueryAsync(sql));

            // Assert
            Assert.Equal("sql", exception.ParamName);
        }

        [Fact]
        public async Task QueryAsync()
        {
            // Arrange
            await _connection.ExecuteAsync("INSERT INTO test (id, name) VALUES (1, 'test1');");

            // Act
            var rows = (await _session.QueryAsync("SELECT id, name FROM test")).ToArray();

            // Assert
            var row = Assert.Single(rows);
            Assert.NotNull(row);
            Assert.Equal(1, row.id);
            Assert.Equal("test1", row.name);
        }

        [Fact]
        public async Task QueryAsync_WithParams()
        {
            // Arrange
            await _connection.ExecuteAsync("INSERT INTO test (id, name) VALUES (1, 'test1');");

            // Act
            var rows =
                (await _session.QueryAsync("SELECT id, name FROM test WHERE id=@id", new {id = 1})).ToArray();

            // Assert
            var row = Assert.Single(rows);
            Assert.NotNull(row);
            Assert.Equal(1, row.id);
            Assert.Equal("test1", row.name);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ExecuteAsync_NullSql_ThrowsException(string sql)
        {
            // Act
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _session.ExecuteAsync(sql));

            // Assert
            Assert.Equal("sql", exception.ParamName);
        }

        [Fact]
        public async Task ExecuteAsync()
        {
            // Act
            var result = await _session.ExecuteAsync("INSERT INTO test (id, name) VALUES (1, 'test1');");

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task ExecuteAsync_WithParams()
        {
            // Act
            var result = await _session.ExecuteAsync("INSERT INTO test (id, name) VALUES (@id, @name);",
                new {id = 1, name = "name"});

            // Assert
            Assert.Equal(1, result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ExecuteScalarAsync_NullSql_ThrowsException(string sql)
        {
            // Act
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _session.ExecuteScalarAsync(sql));

            // Assert
            Assert.Equal("sql", exception.ParamName);
        }

        [Fact]
        public async Task ExecuteScalarAsync()
        {
            // Act
            var id = await _session.ExecuteScalarAsync(
                "INSERT INTO test (id, name) VALUES (1, 'test1'); SELECT last_insert_rowid();");

            // Assert
            Assert.Equal(1, id);
        }

        [Fact]
        public async Task ExecuteScalarAsync_WithParams()
        {
            // Act
            var id = await _session.ExecuteScalarAsync(
                "INSERT INTO test (id, name) VALUES (@id, @name);  SELECT last_insert_rowid();",
                new {id = 1, name = "name"});

            // Assert
            Assert.Equal(1, id);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ExecuteScalarAsync_Generic_NullSql_ThrowsException(string sql)
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() => _session.ExecuteScalarAsync<long>(sql));

            // Assert
            Assert.Equal("sql", exception.ParamName);
        }

        [Fact]
        public async Task ExecuteScalarAsync_Generic()
        {
            // Act
            var id = await _session.ExecuteScalarAsync<long>(
                "INSERT INTO test (id, name) VALUES (1, 'test1'); SELECT last_insert_rowid();");

            // Assert
            Assert.Equal(1, id);
        }

        [Fact]
        public async Task ExecuteScalarAsync_Generic_WithParams()
        {
            // Act
            var id = await _session.ExecuteScalarAsync<long>(
                "INSERT INTO test (id, name) VALUES (@id, @name); SELECT last_insert_rowid();",
                new {id = 1, name = "name"});

            // Assert
            Assert.Equal(1, id);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task QueryMultipleAsync_NullSql_ThrowsException(string sql)
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() => _session.QueryMultipleAsync(sql));

            // Assert
            Assert.Equal("sql", exception.ParamName);
        }

        [Fact]
        public async Task QueryMultipleAsync()
        {
            // Arrange
            await _connection.ExecuteAsync("INSERT INTO test (id, name) VALUES (1, 'test1');");
            await _connection.ExecuteAsync("INSERT INTO test (id, name) VALUES (2, 'test2');");

            // Act
            var result =
                await _session.QueryMultipleAsync(
                    "SELECT id, name FROM test WHERE id = 1; SELECT id, name FROM test WHERE id = 2;");

            // Assert
            var set1 = await result.ReadAsync<Row>();
            var row1 = Assert.Single(set1);
            Assert.NotNull(row1);
            Assert.Equal(1, row1.Id);
            Assert.Equal("test1", row1.Name);

            var set2 = await result.ReadAsync<Row>();
            var row2 = Assert.Single(set2);
            Assert.NotNull(row2);
            Assert.Equal(2, row2.Id);
            Assert.Equal("test2", row2.Name);
        }

        [Fact]
        public async Task QueryMultipleAsync_WithParams()
        {
            // Arrange
            await _connection.ExecuteAsync("INSERT INTO test (id, name) VALUES (1, 'test1');");
            await _connection.ExecuteAsync("INSERT INTO test (id, name) VALUES (2, 'test2');");

            // Act
            var result = await _session.QueryMultipleAsync(
                "SELECT id, name FROM test WHERE id = @id1; SELECT id, name FROM test WHERE id = @id2;",
                new {id1 = 1, id2 = 2});

            // Assert
            var set1 = await result.ReadAsync<Row>();
            var row1 = Assert.Single(set1);
            Assert.NotNull(row1);
            Assert.Equal(1, row1.Id);
            Assert.Equal("test1", row1.Name);

            var set2 = await result.ReadAsync<Row>();
            var row2 = Assert.Single(set2);
            Assert.NotNull(row2);
            Assert.Equal(2, row2.Id);
            Assert.Equal("test2", row2.Name);
        }

#if NETCOREAPP3_1
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Query_NullSql_ThrowsException(string sql)
        {
            // Act
            try
            {
                await _session.Query(sql).ToArrayAsync();
                Assert.False(true);
            }
            catch (ArgumentNullException exception)
            {
                // Assert
                Assert.Equal("sql", exception.ParamName);
            }
        }

        [Fact]
        public async Task Query()
        {
            // Arrange
            await _connection.ExecuteAsync("INSERT INTO test (id, name) VALUES (1, 'test1');");

            // Act
            var rows = await _session.Query("SELECT id, name FROM test").ToArrayAsync();

            // Assert
            var row = Assert.Single(rows);
            Assert.NotNull(row);
            Assert.Equal(1, row.id);
            Assert.Equal("test1", row.name);
        }

        [Fact]
        public async Task Query_WithParams()
        {
            // Arrange
            await _connection.ExecuteAsync("INSERT INTO test (id, name) VALUES (1, 'test1');");

            // Act
            var rows = await _session.Query("SELECT id, name FROM test WHERE id = @id", new{id = 1}).ToArrayAsync();

            // Assert
            var row = Assert.Single(rows);
            Assert.NotNull(row);
            Assert.Equal(1, row.id);
            Assert.Equal("test1", row.name);
        }


        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Query_Generic_NullSql_ThrowsException(string sql)
        {
            // Act
            try
            {
                await _session.Query<Row>(sql).ToArrayAsync();
                Assert.False(true);
            }
            catch (ArgumentNullException exception)
            {
                // Assert
                Assert.Equal("sql", exception.ParamName);
            }
        }

        [Fact]
        public async Task Query_Generic()
        {
            // Arrange
            await _connection.ExecuteAsync("INSERT INTO test (id, name) VALUES (1, 'test1');");

            // Act
            var rows = await _session.Query<Row>("SELECT id, name FROM test").ToArrayAsync();

            // Assert
            var row = Assert.Single(rows);
            Assert.NotNull(row);
            Assert.Equal(1, row.Id);
            Assert.Equal("test1", row.Name);
        }

        [Fact]
        public async Task Query_Generic_WithParams()
        {
            // Arrange
            await _connection.ExecuteAsync("INSERT INTO test (id, name) VALUES (1, 'test1');");

            // Act
            var rows = await _session.Query<Row>("SELECT id, name FROM test WHERE id=@id", new {id = 1}).ToArrayAsync();

            // Assert
            var row = Assert.Single(rows);
            Assert.NotNull(row);
            Assert.Equal(1, row.Id);
            Assert.Equal("test1", row.Name);
        }
#endif
    }
}