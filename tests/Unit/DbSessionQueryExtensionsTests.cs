using System;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace Byndyusoft.Data.Relational.Unit
{
    public class DbSessionQueryExtensions : IAsyncLifetime
    {
        private readonly string _sql = "SELECT * FROM TABLE";
        private DbSession _session;

        public Task InitializeAsync()
        {
            _session = new DbSession(Mock.Of<DbConnection>());
            return Task.CompletedTask;
        }

        public Task DisposeAsync()
        {
            _session.Dispose();
            return Task.CompletedTask;
        }

        [Fact]
        public async Task QueryAsync_Generic_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(
                    () => ((IDbSession) null)!.QueryAsync<int>(_sql));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Theory]
        [InlineData(null, typeof(ArgumentNullException))]
        [InlineData("", typeof(ArgumentException))]
        [InlineData(" ", typeof(ArgumentException))]
        public async Task QueryAsync_Generic_NullSql_ThrowsException(string sql, Type exceptionType)
        {
            // Act
            var exception = await Assert.ThrowsAnyAsync<ArgumentException>(
                () => _session.QueryAsync<int>(sql));

            // Assert
            Assert.IsType(exceptionType, exception);
            Assert.Equal("sql", exception.ParamName);
        }

        [Fact]
        public async Task QueryAsync_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(
                    () => ((IDbSession) null)!.QueryAsync(_sql));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Theory]
        [InlineData(null, typeof(ArgumentNullException))]
        [InlineData("", typeof(ArgumentException))]
        [InlineData(" ", typeof(ArgumentException))]
        public async Task QueryAsync_NullSql_ThrowsException(string sql, Type exceptionType)
        {
            // Act
            var exception = await Assert.ThrowsAnyAsync<ArgumentException>(() => _session.QueryAsync(sql));

            // Assert
            Assert.IsType(exceptionType, exception);
            Assert.Equal("sql", exception.ParamName);
        }

        [Fact]
        public async Task QueryFirstAsync_Generic_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(
                    () => ((IDbSession) null)!.QueryFirstAsync<int>(_sql));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Theory]
        [InlineData(null, typeof(ArgumentNullException))]
        [InlineData("", typeof(ArgumentException))]
        [InlineData(" ", typeof(ArgumentException))]
        public async Task QueryFirstAsync_Generic_NullSql_ThrowsException(string sql, Type exceptionType)
        {
            // Act
            var exception = await Assert.ThrowsAnyAsync<ArgumentException>(() => _session.QueryFirstAsync<int>(sql));

            // Assert
            Assert.IsType(exceptionType, exception);
            Assert.Equal("sql", exception.ParamName);
        }

        [Fact]
        public async Task QueryFirstAsync_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(
                    () => ((IDbSession) null)!.QueryFirstAsync(_sql));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Theory]
        [InlineData(null, typeof(ArgumentNullException))]
        [InlineData("", typeof(ArgumentException))]
        [InlineData(" ", typeof(ArgumentException))]
        public async Task QueryFirstAsync_NullSql_ThrowsException(string sql, Type exceptionType)
        {
            // Act
            var exception = await Assert.ThrowsAnyAsync<ArgumentException>(() => _session.QueryFirstAsync(sql));

            // Assert
            Assert.IsType(exceptionType, exception);
            Assert.Equal("sql", exception.ParamName);
        }

        [Fact]
        public async Task QueryFirstOrDefaultAsync_Generic_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    ((IDbSession) null)!.QueryFirstOrDefaultAsync<int>(_sql));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Theory]
        [InlineData(null, typeof(ArgumentNullException))]
        [InlineData("", typeof(ArgumentException))]
        [InlineData(" ", typeof(ArgumentException))]
        public async Task QueryFirstOrDefaultAsync_Generic_NullSql_ThrowsException(string sql, Type exceptionType)
        {
            // Act
            var exception =
                await Assert.ThrowsAnyAsync<ArgumentException>(
                    () => _session.QueryFirstOrDefaultAsync<int>(sql));

            // Assert
            Assert.IsType(exceptionType, exception);
            Assert.Equal("sql", exception.ParamName);
        }

        [Fact]
        public async Task QueryFirstOrDefaultAsync_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(
                    () => ((IDbSession) null)!.QueryFirstOrDefaultAsync(_sql));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Theory]
        [InlineData(null, typeof(ArgumentNullException))]
        [InlineData("", typeof(ArgumentException))]
        [InlineData(" ", typeof(ArgumentException))]
        public async Task QueryFirstOrDefaultAsync_NullSql_ThrowsException(string sql, Type exceptionType)
        {
            // Act
            var exception =
                await Assert.ThrowsAnyAsync<ArgumentException>(
                    () => _session.QueryFirstOrDefaultAsync(sql));

            // Assert
            Assert.IsType(exceptionType, exception);
            Assert.Equal("sql", exception.ParamName);
        }


        [Fact]
        public async Task QuerySingleAsync_Generic_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(
                    () => ((IDbSession) null)!.QuerySingleAsync<int>(_sql));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Theory]
        [InlineData(null, typeof(ArgumentNullException))]
        [InlineData("", typeof(ArgumentException))]
        [InlineData(" ", typeof(ArgumentException))]
        public async Task QuerySingleAsync_Generic_NullSql_ThrowsException(string sql, Type exceptionType)
        {
            // Act
            var exception = await Assert.ThrowsAnyAsync<ArgumentException>(
                () => _session.QuerySingleAsync<int>(sql));

            // Assert
            Assert.IsType(exceptionType, exception);
            Assert.Equal("sql", exception.ParamName);
        }

        [Fact]
        public async Task QuerySingleAsync_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(
                    () => ((IDbSession) null)!.QuerySingleAsync(_sql));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Theory]
        [InlineData(null, typeof(ArgumentNullException))]
        [InlineData("", typeof(ArgumentException))]
        [InlineData(" ", typeof(ArgumentException))]
        public async Task QuerySingleAsync_NullSql_ThrowsException(string sql, Type exceptionType)
        {
            // Act
            var exception = await Assert.ThrowsAnyAsync<ArgumentException>(
                () => _session.QuerySingleAsync(sql));

            // Assert
            Assert.IsType(exceptionType, exception);
            Assert.Equal("sql", exception.ParamName);
        }


        [Fact]
        public async Task QuerySingleOrDefaultAsync_Generic_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    ((IDbSession) null)!.QuerySingleOrDefaultAsync<int>(_sql));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Theory]
        [InlineData(null, typeof(ArgumentNullException))]
        [InlineData("", typeof(ArgumentException))]
        [InlineData(" ", typeof(ArgumentException))]
        public async Task QuerySingleOrDefaultAsync_Generic_NullSql_ThrowsException(string sql, Type exceptionType)
        {
            // Act
            var exception =
                await Assert.ThrowsAnyAsync<ArgumentException>(
                    () => _session.QuerySingleOrDefaultAsync<int>(sql));

            // Assert
            Assert.IsType(exceptionType, exception);
            Assert.Equal("sql", exception.ParamName);
        }

        [Fact]
        public async Task QuerySingleOrDefaultAsync_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    ((IDbSession) null)!.QuerySingleOrDefaultAsync(_sql));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Theory]
        [InlineData(null, typeof(ArgumentNullException))]
        [InlineData("", typeof(ArgumentException))]
        [InlineData(" ", typeof(ArgumentException))]
        public async Task QuerySingleOrDefaultAsync_NullSql_ThrowsException(string sql, Type exceptionType)
        {
            // Act
            var exception =
                await Assert.ThrowsAnyAsync<ArgumentException>(
                    () => _session.QuerySingleOrDefaultAsync(sql));

            // Assert
            Assert.IsType(exceptionType, exception);
            Assert.Equal("sql", exception.ParamName);
        }


        [Fact]
        public async Task ExecuteAsync_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(
                    () => ((IDbSession) null)!.ExecuteAsync(_sql));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }
        
        [Theory]
        [InlineData(null, typeof(ArgumentNullException))]
        [InlineData("", typeof(ArgumentException))]
        [InlineData(" ", typeof(ArgumentException))]
        public async Task ExecuteAsync_NullSql_ThrowsException(string sql, Type exceptionType)
        {
            // Act
            var exception = await Assert.ThrowsAnyAsync<ArgumentException>(
                () => _session.ExecuteAsync(sql));

            // Assert
            Assert.IsType(exceptionType, exception);
            Assert.Equal("sql", exception.ParamName);
        }

        [Fact]
        public async Task ExecuteScalarAsync_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(
                    () => ((IDbSession) null)!.ExecuteScalarAsync(_sql));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Theory]
        [InlineData(null, typeof(ArgumentNullException))]
        [InlineData("", typeof(ArgumentException))]
        [InlineData(" ", typeof(ArgumentException))]
        public async Task ExecuteScalarAsync_NullSql_ThrowsException(string sql, Type exceptionType)
        {
            // Act
            var exception = await Assert.ThrowsAnyAsync<ArgumentException>(
                () => _session.ExecuteScalarAsync(sql));

            // Assert
            Assert.IsType(exceptionType, exception);
            Assert.Equal("sql", exception.ParamName);
        }

        [Fact]
        public async Task ExecuteScalarAsync_Generic_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(
                    () => ((IDbSession) null)!.ExecuteScalarAsync<int>(_sql));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }


        [Theory]
        [InlineData(null, typeof(ArgumentNullException))]
        [InlineData("", typeof(ArgumentException))]
        [InlineData(" ", typeof(ArgumentException))]
        public async Task ExecuteScalarAsync_Generic_NullSql_ThrowsException(string sql, Type exceptionType)
        {
            // Act
            var exception =
                await Assert.ThrowsAnyAsync<ArgumentException>(
                    () => _session.ExecuteScalarAsync<long>(sql));

            // Assert
            Assert.IsType(exceptionType, exception);
            Assert.Equal("sql", exception.ParamName);
        }

        [Fact]
        public async Task QueryMultipleAsync_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(
                    () => ((IDbSession) null)!.QueryMultipleAsync(_sql));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }


        [Theory]
        [InlineData(null, typeof(ArgumentNullException))]
        [InlineData("", typeof(ArgumentException))]
        [InlineData(" ", typeof(ArgumentException))]
        public async Task QueryMultipleAsync_NullSql_ThrowsException(string sql, Type exceptionType)
        {
            // Act
            var exception =
                await Assert.ThrowsAnyAsync<ArgumentException>(
                    () => _session.QueryMultipleAsync(sql));

            // Assert
            Assert.IsType(exceptionType, exception);
            Assert.Equal("sql", exception.ParamName);
        }

        [Fact]
        public async Task Query_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(
                    async () => await ((IDbSession) null)!.Query(_sql).ToArrayAsync());

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Theory]
        [InlineData(null, typeof(ArgumentNullException))]
        [InlineData("", typeof(ArgumentException))]
        [InlineData(" ", typeof(ArgumentException))]
        public async Task Query_NullSql_ThrowsException(string sql, Type exceptionType)
        {
            // Act
            var exception =
                await Assert.ThrowsAnyAsync<ArgumentException>(
                    async () => await _session.Query(sql).ToArrayAsync());

            // Assert
            Assert.IsType(exceptionType, exception);
            Assert.Equal("sql", exception.ParamName);
        }

        [Fact]
        public async Task Query_Generic_NullSession_ThrowsException()
        {
            // Act
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await ((IDbSession) null)!.Query<int>(_sql).ToArrayAsync());

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Theory]
        [InlineData(null, typeof(ArgumentNullException))]
        [InlineData("", typeof(ArgumentException))]
        [InlineData(" ", typeof(ArgumentException))]
        public async Task Query_Generic_NullSql_ThrowsException(string sql, Type exceptionType)
        {
            // Act
            var exception =
                await Assert.ThrowsAnyAsync<ArgumentException>(
                    async () => await _session.Query<int>(sql).ToArrayAsync());

            // Assert
            Assert.IsType(exceptionType, exception);
            Assert.Equal("sql", exception.ParamName);
        }
    }
}