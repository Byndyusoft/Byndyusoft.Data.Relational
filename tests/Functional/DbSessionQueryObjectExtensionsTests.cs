using System;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using Xunit;

namespace Byndyusoft.Data.Relational.Functional
{
    public class DbSessionQueryObjectExtensionsTests : IAsyncLifetime
    {
        private readonly string _file = $"{Guid.NewGuid()}.db";

        private DbConnection _connection;
        private DbSession _session;

        public async Task InitializeAsync()
        {
            _connection = new SqliteConnection($"Data Source={_file};Pooling=false");
            await _connection.ExecuteAsync("CREATE TABLE test (id INT, name TEXT)");

            _session = new DbSession(_connection);
        }

        public async Task DisposeAsync()
        {
            await _session.DisposeAsync();
            await _connection.DisposeAsync();

            File.Delete(_file);
        }

        [Fact]
        public async Task QueryAsync_Generic()
        {
            // Arrange
            await _connection.ExecuteAsync("INSERT INTO test (id, name) VALUES (1, 'test1');");
            var query = new QueryObject("SELECT id, name FROM test WHERE id=@id", new {id = 1});

            // Act
            var rows = (await _session.QueryAsync<Row>(query)).ToArray();

            // Assert
            var row = Assert.Single(rows);
            Assert.NotNull(row);
            Assert.Equal(1, row.Id);
            Assert.Equal("test1", row.Name);
        }

        [Fact]
        public async Task QueryAsync()
        {
            // Arrange
            await _connection.ExecuteAsync("INSERT INTO test (id, name) VALUES (1, 'test1');");
            var query = new QueryObject("SELECT id, name FROM test WHERE id=@id", new {id = 1});

            // Act
            var rows = (await _session.QueryAsync(query)).ToArray();

            // Assert
            var row = Assert.Single(rows);
            Assert.NotNull(row);
            Assert.Equal(1, row.id);
            Assert.Equal("test1", row.name);
        }

        [Fact]
        public async Task QueryFirstAsync_Generic()
        {
            // Arrange
            await _connection.ExecuteAsync("INSERT INTO test (id, name) VALUES (1, 'test1');");
            var query = new QueryObject("SELECT id, name FROM test WHERE id=@id", new {id = 1});

            // Act
            var row = await _session.QueryFirstAsync<Row>(query);

            // Assert
            Assert.NotNull(row);
            Assert.Equal(1, row.Id);
            Assert.Equal("test1", row.Name);
        }

        [Fact]
        public async Task QueryFirstAsync()
        {
            // Arrange
            await _connection.ExecuteAsync("INSERT INTO test (id, name) VALUES (1, 'test1');");
            var query = new QueryObject("SELECT id, name FROM test WHERE id=@id", new {id = 1});

            // Act
            var row = await _session.QueryFirstAsync(query);

            // Assert
            Assert.NotNull(row);
            Assert.Equal(1, row.id);
            Assert.Equal("test1", row.name);
        }

        [Fact]
        public async Task QueryFirstOrDefaultAsync_Generic()
        {
            // Arrange
            await _connection.ExecuteAsync("INSERT INTO test (id, name) VALUES (1, 'test1');");
            var query = new QueryObject("SELECT id, name FROM test WHERE id=@id", new {id = 1});

            // Act
            var row = await _session.QueryFirstOrDefaultAsync<Row>(query);

            // Assert
            Assert.NotNull(row);
            Assert.Equal(1, row.Id);
            Assert.Equal("test1", row.Name);
        }

        [Fact]
        public async Task QueryFirstOrDefaultAsync()
        {
            // Arrange
            await _connection.ExecuteAsync("INSERT INTO test (id, name) VALUES (1, 'test1');");
            var query = new QueryObject("SELECT id, name FROM test WHERE id=@id", new {id = 1});

            // Act
            var row = await _session.QueryFirstOrDefaultAsync(query);

            // Assert
            Assert.NotNull(row);
            Assert.Equal(1, row.id);
            Assert.Equal("test1", row.name);
        }


        [Fact]
        public async Task QuerySingleAsync_Generic()
        {
            // Arrange
            await _connection.ExecuteAsync("INSERT INTO test (id, name) VALUES (1, 'test1');");
            var query = new QueryObject("SELECT id, name FROM test WHERE id=@id", new {id = 1});

            // Act
            var row = await _session.QuerySingleAsync<Row>(query);

            // Assert
            Assert.NotNull(row);
            Assert.Equal(1, row.Id);
            Assert.Equal("test1", row.Name);
        }

        [Fact]
        public async Task QuerySingleAsync()
        {
            // Arrange
            await _connection.ExecuteAsync("INSERT INTO test (id, name) VALUES (1, 'test1');");
            var query = new QueryObject("SELECT id, name FROM test WHERE id=@id", new {id = 1});

            // Act
            var row = await _session.QuerySingleAsync(query);

            // Assert
            Assert.NotNull(row);
            Assert.Equal(1, row.id);
            Assert.Equal("test1", row.name);
        }

        [Fact]
        public async Task QuerySingleOrDefaultAsync_Generic()
        {
            // Arrange
            await _connection.ExecuteAsync("INSERT INTO test (id, name) VALUES (1, 'test1');");
            var query = new QueryObject("SELECT id, name FROM test WHERE id=@id", new {id = 1});

            // Act
            var row = await _session.QuerySingleOrDefaultAsync<Row>(query);

            // Assert
            Assert.NotNull(row);
            Assert.Equal(1, row.Id);
            Assert.Equal("test1", row.Name);
        }

        [Fact]
        public async Task QuerySingleOrDefaultAsync()
        {
            // Arrange
            await _connection.ExecuteAsync("INSERT INTO test (id, name) VALUES (1, 'test1');");
            var query = new QueryObject("SELECT id, name FROM test WHERE id=@id", new {id = 1});

            // Act
            var row = await _session.QuerySingleOrDefaultAsync(query);

            // Assert
            Assert.NotNull(row);
            Assert.Equal(1, row.id);
            Assert.Equal("test1", row.name);
        }


        [Fact]
        public async Task ExecuteAsync()
        {
            // Arrange
            var queryObject = new QueryObject("INSERT INTO test (id, name) VALUES (@id, @name);",
                new {id = 1, name = "name"});

            // Act
            var result = await _session.ExecuteAsync(queryObject);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task ExecuteScalarAsync()
        {
            // Arrange
            var queryObject =
                new QueryObject("INSERT INTO test (id, name) VALUES (@id, @name); SELECT last_insert_rowid();",
                    new {id = 1, name = "name"});

            // Act
            var id = await _session.ExecuteScalarAsync(queryObject);

            // Assert
            Assert.Equal(1, id);
        }

        [Fact]
        public async Task ExecuteScalarAsync_Generic()
        {
            // Arrange
            var queryObject =
                new QueryObject("INSERT INTO test (id, name) VALUES (@id, @name); SELECT last_insert_rowid();",
                    new {id = 1, name = "name"});

            // Act
            var id = await _session.ExecuteScalarAsync<int>(queryObject);

            // Assert
            Assert.Equal(1, id);
        }

        [Fact]
        public async Task QueryMultipleAsync()
        {
            // Arrange
            await _connection.ExecuteAsync("INSERT INTO test (id, name) VALUES (1, 'test1');");
            await _connection.ExecuteAsync("INSERT INTO test (id, name) VALUES (2, 'test2');");
            var queryObject =
                new QueryObject("SELECT id, name FROM test WHERE id = @id1; SELECT id, name FROM test WHERE id = @id2",
                    new {id1 = 1, id2 = 2});

            // Act
            var result = await _session.QueryMultipleAsync(queryObject);

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
        public async Task Query()
        {
            // Arrange
            await _connection.ExecuteAsync("INSERT INTO test (id, name) VALUES (1, 'test1');");
            var queryObject = new QueryObject("SELECT id, name FROM test WHERE id = @id", new {id = 1});

            // Act
            var rows = await _session.QueryUnbufferedAsync(queryObject).ToArrayAsync();

            // Assert
            var row = Assert.Single(rows);
            Assert.NotNull(row);
            Assert.Equal(1, row.id);
            Assert.Equal("test1", row.name);
        }

        [Fact]
        public async Task Query_Generic()
        {
            // Arrange
            await _connection.ExecuteAsync("INSERT INTO test (id, name) VALUES (1, 'test1');");
            var queryObject = new QueryObject("SELECT id, name FROM test WHERE id = @id", new {id = 1});

            // Act
            var rows = await _session.QueryUnbufferedAsync<Row>(queryObject).ToArrayAsync();

            // Assert
            var row = Assert.Single(rows);
            Assert.NotNull(row);
            Assert.Equal(1, row.Id);
            Assert.Equal("test1", row.Name);
        }

        private class Row
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}