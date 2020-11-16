using System;
using System.Linq;
using System.Data.Common;
using System.IO;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using Xunit;

namespace Byndyusoft.Data.Relational
{
    public class DbSessionQueryObjectExtensionsTests : IAsyncLifetime
    {
        private class Row
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        private DbSession _session;
        private DbConnection _connection;
        private QueryObject _queryObject;

        public async Task InitializeAsync()
        {
            File.Delete("query_object.db");

            _connection = new SqliteConnection("Data Source=query_object.db");
            await _connection.ExecuteAsync("CREATE TABLE test (id INT, name TEXT)");

            _session = new DbSession(_connection);
            _queryObject = new QueryObject("SELECT * FROM test");
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

        [Fact]
        public async Task QueryAsync_Generic_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    ((IDbSession) null).QueryAsync<Row>(_queryObject));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Fact]
        public async Task QueryAsync_Generic_NullQueryObject_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() => _session.QueryAsync<Row>((IQueryObject) null));

            // Assert
            Assert.Equal("queryObject", exception.ParamName);
        }

        [Fact]
        public async Task QueryAsync_Generic()
        {
            // Arrange
            await _connection.ExecuteAsync("INSERT INTO test (id, name) VALUES (1, 'test1');");
            var query = new QueryObject("SELECT id, name FROM test WHERE id=@id", new { id = 1 });

            // Act
            var rows = (await _session.QueryAsync<Row>(query)).ToArray();

            // Assert
            var row = Assert.Single(rows);
            Assert.NotNull(row);
            Assert.Equal(1, row.Id);
            Assert.Equal("test1", row.Name);
        }

        [Fact]
        public async Task QueryAsync_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() => ((IDbSession) null).QueryAsync(_queryObject));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Fact]
        public async Task QueryAsync_NullQueryObject_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() => _session.QueryAsync((IQueryObject) null));

            // Assert
            Assert.Equal("queryObject", exception.ParamName);
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
        public async Task ExecuteAsync_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() => ((IDbSession)null).ExecuteAsync(_queryObject));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Fact]
        public async Task ExecuteAsync_NullQueryObject_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() => _session.ExecuteAsync((IQueryObject)null));

            // Assert
            Assert.Equal("queryObject", exception.ParamName);
        }

        [Fact]
        public async Task ExecuteAsync()
        {
            // Arrange
            var queryObject = new QueryObject("INSERT INTO test (id, name) VALUES (@id, @name);", new { id = 1, name = "name" });

            // Act
            var result = await _session.ExecuteAsync(queryObject);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task ExecuteScalarAsync_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() => ((IDbSession)null).ExecuteScalarAsync(_queryObject));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Fact]
        public async Task ExecuteScalarAsync_NullQueryObject_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() => _session.ExecuteScalarAsync((IQueryObject)null));

            // Assert
            Assert.Equal("queryObject", exception.ParamName);
        }

        [Fact]
        public async Task ExecuteScalarAsync()
        {
            // Arrange
            var queryObject = new QueryObject("INSERT INTO test (id, name) VALUES (@id, @name); SELECT last_insert_rowid();", new { id = 1, name = "name" });

            // Act
            var id = await _session.ExecuteScalarAsync(queryObject);

            // Assert
            Assert.Equal(1, id);
        }

        [Fact]
        public async Task ExecuteScalarAsync_Generic_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() => ((IDbSession)null).ExecuteScalarAsync<int>(_queryObject));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Fact]
        public async Task ExecuteScalarAsync_Generic_NullQueryObject_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() => _session.ExecuteScalarAsync<int>((IQueryObject)null));

            // Assert
            Assert.Equal("queryObject", exception.ParamName);
        }

        [Fact]
        public async Task ExecuteScalarAsync_Generic()
        {
            // Arrange
            var queryObject = new QueryObject("INSERT INTO test (id, name) VALUES (@id, @name); SELECT last_insert_rowid();", new { id = 1, name = "name" });

            // Act
            var id = await _session.ExecuteScalarAsync<int>(queryObject);

            // Assert
            Assert.Equal(1, id);
        }

        [Fact]
        public async Task QueryMultipleAsync_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() => ((IDbSession)null).QueryMultipleAsync(_queryObject));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Fact]
        public async Task QueryMultipleAsync_NullQueryObject_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() => _session.QueryMultipleAsync((IQueryObject)null));

            // Assert
            Assert.Equal("queryObject", exception.ParamName);
        }

        [Fact]
        public async Task QueryMultipleAsync()
        {
            // Arrange
            await _connection.ExecuteAsync("INSERT INTO test (id, name) VALUES (1, 'test1');");
            await _connection.ExecuteAsync("INSERT INTO test (id, name) VALUES (2, 'test2');");
            var queryObject = new QueryObject("SELECT id, name FROM test WHERE id = @id1; SELECT id, name FROM test WHERE id = @id2", new{id1 = 1, id2=2});

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

#if !NETCOREAPP2_1

        [Fact]
        public async Task Query_NullQueryObject_ThrowsException()
        {
            // Act
            try
            {
                await _session.Query((IQueryObject)null).ToArrayAsync();
                Assert.False(true);
            }
            catch (ArgumentNullException exception)
            {
                // Assert
                Assert.Equal("queryObject", exception.ParamName);
            }
        }

        [Fact]
        public async Task Query_NullSession_ThrowsException()
        {
            // Act
            try
            {
                await ((IDbSession)null).Query(_queryObject).ToArrayAsync();
                Assert.False(true);
            }
            catch (ArgumentNullException exception)
            {
                // Assert
                Assert.Equal("session", exception.ParamName);
            }
        }

        [Fact]
        public async Task Query()
        {
            // Arrange
            await _connection.ExecuteAsync("INSERT INTO test (id, name) VALUES (1, 'test1');");
            var queryObject = new QueryObject("SELECT id, name FROM test WHERE id = @id", new {id = 1});

            // Act
            var rows = await _session.Query(queryObject).ToArrayAsync();

            // Assert
            var row = Assert.Single(rows);
            Assert.NotNull(row);
            Assert.Equal(1, row.id);
            Assert.Equal("test1", row.name);
        }

        [Fact]
        public async Task Query_Generic_NullQueryObject_ThrowsException()
        {
            // Act
            try
            {
                await _session.Query<Row>((IQueryObject)null).ToArrayAsync();
                Assert.False(true);
            }
            catch (ArgumentNullException exception)
            {
                // Assert
                Assert.Equal("queryObject", exception.ParamName);
            }
        }

        [Fact]
        public async Task Query_Generic_NullSession_ThrowsException()
        {
            // Act
            try
            {
                await ((IDbSession)null).Query<Row>(_queryObject).ToArrayAsync();
                Assert.False(true);
            }
            catch (ArgumentNullException exception)
            {
                // Assert
                Assert.Equal("session", exception.ParamName);
            }
        }

        [Fact]
        public async Task Query_Generic()
        {
            // Arrange
            await _connection.ExecuteAsync("INSERT INTO test (id, name) VALUES (1, 'test1');");
            var queryObject = new QueryObject("SELECT id, name FROM test WHERE id = @id", new { id = 1 });

            // Act
            var rows = await _session.Query<Row>(queryObject).ToArrayAsync();

            // Assert
            var row = Assert.Single(rows);
            Assert.NotNull(row);
            Assert.Equal(1, row.Id);
            Assert.Equal("test1", row.Name);
        }


#endif

    }
}
