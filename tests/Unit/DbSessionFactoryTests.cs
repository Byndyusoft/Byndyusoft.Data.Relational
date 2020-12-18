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
    public class DbSessionFactoryTests
    {
        private readonly CancellationToken _cancellationToken;
        private readonly DbConnection _connection;
        private readonly string _connectionString;
        private readonly DbProviderFactory _dbProviderFactory;
        private readonly IsolationLevel _isolationLevel;
        private readonly IDbSessionAccessor _sessionAccessor;
        private readonly DbTransaction _transaction;

        public DbSessionFactoryTests()
        {
            _isolationLevel = IsolationLevel.Serializable;
            _cancellationToken = new CancellationToken();
            _connectionString = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;";
            _sessionAccessor = new DbSessionAccessor();

            _dbProviderFactory = new Mock<DbProviderFactory> {CallBase = true}.Object;
            _connection = new Mock<DbConnection> {CallBase = true}.Object;
            _transaction = new Mock<DbTransaction> {CallBase = true}.Object;

            Mock.Get(_dbProviderFactory).Setup(x => x.CreateConnection()).Returns(_connection);
        }

        [Fact]
        public void Constructor_NullProvider_ThrowsException()
        {
            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => new DbSessionFactory(null, _connectionString));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal("dbProviderFactory", exception.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Constructor_NullConnectionString_ThrowsException(string connectionString)
        {
            // Act
            var exception =
                Assert.Throws<ArgumentNullException>(() => new DbSessionFactory(_dbProviderFactory, connectionString));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal("connectionString", exception.ParamName);
        }

        [Fact]
        public async Task CreateSessionAsync_CreatesSession()
        {
            // Arrange
            Mock.Get(_dbProviderFactory).Setup(x => x.CreateConnection()).Returns(_connection);
            Mock.Get(_connection).SetupSet(x => x.ConnectionString = _connectionString);

            // Act
            var factory = new DbSessionFactory(_dbProviderFactory, _connectionString);
            await using var session = await factory.CreateSessionAsync(_cancellationToken);

            // Assert
            Assert.Equal(session.Connection, _connection);
            Mock.Get(_connection).VerifyAll();
        }

        [Fact]
        public async Task CreateSessionAsync_ProviderCreatesNullConnection_ThrowsException()
        {
            // Arrange
            Mock.Get(_dbProviderFactory).Setup(x => x.CreateConnection()).Returns(null as DbConnection);

            // Act
            var factory = new DbSessionFactory(_dbProviderFactory, _connectionString);
            await Assert.ThrowsAsync<InvalidOperationException>(() => factory.CreateSessionAsync(_cancellationToken));
            Assert.Null(_sessionAccessor.DbSession);
        }

        [Fact]
        public async Task CreateSessionAsync_SetsSessionToAccessor()
        {
            // Act
            var factory = new DbSessionFactory(_dbProviderFactory, _connectionString);
            await using var session = await factory.CreateSessionAsync(_cancellationToken);

            // Assert
            Assert.NotNull(_sessionAccessor.DbSession);
            Assert.Same(session, _sessionAccessor.DbSession);
        }

        [Fact]
        public async Task CreateSessionAsync_AlreadyExists_ThrowsException()
        {
            // Arrange
            var factory = new DbSessionFactory(_dbProviderFactory, _connectionString);
            await using var session = await factory.CreateSessionAsync(_cancellationToken);

            // Act
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                factory.CreateSessionAsync(_cancellationToken));
        }

        [Fact]
        public async Task CreateCommittableSessionAsync_ProviderCreatesNullConnection_ThrowsException()
        {
            // Arrange
            Mock.Get(_dbProviderFactory).Setup(x => x.CreateConnection()).Returns(null as DbConnection);

            // Act
            var factory = new DbSessionFactory(_dbProviderFactory, _connectionString);
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                factory.CreateCommittableSessionAsync(_cancellationToken));
            Assert.Null(_sessionAccessor.DbSession);
        }

        [Fact]
        public async Task CreateCommittableSessionAsync_CreatesSession()
        {
            // Arrange
            Mock.Get(_dbProviderFactory).Setup(x => x.CreateConnection()).Returns(_connection);
            Mock.Get(_connection).SetupSet(x => x.ConnectionString = _connectionString);
            Mock.Get(_connection).Protected().Setup<DbTransaction>("BeginDbTransaction", _isolationLevel)
                .Returns(_transaction);

            // Act
            var factory = new DbSessionFactory(_dbProviderFactory, _connectionString);
            await using var session = await factory.CreateCommittableSessionAsync(_isolationLevel, _cancellationToken);

            // Assert
            Assert.Equal(session.Connection,_connection);
            Assert.Equal(session.Transaction, _transaction);
        }

        [Fact]
        public async Task CreateCommittableSessionAsync_SetsSessionToAccessor()
        {
            // Act
            var factory = new DbSessionFactory(_dbProviderFactory, _connectionString);
            await using var session = await factory.CreateCommittableSessionAsync(_cancellationToken);

            // Assert
            Assert.NotNull(_sessionAccessor.DbSession);
            Assert.Equal(session, _sessionAccessor.DbSession);
        }

        [Fact]
        public async Task CreateCommittableSessionAsync_AlreadyExists_ThrowsException()
        {
            // Arrange
            var factory = new DbSessionFactory(_dbProviderFactory, _connectionString);
            await using var session = await factory.CreateCommittableSessionAsync(_cancellationToken);

            // Act
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                factory.CreateCommittableSessionAsync(_cancellationToken));
        }

        [Fact]
        public async Task CreateCommittableSessionAsync_CanNotBeginTransaction_DisposesConnection()
        {
            // Arrange
            Mock.Get(_dbProviderFactory).Setup(x => x.CreateConnection()).Returns(_connection);
            Mock.Get(_connection).SetupSet(x => x.ConnectionString = _connectionString);
#if NETCOREAPP2_1
            Mock.Get(_connection).Protected().Setup<DbTransaction>("BeginDbTransaction", _isolationLevel)
                .Throws(new DataException());
#else
           Mock.Get(_connection).Protected().Setup<ValueTask<DbTransaction>>("BeginDbTransactionAsync", _isolationLevel, _cancellationToken)
                .Throws(new DataException());
#endif

            // Act
            var factory = new DbSessionFactory(_dbProviderFactory, _connectionString);
            await Assert.ThrowsAsync<DataException>(() => factory.CreateCommittableSessionAsync(_isolationLevel, _cancellationToken));

            // Assert
            Mock.Get(_connection).Protected().Verify("Dispose", Times.Once(), new object[]{true});
            Assert.Null(_sessionAccessor.DbSession);
        }
    }
}