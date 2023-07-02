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

        private static IDbSession NullDbSession => null!;

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
                    () => NullDbSession.QueryAsync<int>(_sql));

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
                    () => NullDbSession.QueryAsync(_sql));

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
                    () => NullDbSession.QueryFirstAsync<int>(_sql));

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
                    () => NullDbSession.QueryFirstAsync(_sql));

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
                    NullDbSession.QueryFirstOrDefaultAsync<int>(_sql));

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
                    () => NullDbSession.QueryFirstOrDefaultAsync(_sql));

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
                    () => NullDbSession.QuerySingleAsync<int>(_sql));

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
                    () => NullDbSession.QuerySingleAsync(_sql));

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
                    NullDbSession.QuerySingleOrDefaultAsync<int>(_sql));

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
                    NullDbSession.QuerySingleOrDefaultAsync(_sql));

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
                    () => NullDbSession.ExecuteAsync(_sql));

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
                    () => NullDbSession.ExecuteScalarAsync(_sql));

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
                    () => NullDbSession.ExecuteScalarAsync<int>(_sql));

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
                    () => NullDbSession.QueryMultipleAsync(_sql));

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
        public async Task QueryUnbufferedAsync_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(
                    async () => await NullDbSession.QueryUnbufferedAsync(_sql).ToArrayAsync());

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Theory]
        [InlineData(null, typeof(ArgumentNullException))]
        [InlineData("", typeof(ArgumentException))]
        [InlineData(" ", typeof(ArgumentException))]
        public async Task QueryUnbufferedAsync_NullSql_ThrowsException(string sql, Type exceptionType)
        {
            // Act
            var exception =
                await Assert.ThrowsAnyAsync<ArgumentException>(
                    async () => await _session.QueryUnbufferedAsync(sql).ToArrayAsync());

            // Assert
            Assert.IsType(exceptionType, exception);
            Assert.Equal("sql", exception.ParamName);
        }

        [Fact]
        public async Task QueryUnbufferedAsync_Generic_NullSession_ThrowsException()
        {
            // Act
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await NullDbSession.QueryUnbufferedAsync<int>(_sql).ToArrayAsync());

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Theory]
        [InlineData(null, typeof(ArgumentNullException))]
        [InlineData("", typeof(ArgumentException))]
        [InlineData(" ", typeof(ArgumentException))]
        public async Task QueryUnbufferedAsync_Generic_NullSql_ThrowsException(string sql, Type exceptionType)
        {
            // Act
            var exception =
                await Assert.ThrowsAnyAsync<ArgumentException>(
                    async () => await _session.QueryUnbufferedAsync<int>(sql).ToArrayAsync());

            // Assert
            Assert.IsType(exceptionType, exception);
            Assert.Equal("sql", exception.ParamName);
        }
    }
}