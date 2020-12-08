using System;
using System.Data.Common;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Byndyusoft.Data.Relational.Unit.DependencyInjection
{
    public class ServiceCollectionTests
    {
        private readonly string _connectionString;
        private readonly DbProviderFactory _dbProviderFactory;
        private readonly IServiceCollection _services;

        public ServiceCollectionTests()
        {
            _connectionString = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;";
            _dbProviderFactory = Mock.Of<DbProviderFactory>();
            _services = new ServiceCollection();
        }

        [Fact]
        public void AddRelationalDb_NullServices_ThrowsException()
        {
            // Arrange
            var services = null as IServiceCollection;

            // Act
            // ReSharper disable once ExpressionIsAlwaysNull
            var exception =
                Assert.Throws<ArgumentNullException>(() =>
                    services.AddRelationalDb(_dbProviderFactory, _connectionString));

            // Assert
            Assert.Equal("services", exception.ParamName);
        }

        [Fact]
        public void AddRelationalDb_NullProvider_ThrowsException()
        {
            // Act
            var exception =
                Assert.Throws<ArgumentNullException>(() => _services.AddRelationalDb(null, _connectionString));

            // Assert
            Assert.Equal("dbProviderFactory", exception.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void AddRelationalDb_NullConnectionString_ThrowsException(string connectionString)
        {
            // Act
            // ReSharper disable once ExpressionIsAlwaysNull
            var exception =
                Assert.Throws<ArgumentNullException>(() =>
                    _services.AddRelationalDb(_dbProviderFactory, connectionString));

            // Assert
            Assert.Equal("connectionString", exception.ParamName);
        }

        [Fact]
        public void AddRelationalDb_Registers_SessionAccessor()
        {
            // Arrange
            _services.AddRelationalDb(_dbProviderFactory, _connectionString);

            // Act
            var service = _services.BuildServiceProvider().GetService<IDbSessionAccessor>();

            // Assert
            Assert.NotNull(service);
            Assert.IsType<DbSessionAccessor>(service);
        }

        [Fact]
        public void AddRelationalDb_Registers_SessionFactory()
        {
            // Arrange
            _services.AddRelationalDb(_dbProviderFactory, _connectionString);

            // Act
            var service = _services.BuildServiceProvider().GetService<IDbSessionFactory>();

            // Assert
            Assert.NotNull(service);
            var sessionFactory = Assert.IsType<DbSessionFactory>(service);
            Assert.Equal(_dbProviderFactory, sessionFactory.ProviderFactory);
            Assert.Equal(_connectionString, sessionFactory.ConnectionString);
        }

        [Fact]
        public void AddRelationalDb_Func_NullServices_ThrowsException()
        {
            // Arrange
            var services = null as IServiceCollection;

            // Act
            // ReSharper disable once ExpressionIsAlwaysNull
            var exception =
                Assert.Throws<ArgumentNullException>(() =>
                    services.AddRelationalDb(_dbProviderFactory, () => _connectionString));

            // Assert
            Assert.Equal("services", exception.ParamName);
        }

        [Fact]
        public void AddRelationalDb_Func_NullProvider_ThrowsException()
        {
            // Act
            var exception =
                Assert.Throws<ArgumentNullException>(() => _services.AddRelationalDb(null, () => _connectionString));

            // Assert
            Assert.Equal("dbProviderFactory", exception.ParamName);
        }

        [Fact]
        public void AddRelationalDb_Func_NullFunc_ThrowsException()
        {
            // Act
            var exception =
                Assert.Throws<ArgumentNullException>(() =>
                    _services.AddRelationalDb(_dbProviderFactory, null as Func<string>));

            // Assert
            Assert.Equal("connectionStringFunc", exception.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void AddRelationalDb_Func_NullConnectionString_ThrowsException(string connectionString)
        {
            // Act
            // ReSharper disable once ExpressionIsAlwaysNull
            var exception =
                Assert.Throws<ArgumentNullException>(() =>
                    _services.AddRelationalDb(_dbProviderFactory, () => connectionString));

            // Assert
            Assert.Equal("connectionString", exception.ParamName);
        }

        [Fact]
        public void AddRelationalDb_Func_Registers_SessionAccessor()
        {
            // Arrange
            _services.AddRelationalDb(_dbProviderFactory, () => _connectionString);

            // Act
            var service = _services.BuildServiceProvider().GetService<IDbSessionAccessor>();

            // Assert
            Assert.NotNull(service);
            Assert.IsType<DbSessionAccessor>(service);
        }

        [Fact]
        public void AddRelationalDb_Func_Registers_SessionFactory()
        {
            // Arrange
            _services.AddRelationalDb(_dbProviderFactory, () => _connectionString);

            // Act
            var service = _services.BuildServiceProvider().GetService<IDbSessionFactory>();

            // Assert
            Assert.NotNull(service);
            var sessionFactory = Assert.IsType<DbSessionFactory>(service);
            Assert.Equal(_dbProviderFactory, sessionFactory.ProviderFactory);
            Assert.Equal(_connectionString, sessionFactory.ConnectionString);
        }
    }
}