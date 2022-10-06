using System;
using System.Data.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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
                    services!.AddRelationalDb(_dbProviderFactory, _connectionString));

            // Assert
            Assert.Equal("services", exception.ParamName);
        }

        [Fact]
        public void AddRelationalDb_NullProvider_ThrowsException()
        {
            // Act
            var exception =
                Assert.Throws<ArgumentNullException>(() => _services.AddRelationalDb(null!, _connectionString));

            // Assert
            Assert.Equal("dbProviderFactory", exception.ParamName);
        }

        [Theory]
        [InlineData(null, typeof(ArgumentNullException))]
        [InlineData("", typeof(ArgumentException))]
        [InlineData(" ", typeof(ArgumentException))]
        public void AddRelationalDb_NullConnectionString_ThrowsException(string connectionString, Type exceptionType)
        {
            // Act
            // ReSharper disable once ExpressionIsAlwaysNull
            var exception =
                Assert.ThrowsAny<ArgumentException>(() =>
                    _services.AddRelationalDb(_dbProviderFactory, connectionString));

            // Assert
            Assert.IsType(exceptionType, exception);
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
            const string name = "name";
            _services.AddRelationalDb(name, _dbProviderFactory, _connectionString);

            // Act
            var options = _services.BuildServiceProvider().GetService<IOptionsMonitor<DbSessionOptions>>();

            // Assert
            Assert.NotNull(options);
            Assert.Equal(_dbProviderFactory, options.Get(name).DbProviderFactory);
            Assert.Equal(_connectionString, options.Get(name).ConnectionString);
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
                    services!.AddRelationalDb(_dbProviderFactory, () => _connectionString));

            // Assert
            Assert.Equal("services", exception.ParamName);
        }

        [Fact]
        public void AddRelationalDb_Func_NullProvider_ThrowsException()
        {
            // Act
            var exception =
                Assert.Throws<ArgumentNullException>(() => _services.AddRelationalDb(null!, () => _connectionString));

            // Assert
            Assert.Equal("dbProviderFactory", exception.ParamName);
        }

        [Fact]
        public void AddRelationalDb_Func_NullFunc_ThrowsException()
        {
            // Act
            var exception =
                Assert.Throws<ArgumentNullException>(() =>
                    _services.AddRelationalDb(_dbProviderFactory, (null as Func<string>)!));

            // Assert
            Assert.Equal("connectionStringFunc", exception.ParamName);
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
            const string name = "name";
            _services.AddRelationalDb(name, _dbProviderFactory, () => _connectionString);

            // Act
            var options = _services.BuildServiceProvider().GetService<IOptionsMonitor<DbSessionOptions>>();

            // Assert
            Assert.NotNull(options);
            Assert.Equal(_dbProviderFactory, options.Get(name).DbProviderFactory);
            Assert.Equal(_connectionString, options.Get(name).ConnectionString);
        }
    }
}