namespace Byndyusoft.Data.Relational
{
    using System;
    using System.Data.Common;
    using System.Threading;
    using System.Threading.Tasks;
    using Moq;
    using Moq.Protected;
    using Xunit;
    
    public class DbSessionTests
    {
        private readonly DbConnection _connection;
        
        public DbSessionTests()
        {
            _connection = new Mock<DbConnection> { CallBase = true }.Object;
        }

        [Fact]
        public void Constructor_WithConnection()
        {
            // Arrange
            var session = new DbSession(_connection);

            // Assert
            Assert.Equal(_connection, session.Connection);
            Assert.Null(session.Transaction);
        }

        [Fact]
        public void Constructor_NullConnection_ThrowsException()
        {
            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => new DbSession(null));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal("connection", exception.ParamName);
        }

        [Fact]
        public void Connection_Disposed_ThrowsException()
        {
            // Arrange
            var session = new DbSession(_connection);
            using (session) { }

            // Act
            var exception = Assert.Throws<ObjectDisposedException>(() => session.Connection);

            // Assert
            Assert.NotNull(exception);
            Assert.Equal(typeof(DbSession).FullName, exception.ObjectName);
        }

        [Fact]
        public void Transaction_Disposed_ThrowsException()
        {
            // Arrange
            var session = new DbSession(_connection);
            using (session) { }

            // Act
            var exception = Assert.Throws<ObjectDisposedException>(() => session.Transaction);

            // Assert
            Assert.NotNull(exception);
            Assert.Equal(typeof(DbSession).FullName, exception.ObjectName);
        }

        [Fact]
        public void Dispose_CanBeDisposedTwice()
        {
            // Arrange
            var session = new DbSession(_connection);

            // Act
            using (session) { }
            using (session) { }
        }

        [Fact]
        public void Dispose_DisposesConnection()
        {
            // Arrange
            var session = new DbSession(_connection);

            // Act
            using (session) { }

            // Assert
            Mock.Get(_connection).Protected().Verify("Dispose", Times.Once(), new object[]{true});
        }

#if NETCOREAPP3_1

        private readonly CancellationToken _cancellationToken = new CancellationTokenSource().Token;

        [Fact]
        public async ValueTask DisposeAsync_CanBeDisposedTwice()
        {
            // Arrange
            var session = new DbSession(_connection);

            // Act
            await using (session) { }
            await using (session) { }
        }

        [Fact]
        public async ValueTask DisposeAsync_DisposesConnection()
        {
            // Arrange
            var session = new DbSession(_connection);
            await session.EnsureOpenedAsync(_cancellationToken);

            // Act
            await using (session) { }

            // Assert
            Mock.Get(_connection).Verify(x => x.DisposeAsync(), Times.Once);
        }

#endif
    }
}