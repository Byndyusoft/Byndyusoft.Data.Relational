using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace Byndyusoft.Data.Relational
{
    public class DbSessionFactoryTests
    {
        private readonly string _connectionString;
        private readonly DbProviderFactory _dbProviderFactory;
        private readonly IDbSessionAccessor _sessionAccessor;

        public DbSessionFactoryTests()
        {
            _connectionString = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;";
            _dbProviderFactory = Mock.Of<DbProviderFactory>();
            _sessionAccessor = new DbSessionAccessor();

            Mock.Get(_dbProviderFactory).Setup(x => x.CreateConnection()).Returns(Mock.Of<DbConnection>());
        }

        [Fact]
        public void Constructor_WithProviderAndConnectionString()
        {
            // Act
            var factory = new DbSessionFactory(_dbProviderFactory, _connectionString);

            // Assert
            Assert.Equal(_connectionString, factory.ConnectionString);
            Assert.Equal(_dbProviderFactory, factory.DbProviderFactory);
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
        public async ValueTask CreateSessionAsync_CreatesSession()
        {
            // Arrange
            var connection = Mock.Of<DbConnection>();
            Mock.Get(_dbProviderFactory).Setup(x => x.CreateConnection()).Returns(connection);

            // Act
            var factory = new DbSessionFactory(_dbProviderFactory, _connectionString);
            var session = await factory.CreateSessionAsync();

            // Assert
            Assert.Equal(connection, session.Connection);
            Assert.Equal(_connectionString, session.Connection.ConnectionString);
        }

        [Fact]
        public async ValueTask CreateSessionAsync_ProviderCreatesNullConnection_ThrowsException()
        {
            // Arrange
            Mock.Get(_dbProviderFactory).Setup(x => x.CreateConnection()).Returns(null as DbConnection);

            // Act
            var factory = new DbSessionFactory(_dbProviderFactory, _connectionString);
            await Assert.ThrowsAsync<InvalidOperationException>(() => factory.CreateSessionAsync());
        }

        [Fact]
        public async ValueTask CreateSessionAsync_SetsSessionToAccessor()
        {
            // Act
            var factory = new DbSessionFactory(_dbProviderFactory, _connectionString);
            var session = await factory.CreateSessionAsync();

            // Assert
            Assert.NotNull(_sessionAccessor.DbSession);
            Assert.Same(session, _sessionAccessor.DbSession);
        }

        [Fact]
        public async ValueTask CreateCommittableSessionAsync_Commitable_ProviderCreatesNullConnection_ThrowsException()
        {
            // Arrange
            Mock.Get(_dbProviderFactory).Setup(x => x.CreateConnection()).Returns(null as DbConnection);

            // Act
            var factory = new DbSessionFactory(_dbProviderFactory, _connectionString);
            await Assert.ThrowsAsync<InvalidOperationException>(() => factory.CreateCommittableSessionAsync());
        }

        [Fact]
        public async ValueTask CreateCommittableSessionAsync_Commitable_CreatesSession()
        {
            // Arrange
            var isolationLevel = IsolationLevel.Unspecified;
            var connection = Mock.Of<DbConnection>();
            Mock.Get(_dbProviderFactory).Setup(x => x.CreateConnection()).Returns(connection);

            // Act
            var factory = new DbSessionFactory(_dbProviderFactory, _connectionString);
            var session = await factory.CreateCommittableSessionAsync(isolationLevel, CancellationToken.None);

            // Assert
            Assert.Equal(connection, session.Connection);
            Assert.Equal(isolationLevel, session.IsolationLevel);
            Assert.Equal(_connectionString, session.Connection.ConnectionString);
        }

        [Fact]
        public async ValueTask CreateCommittableSessionAsync_Commitable_SetsSessionToAccessor()
        {
            // Act
            var factory = new DbSessionFactory(_dbProviderFactory, _connectionString);
            var session = await factory.CreateCommittableSessionAsync();

            // Assert
            Assert.NotNull(_sessionAccessor.DbSession);
            Assert.Same(session, _sessionAccessor.DbSession);
        }
    }
}
