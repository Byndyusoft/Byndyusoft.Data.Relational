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
                    ((IDbSession) null).QueryAsync<int>(_queryObject));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Fact]
        public async Task QueryAsync_Generic_NullQueryObject_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() => _session.QueryAsync<int>((IQueryObject) null));

            // Assert
            Assert.Equal("queryObject", exception.ParamName);
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
        public async Task QuerySingleAsync_Generic_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    ((IDbSession) null).QuerySingleAsync<int>(_queryObject));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Fact]
        public async Task QuerySingleAsync_Generic_NullQueryObject_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    _session.QuerySingleAsync<int>((IQueryObject) null));

            // Assert
            Assert.Equal("queryObject", exception.ParamName);
        }

        [Fact]
        public async Task QuerySingleAsync_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(
                    () => ((IDbSession) null).QuerySingleAsync(_queryObject));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Fact]
        public async Task QuerySingleAsync_NullQueryObject_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() => _session.QuerySingleAsync((IQueryObject) null));

            // Assert
            Assert.Equal("queryObject", exception.ParamName);
        }

        [Fact]
        public async Task QuerySingleOrDefaultAsync_Generic_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    ((IDbSession) null).QuerySingleOrDefaultAsync<int>(_queryObject));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Fact]
        public async Task QuerySingleOrDefaultAsync_Generic_NullQueryObject_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    _session.QuerySingleOrDefaultAsync<int>((IQueryObject) null));

            // Assert
            Assert.Equal("queryObject", exception.ParamName);
        }

        [Fact]
        public async Task QuerySingleOrDefaultAsync_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    ((IDbSession) null).QuerySingleOrDefaultAsync(_queryObject));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Fact]
        public async Task QuerySingleOrDefaultAsync_NullQueryObject_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    _session.QuerySingleOrDefaultAsync((IQueryObject) null));

            // Assert
            Assert.Equal("queryObject", exception.ParamName);
        }

        [Fact]
        public async Task QueryFirstAsync_Generic_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    ((IDbSession) null).QueryFirstAsync<int>(_queryObject));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Fact]
        public async Task QueryFirstAsync_Generic_NullQueryObject_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(
                    () => _session.QueryFirstAsync<int>((IQueryObject) null));

            // Assert
            Assert.Equal("queryObject", exception.ParamName);
        }

        [Fact]
        public async Task QueryFirstAsync_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    ((IDbSession) null).QueryFirstAsync(_queryObject));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Fact]
        public async Task QueryFirstAsync_NullQueryObject_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() => _session.QueryFirstAsync((IQueryObject) null));

            // Assert
            Assert.Equal("queryObject", exception.ParamName);
        }

        [Fact]
        public async Task QueryFirstOrDefaultAsync_Generic_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    ((IDbSession) null).QueryFirstOrDefaultAsync<int>(_queryObject));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Fact]
        public async Task QueryFirstOrDefaultAsync_Generic_NullQueryObject_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    _session.QueryFirstOrDefaultAsync<int>((IQueryObject) null));

            // Assert
            Assert.Equal("queryObject", exception.ParamName);
        }

        [Fact]
        public async Task QueryFirstOrDefaultAsync_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    ((IDbSession) null).QueryFirstOrDefaultAsync(_queryObject));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Fact]
        public async Task QueryFirstOrDefaultAsync_NullQueryObject_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    _session.QueryFirstOrDefaultAsync((IQueryObject) null));

            // Assert
            Assert.Equal("queryObject", exception.ParamName);
        }

        [Fact]
        public async Task ExecuteAsync_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() => ((IDbSession) null).ExecuteAsync(_queryObject));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Fact]
        public async Task ExecuteAsync_NullQueryObject_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() => _session.ExecuteAsync((IQueryObject) null));

            // Assert
            Assert.Equal("queryObject", exception.ParamName);
        }

        [Fact]
        public async Task ExecuteScalarAsync_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    ((IDbSession) null).ExecuteScalarAsync(_queryObject));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Fact]
        public async Task ExecuteScalarAsync_NullQueryObject_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() => _session.ExecuteScalarAsync((IQueryObject) null));

            // Assert
            Assert.Equal("queryObject", exception.ParamName);
        }

        [Fact]
        public async Task ExecuteScalarAsync_Generic_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    ((IDbSession) null).ExecuteScalarAsync<int>(_queryObject));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Fact]
        public async Task ExecuteScalarAsync_Generic_NullQueryObject_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    _session.ExecuteScalarAsync<int>((IQueryObject) null));

            // Assert
            Assert.Equal("queryObject", exception.ParamName);
        }

        [Fact]
        public async Task QueryMultipleAsync_NullSession_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    ((IDbSession) null).QueryMultipleAsync(_queryObject));

            // Assert
            Assert.Equal("session", exception.ParamName);
        }

        [Fact]
        public async Task QueryMultipleAsync_NullQueryObject_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(() => _session.QueryMultipleAsync((IQueryObject) null));

            // Assert
            Assert.Equal("queryObject", exception.ParamName);
        }

        [Fact]
        public async Task Query_NullQueryObject_ThrowsException()
        {
            // Act
            try
            {
                await _session.Query((IQueryObject) null).ToArrayAsync();
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
                await ((IDbSession) null).Query(_queryObject).ToArrayAsync();
                Assert.False(true);
            }
            catch (ArgumentNullException exception)
            {
                // Assert
                Assert.Equal("session", exception.ParamName);
            }
        }

        [Fact]
        public async Task Query_Generic_NullQueryObject_ThrowsException()
        {
            // Act
            try
            {
                await _session.Query<int>((IQueryObject) null).ToArrayAsync();
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
                await ((IDbSession) null).Query<int>(_queryObject).ToArrayAsync();
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