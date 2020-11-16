using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Xunit;

namespace Byndyusoft.Data.Relational
{
    public class CommittableDbSessionTests
    {
        private readonly IsolationLevel _isolationLevel;
        private readonly DbConnection _connection;
        private readonly DbTransaction _transaction;
        private readonly CancellationToken _cancellationToken;

        public CommittableDbSessionTests()
        {
            _isolationLevel = IsolationLevel.Chaos;
            _transaction = new Mock<DbTransaction> {CallBase = true}.Object;
            _connection = new Mock<DbConnection> {CallBase = true}.Object;
            _cancellationToken = new CancellationTokenSource().Token;

            Mock.Get(_connection).Protected().Setup<DbTransaction>("BeginDbTransaction", _isolationLevel)
                .Returns(_transaction).Verifiable();
        }

        [Fact]
        public void Constructor_WithConnectionAndIsolationLevel()
        {
            // Arrange
            var session = new CommittableDbSession(_connection, _isolationLevel);

            // Assert
            Assert.Equal(_connection, session.Connection);
            Assert.Equal(_isolationLevel, session.IsolationLevel);
        }

        [Fact]
        public void Constructor_NullConnection_ThrowsException()
        {
            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => new CommittableDbSession(null, _isolationLevel));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal("connection", exception.ParamName);
        }

        [Fact]
        public void Commit_Disposed_ThrowsException()
        {
            // Arrange
            var session = new CommittableDbSession(_connection, _isolationLevel);
            using (session)
            {
            }

            // Act
            var exception = Assert.Throws<ObjectDisposedException>(() => session.Commit());

            // Assert
            Assert.Equal(typeof(CommittableDbSession).FullName, exception.ObjectName);
        }

        [Fact]
        public void Commit_SkipsIfNoTransaction()
        {
            // Arrange
            var session = new CommittableDbSession(_connection, _isolationLevel);

            // Act
            session.Commit();

            // Assert
            Mock.Get(_transaction).VerifyNoOtherCalls();
        }

        [Fact]
        public async ValueTask Commit_CommitsTransaction()
        {
            // Arrange
            var session = new CommittableDbSession(_connection, _isolationLevel);
            await session.EnsureOpenedAsync(_cancellationToken);

            // Act
            session.Commit();

            // Assert
            Mock.Get(_transaction).Verify(x => x.Commit(), Times.Once);
        }

        [Fact]
        public void Rollback_SkipsIfNoTransaction()
        {
            // Arrange
            var session = new CommittableDbSession(_connection, _isolationLevel);

            // Act
            session.Rollback();

            // Assert
            Mock.Get(_transaction).VerifyNoOtherCalls();
        }

        [Fact]
        public async ValueTask Commit_RollbacksTransaction()
        {
            // Arrange
            var session = new CommittableDbSession(_connection, _isolationLevel);
            await session.EnsureOpenedAsync(_cancellationToken);

            // Act
            session.Rollback();

            // Assert
            Mock.Get(_transaction).Verify(x => x.Rollback(), Times.Once);
        }

        [Fact]
        public async ValueTask Dispose_DisposesTransaction()
        {
            // Arrange
            var session = new DbSession(_connection);
            await session.EnsureOpenedAsync(_cancellationToken);

            // Act
            // ReSharper disable once UseAwaitUsing
            using (session)
            {
            }

            // Assert
            Mock.Get(_transaction).Protected().Verify("Dispose", Times.Once(), new object[] {true});
        }

#if !NETCOREAPP2_1
        [Fact]
        public async ValueTask CommitAsync_Disposed_ThrowsException()
        {
            // Arrange
            var session = new CommittableDbSession(_connection, _isolationLevel);
            await using (session) { }

            // Act
            var exception = Assert.Throws<ObjectDisposedException>(() => session.Commit());

            // Assert
            Assert.Equal(typeof(CommittableDbSession).FullName, exception.ObjectName);
        }

        [Fact]
        public async ValueTask CommitAsync_SkipsIfNoTransaction()
        {
            // Arrange
            var session = new CommittableDbSession(_connection, _isolationLevel);

            // Act
            await session.CommitAsync(_cancellationToken);

            // Assert
            Mock.Get(_transaction).VerifyNoOtherCalls();
        }

        [Fact]
        public async ValueTask CommitAsync_CommitsTransaction()
        {
            // Arrange
            var session = new CommittableDbSession(_connection, _isolationLevel);
            await session.EnsureOpenedAsync(_cancellationToken);

            // Act
            await session.CommitAsync(_cancellationToken);

            // Assert
            Mock.Get(_transaction).Verify(x => x.CommitAsync(_cancellationToken), Times.Once);
        }

        [Fact]
        public async ValueTask RollbackAsync_SkipsIfNoTransaction()
        {
            // Arrange
            var session = new CommittableDbSession(_connection, _isolationLevel);

            // Act
            await session.RollbackAsync(_cancellationToken);

            // Assert
            Mock.Get(_transaction).VerifyNoOtherCalls();
        }

        [Fact]
        public async ValueTask RollbackAsync_CommitsTransaction()
        {
            // Arrange
            var session = new CommittableDbSession(_connection, _isolationLevel);
            await session.EnsureOpenedAsync(_cancellationToken);

            // Act
            await session.RollbackAsync(_cancellationToken);

            // Assert
            Mock.Get(_transaction).Verify(x => x.RollbackAsync(_cancellationToken), Times.Once);
        }

        [Fact]
        public async ValueTask DisposeAsync_DisposesTransaction()
        {
            // Arrange
            var session = new CommittableDbSession(_connection, _isolationLevel);
            await session.EnsureOpenedAsync(_cancellationToken);

            // Act
            await using (session) { }

            // Assert
            Mock.Get(_transaction).Verify(x => x.DisposeAsync(), Times.Exactly(3));
        }

#endif
    }
}
