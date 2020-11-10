using Moq;
using Xunit;

namespace Byndyusoft.Data.Relational
{
    public class DbSessionConsumerTests
    {
        private readonly IDbSessionAccessor _sessionAccessor;
        private readonly DbSessionConsumerImpl _repository;

        private class DbSessionConsumerImpl : DbSessionConsumer
        {
            public DbSessionConsumerImpl(IDbSessionAccessor sessionAccessor) : base(sessionAccessor)
            {
            }

            public new IDbSession DbSession => base.DbSession;
        }

        public DbSessionConsumerTests()
        {
            _sessionAccessor = Mock.Of<IDbSessionAccessor>();
            _repository = new DbSessionConsumerImpl(_sessionAccessor);
        }

        [Fact]
        public void Session_CallsAccessor()
        {
            // Arrange
            var session = Mock.Of<IDbSession>();
            Mock.Get(_sessionAccessor).SetupGet(x => x.DbSession).Returns(session);

            // Act
            var result = _repository.DbSession;

            // Assert
            Assert.Same(session, result);
        }
    }
}