using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Xunit;

namespace Byndyusoft.Data.Relational.Unit
{
    public class DbSessionTests
    {
        private readonly CancellationToken _cancellationToken = new();
        private readonly DbConnection _connection;
        private readonly DbTransaction _transaction;

        public DbSessionTests()
        {
            _transaction = new Mock<DbTransaction> {CallBase = true}.Object;
            _connection = new Mock<DbConnection> {CallBase = true}.Object;
        }

        [Fact]
        public void Constructor_WithConnection()
        {
            // Arrange
            using var session = new DbSession(_connection);

            // Assert
            Assert.Equal(_connection, session.Connection);
            Assert.Null(session.Transaction);
        }

        [Fact]
        public void Constructor_NullConnection_ThrowsException()
        {
            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => new DbSession(null!));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal("connection", exception.ParamName);
        }

        [Fact]
        public void Constructor_WithConnectionAndTransaction()
        {
            // Arrange
            using var session = new DbSession(_connection, _transaction);

            // Assert
            Assert.Equal(_connection, session.Connection);
            Assert.Equal(_transaction, session.Transaction);
        }

        [Fact]
        public void Connection_Disposed_ThrowsException()
        {
            // Arrange
            using var session = new DbSession(_connection);
            session.Dispose();

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
            using var session = new DbSession(_connection);
            session.Dispose();

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
            using var session = new DbSession(_connection);

            // Act
            session.Dispose();
            session.Dispose();
        }

        [Fact]
        public void Dispose_DisposesConnection()
        {
            // Arrange
            using var session = new DbSession(_connection);

            // Act
            session.Dispose();

            // Assert
            Mock.Get(_connection).Protected().Verify("Dispose", Times.Once(), new object[] {true});
        }

        [Fact]
        public void Dispose_DisposesTransaction()
        {
            // Arrange
            using var session = new DbSession(_connection, _transaction);
            Mock.Get(_transaction).Protected().Setup("Dispose", new object[] {true}).Verifiable();

            // Act
            session.Dispose();

            // Assert
            Mock.Get(_transaction).Protected().Verify("Dispose", Times.Once(), new object[] {true});
        }

        [Fact]
        public void Dispose_DisposesItemsValue()
        {
            // Arrange
            var disposable = Mock.Of<IDisposable>();

            using var session = new DbSession(_connection, _transaction);
            session.Items.Add("key", disposable);

            // Act
            session.Dispose();

            // Assert
            Mock.Get(disposable).Verify(x => x.Dispose(), Times.Once);
        }

        [Fact]
        public async Task DisposeAsync_CanBeDisposedTwice()
        {
            // Arrange
            await using var session = new DbSession(_connection);

            // Act
            await session.DisposeAsync();
            await session.DisposeAsync();
        }

        [Fact]
        public async Task DisposeAsync_DisposesConnection()
        {
            // Arrange
            await using var session = new DbSession(_connection);

            // Act
            await session.DisposeAsync();

            // Assert
            Mock.Get(_connection).Protected().Verify("Dispose", Times.Once(), new object[] {true});
        }

        [Fact]
        public async Task DisposeAsync_DisposesTransaction()
        {
            // Arrange
            await using var session = new DbSession(_connection, _transaction);
            Mock.Get(_transaction).Protected().Setup("Dispose", new object[] {true}).Verifiable();

            // Act
            await session.DisposeAsync();

            // Assert
            Mock.Get(_transaction).Protected().Verify("Dispose", Times.Once(), new object[] {true});
        }

        [Fact]
        public async Task DisposeAsync_DisposesItemsValue()
        {
            // Arrange
            var disposable = Mock.Of<IAsyncDisposable>();

            await using var session = new DbSession(_connection, _transaction);
            session.Items.Add("key", disposable);

            // Act
            await session.DisposeAsync();

            // Assert
            Mock.Get(disposable).Verify(x => x.DisposeAsync(), Times.Once);
        }

        [Fact]
        public async Task CommitAsync_Disposed_ThrowsException()
        {
            // Arrange
            await using var session = new DbSession(_connection, _transaction);
            session.Dispose();

            // Act
            var exception =
                await Assert.ThrowsAsync<ObjectDisposedException>(() => session.CommitAsync(_cancellationToken));

            // Assert
            Assert.Equal(typeof(DbSession).FullName, exception.ObjectName);
        }

        [Fact]
        public async Task CommitAsync_SkipsIfNoTransaction()
        {
            // Arrange
            await using var session = new DbSession(_connection);

            // Act
            await session.CommitAsync(_cancellationToken);

            // Assert
            Mock.Get(_transaction).VerifyNoOtherCalls();
        }

        [Fact]
        public async Task CommitAsync_CommitsTransaction()
        {
            // Arrange
            await using var session = new DbSession(_connection, _transaction);
            Mock.Get(_transaction).Setup(x => x.Commit());

            // Act
            await session.CommitAsync(_cancellationToken);

            // Assert
            Mock.Get(_transaction).Verify(x => x.Commit(), Times.Once);
        }

        [Fact]
        public async Task RollbackAsync_SkipsIfNoTransaction()
        {
            // Arrange
            await using var session = new DbSession(_connection);

            // Act
            await session.RollbackAsync(_cancellationToken);

            // Assert
            Mock.Get(_transaction).VerifyNoOtherCalls();
        }

        [Fact]
        public async Task RollbackAsync_RollbacksTransaction()
        {
            // Arrange
            await using var session = new DbSession(_connection, _transaction);
            await session.StartAsync(_cancellationToken);
            Mock.Get(_transaction).Setup(x => x.Rollback());

            // Act
            await session.RollbackAsync(_cancellationToken);

            // Assert
            Mock.Get(_transaction).Verify(x => x.Rollback(), Times.Once);
        }

        [Fact]
        public void Items_Test()
        {
            // Arrange
            var value = new object();
            using var session = new DbSession(_connection, _transaction);

            // Act
            session.Items.Add("key", value);
            var result = session.Items["key"];

            // Assert
            Assert.Same(value, result);
        }

        [Fact]
        public void IsolationLevel_NoTransaction_ReturnsUnspecified()
        {
            // Arrange
            using var session = new DbSession(_connection);

            // Act
            var isolationLevel = session.IsolationLevel;

            // Assert
            Assert.Equal(IsolationLevel.Unspecified, isolationLevel);
        }

        [Fact]
        public void IsolationLevel_FromTransaction()
        {
            // Arrange
            Mock.Get(_transaction).SetupGet(x => x.IsolationLevel).Returns(IsolationLevel.Chaos);
            using var session = new DbSession(_connection, _transaction);

            // Act
            var isolationLevel = session.IsolationLevel;

            // Assert
            Assert.Equal(IsolationLevel.Chaos, isolationLevel);
        }
    }
}