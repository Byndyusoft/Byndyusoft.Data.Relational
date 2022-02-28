using System;
using Moq;
using Xunit;

namespace Byndyusoft.Data.Relational.Unit
{
    public class DbSessionConsumerTests
    {
        private readonly DbSessionConsumerImpl _repository;
        private readonly IDbSessionAccessor _sessionAccessor;

        public DbSessionConsumerTests()
        {
            _sessionAccessor = Mock.Of<IDbSessionAccessor>();
            _repository = new DbSessionConsumerImpl(_sessionAccessor);
        }

        [Fact]
        public void DbSession_CallsAccessor()
        {
            // Arrange
            using var session = Mock.Of<IDbSession>();
            Mock.Get(_sessionAccessor).SetupGet(x => x.DbSession).Returns(session);

            // Act
            var result = _repository.DbSession;

            // Assert
            Assert.Same(session, result);
        }


        [Fact]
        public void DbSession_NoCurrentOne_ThrowsException()
        {
            // Arrange
            Mock.Get(_sessionAccessor).SetupGet(x => x.DbSession).Returns(null as IDbSession);

            // Act
            var exception = Assert.Throws<InvalidOperationException>(() => _repository.DbSession);

            // Assert
            Assert.Equal($"There is no current {nameof(DbSession)}", exception.Message);
        }

        private class DbSessionConsumerImpl : DbSessionConsumer
        {
            public DbSessionConsumerImpl(IDbSessionAccessor sessionAccessor) : base(sessionAccessor)
            {
            }

            public new IDbSession DbSession => base.DbSession;
        }
    }
}