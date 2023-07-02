using System;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace Byndyusoft.Data.Relational.Unit
{
    public class DbSessionQueryObjectExtensionsTests : IAsyncLifetime
    {
        private QueryObject _queryObject;
        private DbSession _session;

        private static IQueryObject NullQueryObject => null!;
        private static IDbSession NullDbSession => null!;

        public Task InitializeAsync()
        {
            _queryObject = new QueryObject("SELECT * FROM TABLE");
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
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    NullDbSession.QueryAsync<int>(_queryObject));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Fact]
        public async Task QueryAsync_Generic_NullQueryObject_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() => _session.QueryAsync<int>(NullQueryObject));

            // Assert
            Assert.Equal("queryObject", exception.ParamName);
        }

        [Fact]
        public async Task QueryAsync_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() => NullDbSession.QueryAsync(_queryObject));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Fact]
        public async Task QueryAsync_NullQueryObject_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() => _session.QueryAsync(NullQueryObject));

            // Assert
            Assert.Equal("queryObject", exception.ParamName);
        }

        [Fact]
        public async Task QuerySingleAsync_Generic_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    NullDbSession.QuerySingleAsync<int>(_queryObject));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Fact]
        public async Task QuerySingleAsync_Generic_NullQueryObject_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    _session.QuerySingleAsync<int>(NullQueryObject));

            // Assert
            Assert.Equal("queryObject", exception.ParamName);
        }

        [Fact]
        public async Task QuerySingleAsync_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(
                    () => NullDbSession.QuerySingleAsync(_queryObject));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Fact]
        public async Task QuerySingleAsync_NullQueryObject_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() => _session.QuerySingleAsync(NullQueryObject));

            // Assert
            Assert.Equal("queryObject", exception.ParamName);
        }

        [Fact]
        public async Task QuerySingleOrDefaultAsync_Generic_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    NullDbSession.QuerySingleOrDefaultAsync<int>(_queryObject));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Fact]
        public async Task QuerySingleOrDefaultAsync_Generic_NullQueryObject_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    _session.QuerySingleOrDefaultAsync<int>(NullQueryObject));

            // Assert
            Assert.Equal("queryObject", exception.ParamName);
        }

        [Fact]
        public async Task QuerySingleOrDefaultAsync_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    NullDbSession.QuerySingleOrDefaultAsync(_queryObject));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Fact]
        public async Task QuerySingleOrDefaultAsync_NullQueryObject_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    _session.QuerySingleOrDefaultAsync(NullQueryObject));

            // Assert
            Assert.Equal("queryObject", exception.ParamName);
        }

        [Fact]
        public async Task QueryFirstAsync_Generic_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    NullDbSession.QueryFirstAsync<int>(_queryObject));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Fact]
        public async Task QueryFirstAsync_Generic_NullQueryObject_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(
                    () => _session.QueryFirstAsync<int>(NullQueryObject));

            // Assert
            Assert.Equal("queryObject", exception.ParamName);
        }

        [Fact]
        public async Task QueryFirstAsync_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    NullDbSession.QueryFirstAsync(_queryObject));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Fact]
        public async Task QueryFirstAsync_NullQueryObject_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() => _session.QueryFirstAsync(NullQueryObject));

            // Assert
            Assert.Equal("queryObject", exception.ParamName);
        }

        [Fact]
        public async Task QueryFirstOrDefaultAsync_Generic_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    NullDbSession.QueryFirstOrDefaultAsync<int>(_queryObject));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Fact]
        public async Task QueryFirstOrDefaultAsync_Generic_NullQueryObject_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    _session.QueryFirstOrDefaultAsync<int>(NullQueryObject));

            // Assert
            Assert.Equal("queryObject", exception.ParamName);
        }

        [Fact]
        public async Task QueryFirstOrDefaultAsync_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    NullDbSession.QueryFirstOrDefaultAsync(_queryObject));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Fact]
        public async Task QueryFirstOrDefaultAsync_NullQueryObject_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    _session.QueryFirstOrDefaultAsync(NullQueryObject));

            // Assert
            Assert.Equal("queryObject", exception.ParamName);
        }

        [Fact]
        public async Task ExecuteAsync_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() => NullDbSession.ExecuteAsync(_queryObject));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Fact]
        public async Task ExecuteAsync_NullQueryObject_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() => _session.ExecuteAsync(NullQueryObject));

            // Assert
            Assert.Equal("queryObject", exception.ParamName);
        }

        [Fact]
        public async Task ExecuteScalarAsync_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    NullDbSession.ExecuteScalarAsync(_queryObject));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Fact]
        public async Task ExecuteScalarAsync_NullQueryObject_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() => _session.ExecuteScalarAsync(NullQueryObject));

            // Assert
            Assert.Equal("queryObject", exception.ParamName);
        }

        [Fact]
        public async Task ExecuteScalarAsync_Generic_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    NullDbSession.ExecuteScalarAsync<int>(_queryObject));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Fact]
        public async Task ExecuteScalarAsync_Generic_NullQueryObject_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    _session.ExecuteScalarAsync<int>(NullQueryObject));

            // Assert
            Assert.Equal("queryObject", exception.ParamName);
        }

        [Fact]
        public async Task QueryMultipleAsync_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    NullDbSession.QueryMultipleAsync(_queryObject));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Fact]
        public async Task QueryMultipleAsync_NullQueryObject_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() => _session.QueryMultipleAsync(NullQueryObject));

            // Assert
            Assert.Equal("queryObject", exception.ParamName);
        }

        [Fact]
        public async Task Query_NullQueryObject_ThrowsException()
        {
            // Act
            try
            {
                await _session.QueryUnbufferedAsync(NullQueryObject).ToArrayAsync();
                Assert.False(true);
            }
            catch (ArgumentNullException exception)
            {
                // Assert
                Assert.Equal("queryObject", exception.ParamName);
            }
        }

        [Fact]
        public async Task QueryUnbufferedAsync_NullSession_ThrowsException()
        {
            // Act
            try
            {
                await NullDbSession!.QueryUnbufferedAsync(_queryObject).ToArrayAsync();
                Assert.False(true);
            }
            catch (ArgumentNullException exception)
            {
                // Assert
                Assert.Equal("session", exception.ParamName);
            }
        }

        [Fact]
        public async Task QueryUnbufferedAsync_Generic_NullQueryObject_ThrowsException()
        {
            // Act
            try
            {
                await _session.QueryUnbufferedAsync<int>(NullQueryObject).ToArrayAsync();
                Assert.False(true);
            }
            catch (ArgumentNullException exception)
            {
                // Assert
                Assert.Equal("queryObject", exception.ParamName);
            }
        }

        [Fact]
        public async Task QueryUnbufferedAsync_Generic_NullSession_ThrowsException()
        {
            // Act
            try
            {
                await NullDbSession!.QueryUnbufferedAsync<int>(_queryObject).ToArrayAsync();
                Assert.False(true);
            }
            catch (ArgumentNullException exception)
            {
                // Assert
                Assert.Equal("session", exception.ParamName);
            }
        }
    }
}