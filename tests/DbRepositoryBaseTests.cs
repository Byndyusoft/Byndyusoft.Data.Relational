using Moq;
using Xunit;

namespace Byndyusoft.Data.Relational
{
    public class DbRepositoryBaseTests
    {
        private readonly IDbSessionAccessor _sessionAccessor;
        private readonly DbRepositoryImpl _repository;

        private class DbRepositoryImpl : DbRepositoryBase
        {
            public DbRepositoryImpl(IDbSessionAccessor sessionAccessor) : base(sessionAccessor)
            {
            }

            public new IDbSession Session => base.Session;
        }


        public DbRepositoryBaseTests()
        {
            _sessionAccessor = Mock.Of<IDbSessionAccessor>();
            _repository = new DbRepositoryImpl(_sessionAccessor);
        }

        [Fact]
        public void Session_CallsAccessor()
        {
            // Arrange
            var session = Mock.Of<IDbSession>();
            Mock.Get(_sessionAccessor).SetupGet(x => x.DbSession).Returns(session);

            // Act
            var result = _repository.Session;

            // Assert
            Assert.Same(session, result);
        }
    }
}